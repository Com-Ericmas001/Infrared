namespace Com.Ericmas001.Infrared.Hvac.Mitsubishi.Enum
{
    public enum ModeVanneHorizontalEnum : byte
    {
        //NotSet          # 0x00      0000 0000        0
        //Left            # 0x10      0001 0000       16
        //MiddleLeft      # 0x20      0010 0000       32
        //Middle          # 0x30      0011 0000       48
        //MiddleRight     # 0x40      0100 0000       64
        //Right           # 0x50      0101 0000       80
        //Swing           # 0xC0      1100 0000      192

        NotSet = 0b0000_0000,
        Left = 0b0001_0000,
        MiddleLeft = 0b0010_0000,
        Middle = 0b0011_0000,
        MiddleRight = 0b0100_0000,
        Right = 0b0101_0000,
        Swing = 0b1100_0000
    }
}
