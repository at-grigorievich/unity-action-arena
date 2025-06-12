using System;
using ATG.Observable;

namespace ATG.Character
{
    public class CharacterModel: IDisposable
    {
        public readonly IObservableVar<int> Health;
        public readonly IObservableVar<float> Stamina;
        public readonly IObservableVar<int> Damage;
        public readonly IObservableVar<float> Speed;
        public readonly IObservableVar<float> Range;

        public CharacterModel(int baseHealth, int baseStamina, int baseDamage, float baseSpeed, float baseRange)
        {
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
            (Health.Value + Stamina.Value + Damage.Value + Range.Value + Speed.Value) / 5.0f;
    }
}