using System;
using ATG.Animator;
using ATG.Attack;
using ATG.Camera;
using ATG.Input;
using ATG.Move;
using ATG.Observable;
using Settings;
using UnityEngine;
using VContainer;

namespace ATG.Character
{
    [Serializable]
    public abstract class CharacterCreator<T> where T : CharacterPresenter
    {
        [SerializeField] protected CharacterView view;
        [SerializeField] protected AnimatorWrapperCreator animatorCreator;
        
        public virtual void Create(IContainerBuilder builder)
        {
            CharacterModel model = GetModel();

            IAnimatorWrapper animatorWrapper = GetAnimator();
            IMoveableService moveService = GetMove(model.Speed);
            
            builder.Register<T>(Lifetime.Singleton)
                .WithParameter<CharacterView>(view)
                .WithParameter<CharacterModel>(model)
                .WithParameter<IAnimatorWrapper>(animatorWrapper)
                .WithParameter<IMoveableService>(moveService)
                .AsSelf()
                .AsImplementedInterfaces();
        }

        protected CharacterModel GetModel() => new CharacterModel(0, 0, 1, 3, 0);
        protected IAnimatorWrapper GetAnimator() => animatorCreator.Create();
        protected virtual IMoveableService GetMove(IReadOnlyObservableVar<float> speed) => new TransformMoveService(view.transform);
    }
    
    [Serializable]
    public abstract class ArenaCharacterCreator<T> : CharacterCreator<T> where T : CharacterPresenter
    {
        [SerializeField] protected RaycastAttackServiceCreator attackCreator;
        
        public override void Create(IContainerBuilder builder)
        {
            CharacterModel model = GetModel();

            IAnimatorWrapper animatorWrapper = GetAnimator();
            IMoveableService moveService = GetMove(model.Speed);
            IAttackService attackService = GetAttack(model.Range);
            
            builder.Register<T>(Lifetime.Singleton)
                .WithParameter<CharacterView>(view)
                .WithParameter<CharacterModel>(model)
                .WithParameter<IAnimatorWrapper>(animatorWrapper)
                .WithParameter<IMoveableService>(moveService)
                .WithParameter<IAttackService>(attackService)
                .AsSelf()
                .AsImplementedInterfaces();
        }

        protected override IMoveableService GetMove(IReadOnlyObservableVar<float> speed) =>
            new NavMeshMoveService(view.NavAgent, speed);
        
        protected IAttackService GetAttack(IReadOnlyObservableVar<float> range) => attackCreator.Create(range);
    }

    [Serializable]
    public sealed class LobbyCharacterCreator : CharacterCreator<LobbyCharacterPresenter> { }
    
    [Serializable] 
    public sealed class PlayerCharacterCreator : ArenaCharacterCreator<PlayerPresenter> { }

    [Serializable]
    public sealed class BotCharacterCreator : ArenaCharacterCreator<BotPresenter>
    {
        public BotPresenter Create(TargetNavigationPointSet navigationPointSet, IStaminaReset staminaReset)
        {
            CharacterModel model = GetModel();
            IAnimatorWrapper animator = GetAnimator();
            IMoveableService move = GetMove(model.Speed);
            IAttackService attack = GetAttack(model.Range);

            return new BotPresenter(view, model, animator, move, attack, staminaReset, navigationPointSet);
        }
    }
}