namespace Com.Ericmas001.Infrared.Hvac.Mitsubishi.Enum
{
    public enum ModeVanneVerticalEnum : byte
    {
        //Auto            # 0x40      0100 0000       64
        //Top             # 0x48      0100 1000       72
        //MiddleTop       # 0x50      0101 0000       80
        //Middle          # 0x58      0101 1000       88
        //MiddleBottom    # 0x60      0110 0000       96
        //Bottom          # 0x68      0110 1000      104
        //Swing           # 0x78      0111 1000      120

        Auto = 0b0100_0000,
        Top = 0b0100_1000,
        MiddleTop = 0b0101_0000,
        Middle = 0b0101_1000,
        MiddleBottom = 0b0110_0000,
        Bottom = 0b0110_1000,
        Swing = 0b0111_1000
    }
}
