using System;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        // private readonly GameStateMachine _stateMachine;
   [Inject]     private readonly IPersistentProgressService _progressService;
   [Inject]     private readonly ISaveLoadService _saveLoadService;

        public event Action Entered;

        // [Inject]
        public LoadProgressState(
            // GameStateMachine stateMachine, 
            // IPersistentProgressService progressService,
            // ISaveLoadService saveLoadService
            )
        {
            // _stateMachine = stateMachine;
            // _progressService = progressService;
            // _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            Entered?.Invoke();
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(initialLevel: "Main");
            progress.HeroState.MaxHP = 50f;
            progress.HeroStats.Damage = 1f;
            progress.HeroStats.DamageRadius = 0.5f;
            progress.HeroState.ResetHP();
            return progress;
        }
    }
}