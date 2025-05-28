using System;

namespace ATG.Equipment
{
    [Flags]
    public enum EquipmentTag : byte
    {
        NONE = 0,
        HEAD_ARMOR = 1 << 0,
        BODY_ARMOR = 1 << 1,
        BOOTS_ARMOR = 1 << 2,
        LEFT_ARM = 1 << 3,
        RIGHT_ARM = 1 << 4
    }
}