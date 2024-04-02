namespace Domain.Models
{
    public enum UserRoles
    {
        Admin = 0xFFFF,
        Chief1 = 0xF000,
        Chief2 = 0x0F00,
        Chief3 = 0x00F0,
        Chief4 = 0x000F,
        Worker11 = 0x8000,
        Worker12 = 0x4000,
        Worker13 = 0x2000,
        Worker14 = 0x1000,
        Worker21 = 0x0800,
        Worker22 = 0x0400,
        Worker23 = 0x0200,
        Worker24 = 0x0100,
        Worker31 = 0x0080,
        Worker32 = 0x0040,
        Worker33 = 0x0020,
        Worker34 = 0x0010,
        Worker41 = 0x0008,
        Worker42 = 0x0004,
        Worker43 = 0x0002,
        Worker44 = 0x0001
    }
}
