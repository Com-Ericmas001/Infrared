namespace Com.Ericmas001.Infrared.Hvac.Mitsubishi.Enum
{
    public enum ModeClimateEnum : byte
    {
        //Hot2            # 0x00      0000 0000        0
        //Cold2           # 0x06      0000 0110        6
        //Dry2            # 0x02      0000 0010        2
        //Auto2           # 0x00      0000 0000        0

        Hot = 0b0000_1000,
        Cold = 0b0001_1000,
        Dry = 0b0001_0000,
        Auto = 0b0010_0000
    }

    public static class ClimateModeEnumExtensions
    {
        private enum ModeClimateEnum2 : byte
        {
            //Hot2            # 0x00      0000 0000        0
            //Cold2           # 0x06      0000 0110        6
            //Dry2            # 0x02      0000 0010        2
            //Auto2           # 0x00      0000 0000        0

            Hot = 0b0000_0000,
            Cold = 0b0000_0110,
            Dry = 0b0000_0010,
            Auto = 0b0000_0000
        }
        public static byte Version2(this ModeClimateEnum e)
        {
            return (byte) (ModeClimateEnum2) System.Enum.Parse(typeof(ModeClimateEnum2), e.ToString());
        }
    }
}
