using ATG.Equipment;

namespace ATG.Progression
{
    public readonly struct GameplayProgressionData
    {
        public readonly string UserName;
        public readonly EquipmentConfig HeadArmor;
        public readonly EquipmentConfig BodyArmor;
        public readonly EquipmentConfig LegsArmos;
        public readonly EquipmentConfig Arm;

        public GameplayProgressionData(string userName, EquipmentConfig head, EquipmentConfig body, 
            EquipmentConfig legs, EquipmentConfig arm)
        {
            UserName = userName;
            HeadArmor = head;
            BodyArmor = body;
            LegsArmos = legs;
            Arm = arm;
        }
    }
}