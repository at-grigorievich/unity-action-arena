using ATG.Observable;
using VContainer.Unity;

namespace ATG.Stamina
{
    public interface IStaminaRate
    {
        IReadOnlyObservableVar<float> Current { get; }
        float Rate { get; }
        bool IsEnough { get; }
    }
    
    public interface IStaminaService: ITickable, IStaminaRate
    {
        int DefaultReduceAmount { get; }
        
        void Initialize();
        
        void Reduce(int reduceAmount);
        void Reset();
    }
}