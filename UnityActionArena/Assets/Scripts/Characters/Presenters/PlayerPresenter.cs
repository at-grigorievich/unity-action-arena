using ATG.Items.Equipment;

namespace ATG.Character
{
    public sealed class PlayerPresenter: CharacterPresenter
    {
        public PlayerPresenter(CharacterView view, Equipment equipment) : base(view)
        {
        }
    }
}