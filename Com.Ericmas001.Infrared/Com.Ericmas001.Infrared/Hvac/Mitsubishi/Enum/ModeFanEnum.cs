namespace Com.Ericmas001.Infrared.Hvac.Mitsubishi.Enum
{
    public enum ModeFanEnum : byte
    {
        //Speed1          # 0x01      0000 0001        1
        //Speed2          # 0x02      0000 0010        2
        //Speed3          # 0x03      0000 0011        3
        //Auto            # 0x80      1000 0000      128

        Speed1 = 0b0000_0001,
        Speed2 = 0b0000_0010,
        Speed3 = 0b0000_0011,
        Auto = 0b1000_0000
    }
}
