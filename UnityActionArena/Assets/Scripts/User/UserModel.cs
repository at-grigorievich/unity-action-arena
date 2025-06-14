using ATG.Items.Equipment;
using ATG.Items.Inventory;
using ATG.Observable;

namespace User
{
    public sealed class UserModel
    {
        public readonly int Id;
        public readonly IObservableVar<int> Currency;
        
        public readonly Inventory Inventory;
        public readonly Equipment Equipment;
        
        public string Name { get; set; }
        
        public int KillCount { get; set; }
        public int DeathCount { get; set; }

        public UserModel(int id, string name, int currency, Inventory inventory, Equipment equipment, int kill, int death)
        {
            Id = id;
            Name = name;
            Inventory = inventory;
            Equipment = equipment;
            KillCount = kill;
            DeathCount = death;

            Currency = new ObservableVar<int>(currency);
        }
    }
}