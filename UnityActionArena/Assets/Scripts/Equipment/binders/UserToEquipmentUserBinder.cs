using ATG.User;

namespace ATG.Items.Equipment
{
    public abstract class UserToEquipmentUserBinder<T> where T: IUseEquipment
    {
        protected readonly UserPresenter _userPresenter;
        protected readonly T _equipmentUser;

        public UserToEquipmentUserBinder(UserPresenter userPresenter, T equipmentUser)
        {
            _userPresenter = userPresenter;
            _equipmentUser = equipmentUser;
        }
        
        public virtual void Execute()
        {
            foreach (var item in _userPresenter.Equipment.Items.Values)
            {
                OnItemTakeOn(item);
            }
        }

        protected void OnItemTakeOn(Item obj)
        {
            _equipmentUser.TakeOnEquipments(obj);
        }
    }
}