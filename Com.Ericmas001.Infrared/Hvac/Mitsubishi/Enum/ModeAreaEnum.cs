namespace Com.Ericmas001.Infrared.Hvac.Mitsubishi.Enum
{
    public enum ModeAreaEnum : byte
    {
        //NotSet          # 0x00      0000 0000        0
        //Left            # 0x40      0100 0000       64
        //Right           # 0xC0      1100 0000      192
        //Full            # 0x80      1000 0000      128

        NotSet = 0b0000_0000,
        Left = 0b0100_0000,
        Right = 0b1100_0000,
        Full = 0b1000_0000
    }
}
