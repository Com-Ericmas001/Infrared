namespace Com.Ericmas001.Infrared.Hvac.Mitsubishi.Enum
{
    public enum ModeISeeEnum : byte
    {
        //Off             # 0x00      0000 0000        0
        //On              # 0x40      0100 0000       64

        Off = 0b0000_0000,
        On = 0b0100_0000
    }
}
