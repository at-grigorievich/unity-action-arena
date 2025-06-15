using System;
using ATG.Character;
using ATG.OtusHW.Inventory;
using UnityEngine;

namespace ATG.Items
{
    public interface IItemComponent
    {
        IItemComponent Clone();
    }

    [Serializable]
    public class StackableItemComponent: IItemComponent
    {
        public int Count;
        public int MaxCount;
        
        public IItemComponent Clone()
        {
            return new StackableItemComponent() { Count = 1, MaxCount = MaxCount };
        }
    }
    
    public abstract class HeroEffectComponent : IItemComponent
    {
        public const float MAX_VALUE = 100f;

        public abstract string EffectName { get; }
        public abstract float CurrentValue { get; }
        public float Rate => CurrentValue / MAX_VALUE;
        
        public abstract IItemComponent Clone();
        public abstract void AddEffect(CharacterModel hero);
        public abstract void RemoveEffect(CharacterModel hero);
    }
    
    [Serializable]
    public class HeroDamageEffectComponent : HeroEffectComponent
    {
        [Range(0f, 100f)] public int DamageEffect = 2;

        public override string EffectName => "Damage";
        public override float CurrentValue => DamageEffect;

        public override IItemComponent Clone()
        {
            return new HeroDamageEffectComponent()
            {
                DamageEffect = this.DamageEffect
            };
        }

        public override void AddEffect(CharacterModel hero)
        {
            hero.Damage.Value += DamageEffect;
        }

        public override void RemoveEffect(CharacterModel hero)
        {
            hero.Damage.Value -= DamageEffect;
        }
    }
    
    [Serializable]
    public class HeroHealthEffectComponent : HeroEffectComponent
    {
        [Range(0f, 100f)] public int HitPointsEffect = 2;

        public override string EffectName => "Health";
        public override float CurrentValue => HitPointsEffect;

        public override IItemComponent Clone()
        {
            return new HeroHealthEffectComponent()
            {
                HitPointsEffect = this.HitPointsEffect
            };
        }

        public override void AddEffect(CharacterModel hero)
        {
            hero.Health.Value += HitPointsEffect;
        }

        public override void RemoveEffect(CharacterModel hero)
        {
            hero.Health.Value -= HitPointsEffect;
        }
    }
    
    [Serializable]
    public class HeroSpeedEffectComponent : HeroEffectComponent
    {
        [Range(0f, 100f)] public int SpeedEffect = 2;

        public override string EffectName => "Speed";
        public override float CurrentValue => SpeedEffect;
        
        public override IItemComponent Clone()
        {
            return new HeroSpeedEffectComponent()
            {
                SpeedEffect = this.SpeedEffect
            };
        }

        public override void AddEffect(CharacterModel hero)
        {
            hero.Speed.Value += SpeedEffect;
        }

        public override void RemoveEffect(CharacterModel hero)
        {
            hero.Speed.Value -= SpeedEffect;
        }
    }
    
    [Serializable]
    public class HeroRangeEffectComponent : HeroEffectComponent
    {
        [Range(0f, 100f)] public float RangeEffect = 5;

        public override string EffectName => "Range";
        public override float CurrentValue => RangeEffect;

        public override IItemComponent Clone()
        {
            return new HeroRangeEffectComponent()
            {
                RangeEffect= this.RangeEffect
            };
        }

        public override void AddEffect(CharacterModel hero)
        {
            hero.Range.Value += RangeEffect;
        }

        public override void RemoveEffect(CharacterModel hero)
        {
            hero.Range.Value -= RangeEffect;
        }  
    }
    
    [Serializable]
    public class HeroStaminaEffectComponent : HeroEffectComponent
    {
        public float StaminaEffect = 100;

        public override string EffectName => "Stamina";
        public override float CurrentValue => StaminaEffect;

        public override IItemComponent Clone()
        {
            return new HeroStaminaEffectComponent()
            {
                StaminaEffect = this.StaminaEffect
            };
        }

        public override void AddEffect(CharacterModel hero)
        {
            hero.Stamina.Value += StaminaEffect;
        }

        public override void RemoveEffect(CharacterModel hero)
        {
            hero.Stamina.Value -= StaminaEffect;
        }  
    }

    [Serializable]
    public class HeroEquipmentComponent : IItemComponent
    {
        public EquipType Type;
        public Mesh Mesh;
        public Material Material;
        
        public IItemComponent Clone()
        {
            return new HeroEquipmentComponent()
            {
                Type = this.Type,
                Mesh = this.Mesh,
                Material = this.Material
            };
        }
    }
}