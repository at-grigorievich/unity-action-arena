using System;
using ATG.Animator;
using ATG.Attack;
using ATG.Move;
using ATG.Observable;
using ATG.Stamina;
using ATG.UI;
using UnityEngine;
using VContainer;

namespace ATG.Character
{
    [Serializable]
    public abstract class CharacterCreator<T> where T : CharacterPresenter
    {
        [SerializeField] protected ArenaCharacterView view;
        [SerializeField] protected AnimatorWrapperCreator animatorCreator;
        
        public virtual void Create(IContainerBuilder builder)
        {
            CharacterModel model = GetModel();

            IAnimatorWrapper animatorWrapper = GetAnimator();
            IMoveableService moveService = GetMove(model.Speed);
            
            builder.Register<T>(Lifetime.Singleton)
                .WithParameter<ArenaCharacterView>(view)
                .WithParameter<CharacterModel>(model)
                .WithParameter<IAnimatorWrapper>(animatorWrapper)
                .WithParameter<IMoveableService>(moveService)
                .AsSelf()
                .AsImplementedInterfaces();
        }

        protected virtual CharacterModel GetModel() => new CharacterModel(string.Empty,0, 100, 0, 3, 0);
        protected IAnimatorWrapper GetAnimator() => animatorCreator.Create();
        protected virtual IMoveableService GetMove(IReadOnlyObservableVar<float> speed) => new TransformMoveService(view.transform);
    }
    
    [Serializable]
    public abstract class ArenaCharacterCreator<T> : CharacterCreator<T> where T : CharacterPresenter
    {
        [SerializeField] protected RaycastAttackServiceCreator attackCreator;
        [SerializeField] protected AutoResetStaminaServiceCreator staminaCreator;
        
        public override void Create(IContainerBuilder builder)
        {
            CharacterModel model = GetModel();

            IAnimatorWrapper animatorWrapper = GetAnimator();
            IMoveableService moveService = GetMove(model.Speed);
            IAttackService attackService = GetAttack(model.Range);
            IStaminaService staminaService = GetStamina(model.Stamina);
            
            builder.Register<T>(Lifetime.Singleton)
                .WithParameter<ArenaCharacterView>(view)
                .WithParameter<CharacterModel>(model)
                .WithParameter<IAnimatorWrapper>(animatorWrapper)
                .WithParameter<IMoveableService>(moveService)
                .WithParameter<IAttackService>(attackService)
                .WithParameter<IStaminaService>(staminaService)
                .AsSelf()
                .AsImplementedInterfaces();
        }

        protected override IMoveableService GetMove(IReadOnlyObservableVar<float> speed) =>
            new NavMeshMoveService(view.NavAgent, speed);
        
        protected IAttackService GetAttack(IReadOnlyObservableVar<float> range) => attackCreator.Create(range);
        protected IStaminaService GetStamina(IObservableVar<float> stamina) => staminaCreator.Create(stamina);
    }
    
    [Serializable]
    public sealed class PlayerCharacterCreator : ArenaCharacterCreator<PlayerPresenter>
    {
        protected override CharacterModel GetModel()
        {
            string name = "BIG_BOSS";
            
            return new CharacterModel(name,0, 100, 0, 3, 0);
        }
    }

    [Serializable]
    public sealed class BotCharacterCreator : ArenaCharacterCreator<BotPresenter>
    {
        [SerializeField] private ArenaBotUIView uiView;
        
        public BotPresenter Create(TargetNavigationPointSet navigationPointSet)
        {
            CharacterModel model = GetModel();
            IAnimatorWrapper animator = GetAnimator();
            IMoveableService move = GetMove(model.Speed);
            IAttackService attack = GetAttack(model.Range);
            IStaminaService stamina = GetStamina(model.Stamina);

            return new BotPresenter(view, uiView, model, animator, move, attack, stamina, navigationPointSet);
        }

        protected override CharacterModel GetModel()
        {
            string viewHash = Mathf.Abs(view.GetHashCode()).ToString()[..3];
            string name = $"Bot_{viewHash}";
            return new CharacterModel(name,0, 100, 0, 3, 0);
        }
    }
}