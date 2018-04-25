using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Com.Ericmas001.Infrared.Enum;
using Com.Ericmas001.Infrared.Hvac.Mitsubishi.Enum;
using Com.Ericmas001.Logs.Enums;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Infrared.Hvac.Mitsubishi
{
    public class MitsubishiProtocol : IHvacProtocol
    {
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private enum Index
        {
            Header0 = 0,
            Header1 = 1,
            Header2 = 2,
            Header3 = 3,
            Header4 = 4,
            Power = 5,
            ClimateAndISee = 6,
            Temperature = 7,
            ClimateAndHorizontalVanne = 8,
            FanAndVerticalVanne = 9,
            Clock = 10,
            EndTime = 11,
            StartTime = 12,
            TimeControlAndArea = 13,
            Unused14 = 14,
            PowerfulMode = 15,
            Unused16 = 16,
            Crc = 17
        }

        private const int MIN_TEMP = 16;
        private const int MAX_TEMP = 31;

        public int DurationLeadingPulse => 3400;
        public int DurationLeadingGap => 1750;
        public int DurationOnePulse => 450;
        public int DurationOneGap => 1300;
        public int DurationZeroPulse => 450;
        public int DurationZeroGap => 420;
        public int DurationTrailingPulse => 440;
        public int DurationTrailingGap => 17100;
        public int NbPackets => 2;
        public byte MaxMask => 0xFF;
        public bool MustInvert => true;
        public ProtocolEnum Protocol => ProtocolEnum.Nec;

        public ModeClimateEnum ClimateMode { get; set; } = ModeClimateEnum.Auto;
        public int Temperature { get; set; } = 21;
        public ModeFanEnum FanMode { get; set; } = ModeFanEnum.Auto;
        public ModeVanneVerticalEnum VanneVerticalMode { get; set; } = ModeVanneVerticalEnum.Auto;
        public ModeVanneHorizontalEnum VanneHorizontalMode { get; set; } = ModeVanneHorizontalEnum.Swing;
        public ModeISeeEnum IseeMode { get; set; } = ModeISeeEnum.Off;
        public ModeAreaEnum AreaMode { get; set; } = ModeAreaEnum.NotSet;
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public ModePowerfulEnum Powerful { get; set; } = ModePowerfulEnum.Off;
        public ModePowerEnum PowerMode { get; set; } = ModePowerEnum.Off;

        private string DisplayByte(byte b)
        {
            return $"{b:03d}  {b:02x}  {b:08b}";
        }
        private byte TimeByte(DateTime? t)
        {
            return t == null ? (byte)0 : TimeByte(t.Value);
        }
        private byte TimeByte(DateTime t)
        {
            return (byte)(t.Hour * 6 + t.Minute / 10);
        }

        public byte[] GeneratePacket(ILoggerService logger)
        {
            var data = new byte[] {0x23, 0xCB, 0x26, 0x01, 0x00, 0x20, 0x08, 0x06, 0x30, 0x45, 0x67, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x1F};
            logger.Log(LogLevelEnum.Verbose, "");

            data[(int) Index.Power] = (byte) PowerMode;
            logger.Log(LogLevelEnum.Verbose, $"PWR: {DisplayByte(data[(int) Index.Power])}");
            logger.Log(LogLevelEnum.Verbose, "");

            data[(int) Index.ClimateAndISee] = (byte) ((byte) ClimateMode | (byte) IseeMode);
            logger.Log(LogLevelEnum.Verbose, $"CLI: {DisplayByte((byte)ClimateMode)}");
            logger.Log(LogLevelEnum.Verbose, $"SEE: {DisplayByte((byte)IseeMode)}");
            logger.Log(LogLevelEnum.Verbose, $"CLS: {DisplayByte(data[(int)Index.ClimateAndISee])}");
            logger.Log(LogLevelEnum.Verbose, "");

            data[(int) Index.Temperature] = (byte)(Math.Max(MIN_TEMP, Math.Min(MAX_TEMP, Temperature)) - 16);
            logger.Log(LogLevelEnum.Verbose, $"TMP: {DisplayByte(data[(int)Index.Temperature])} (asked: {Temperature})");
            logger.Log(LogLevelEnum.Verbose, "");

            data[(int)Index.ClimateAndHorizontalVanne] = (byte)(ClimateMode.Version2() | (byte)VanneHorizontalMode);
            logger.Log(LogLevelEnum.Verbose, $"CLI: {DisplayByte(ClimateMode.Version2())}");
            logger.Log(LogLevelEnum.Verbose, $"HOR: {DisplayByte((byte)VanneHorizontalMode)}");
            logger.Log(LogLevelEnum.Verbose, $"CLH: {DisplayByte(data[(int)Index.ClimateAndHorizontalVanne])}");
            logger.Log(LogLevelEnum.Verbose, "");

            data[(int)Index.FanAndVerticalVanne] = (byte)((byte)FanMode | (byte)VanneVerticalMode);
            logger.Log(LogLevelEnum.Verbose, $"FAN: {DisplayByte(data[(int)Index.FanAndVerticalVanne])}");
            logger.Log(LogLevelEnum.Verbose, "");

            data[(int) Index.Clock] = TimeByte(DateTime.Now);
            logger.Log(LogLevelEnum.Verbose, $"CLK: {DisplayByte(data[(int)Index.Clock])}");
            logger.Log(LogLevelEnum.Verbose, "");

            data[(int)Index.EndTime] = TimeByte(EndTime);
            logger.Log(LogLevelEnum.Verbose, $"ETI: {DisplayByte(data[(int)Index.EndTime])} {EndTime:HH:mm}");
            logger.Log(LogLevelEnum.Verbose, "");

            data[(int)Index.StartTime] = TimeByte(StartTime);
            logger.Log(LogLevelEnum.Verbose, $"STI: {DisplayByte(data[(int)Index.StartTime])} {StartTime:HH:mm}");
            logger.Log(LogLevelEnum.Verbose, "");

            ModeTimeControlEnum timeControl;
            if (EndTime != null && StartTime != null)
                timeControl = ModeTimeControlEnum.ControlBoth;
            else if (EndTime != null)
                timeControl = ModeTimeControlEnum.ControlEnd;
            else if (StartTime != null)
                timeControl = ModeTimeControlEnum.ControlStart;
            else
                timeControl = ModeTimeControlEnum.NoTimeControl;
            data[(int)Index.TimeControlAndArea] = (byte)((byte)timeControl | (byte)AreaMode);
            logger.Log(LogLevelEnum.Verbose, $"TIC: {DisplayByte((byte)timeControl)}");
            logger.Log(LogLevelEnum.Verbose, $"AEA: {DisplayByte((byte)AreaMode)}");
            logger.Log(LogLevelEnum.Verbose, $"TCA: {DisplayByte(data[(int)Index.TimeControlAndArea])}");
            logger.Log(LogLevelEnum.Verbose, "");


            data[(int) Index.PowerfulMode] = (byte)Powerful;
            logger.Log(LogLevelEnum.Verbose, $"FUL: {DisplayByte(data[(int)Index.PowerfulMode])}");
            logger.Log(LogLevelEnum.Verbose, "");

            // CRC is a simple bits addition
            // sum every bytes but the last one
            data[(int) Index.Crc] = (byte) (data.Reverse().Skip(1).Select(x => (int) x).Sum() % (MaxMask + 1));
            logger.Log(LogLevelEnum.Verbose, $"CRC: {DisplayByte(data[(int)Index.Crc])}");
            logger.Log(LogLevelEnum.Verbose, "");

            return data;
        }
    }
}