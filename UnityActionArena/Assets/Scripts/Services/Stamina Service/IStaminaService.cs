using VContainer.Unity;

namespace ATG.Stamina
{
    public interface IStaminaService: ITickable
    {
        int Current { get; }
        bool IsEnough { get; }
        float Rate { get; }

        int DefaultReduceAmount { get; }
        
        void Initialize();
        
        void Reduce(int reduceAmount);
        void Reset();
    }
}