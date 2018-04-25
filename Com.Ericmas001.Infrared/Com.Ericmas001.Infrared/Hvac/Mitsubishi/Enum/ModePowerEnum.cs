namespace Com.Ericmas001.Infrared.Hvac.Mitsubishi.Enum
{
    public enum ModePowerEnum : byte
    {
        //Off             # 0x00      0000 0000        0
        //On              # 0x20      0010 0000       32

        Off = 0b0000_0000, 
        On = 0b0010_0000 
    }
}
