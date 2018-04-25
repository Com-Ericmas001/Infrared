namespace Com.Ericmas001.Infrared.Hvac.Mitsubishi.Enum
{
    public enum ModePowerfulEnum : byte
    {
        //Off             # 0x00      0000 0000        0
        //On              # 0x08      0000 1000        8

        Off = 0b0000_0000,
        On = 0b0000_1000
    }
}
