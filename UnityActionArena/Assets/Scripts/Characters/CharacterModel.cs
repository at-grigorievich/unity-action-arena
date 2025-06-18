using System;
using ATG.Observable;

namespace ATG.Character
{
    public class CharacterModel: IDisposable
    {
        public readonly string Name;
        public readonly IObservableVar<int> Health;
        public readonly IObservableVar<float> Stamina;
        public readonly IObservableVar<int> Damage;
        public readonly IObservableVar<float> Speed;
        public readonly IObservableVar<float> Range;

        public CharacterModel(string name, int baseHealth, int baseStamina, int baseDamage, float baseSpeed, float baseRange)
        {
            Name = name;
            Health = new ObservableVar<int>(baseHealth);
            Stamina = new ObservableVar<float>(baseStamina);
            Damage = new ObservableVar<int>(baseDamage);
            Speed = new ObservableVar<float>(baseSpeed);
            Range = new ObservableVar<float>(baseRange);
        }

        public void Dispose()
        {
            Health.Dispose();
            Stamina.Dispose();
            Damage.Dispose();
            Speed.Dispose();
            Range.Dispose();
        }

        public float GetRate() =>
            1.0f;
    }
}