using Com.Ericmas001.Infrared.Enum;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Infrared.Hvac
{
    interface IHvacProtocol
    {
        int DurationLeadingPulse { get; }
        int DurationLeadingGap { get; }
        int DurationOnePulse { get; }
        int DurationOneGap { get; }
        int DurationZeroPulse { get; }
        int DurationZeroGap { get; }
        int DurationTrailingPulse { get; }
        int DurationTrailingGap { get; }
        int NbPackets { get; }
        byte MaxMask { get; }
        bool MustInvert { get; }
        ProtocolEnum Protocol { get; }

        byte[] GeneratePacket(ILoggerService logger);
    }
}
