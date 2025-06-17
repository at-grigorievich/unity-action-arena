using ATG.Character;
using ATG.User;

namespace ATG.Items.Equipment
{
    public class UserToArenaEquipmentBinder: UserToEquipmentUserBinder<PlayerPresenter>
    {
        public UserToArenaEquipmentBinder(UserPresenter userPresenter, PlayerPresenter playerPresenter) 
            : base(userPresenter, playerPresenter) { }
    }
}