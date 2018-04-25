namespace Com.Ericmas001.Infrared.Hvac.Mitsubishi.Enum
{
    public enum ModeTimeControlEnum : byte
    {
        //NoTimeControl   # 0x00      0000 0000        0
        //ControlStart    # 0x05      0000 0101        5
        //ControlEnd      # 0x03      0000 0011        3
        //ControlBoth     # 0x07      0000 0111        7

        NoTimeControl = 0b0000_0000,
        ControlStart = 0b0000_0101,
        ControlEnd = 0b0000_0011,
        ControlBoth = 0b0000_0111
    }
}
