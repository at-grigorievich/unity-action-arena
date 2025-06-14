using ATG.Items.Equipment;
using ATG.Items.Inventory;
using ATG.Observable;

namespace ATG.User
{
    public sealed class UserModel
    {
        public int Id;
        public string Name;
        
        public readonly IObservableVar<int> Currency;
        
        public readonly Inventory Inventory;
        public readonly Equipment Equipment;
        
        public int KillCount { get; set; }
        public int DeathCount { get; set; }

        public UserModel()
        {
            Id = -1;
            Name = "player1";
            Inventory = new Inventory();
            Equipment = new Equipment();
            KillCount = 0;
            DeathCount = 0;

            Currency = new ObservableVar<int>(0);
        }
    }
}