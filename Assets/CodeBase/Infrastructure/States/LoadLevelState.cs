using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.CameraLogic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        // private readonly GameStateMachine _stateMachine;
        [Inject] private readonly ISceneLoader _sceneLoader;
        [Inject] private readonly ILoadingCurtain _loadingCurtain;
        [Inject] private readonly IAssets _assets;
        [Inject] private readonly LootCreator _lootCreator;
        [Inject] private readonly GameFactory _gameFactory;
        [Inject] private readonly IPersistentProgressService _progressService;
        [Inject] private readonly IStaticDataService _staticData;
        [Inject] private readonly IUIFactory _uiFactory;

        public event Action Entered;

        // [Inject]
        public LoadLevelState(
            // GameStateMachine gameStateMachine, 
            // ISceneLoader sceneLoader, ILoadingCurtain loadingCurtain,
            // IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData,
            // IUIFactory uiFactory
        )
        {
            // _stateMachine = gameStateMachine;
            // _sceneLoader = sceneLoader;
            // _loadingCurtain = loadingCurtain;
            // _gameFactory = gameFactory;
            // _progressService = progressService;
            // _staticData = staticData;
            // _uiFactory = uiFactory;
        }

        public async void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _assets.CleanUp();
            await _assets.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitGameWorld();
            InformProgressReaders();

            Entered?.Invoke();

            // _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _assets.GetProgressReaders())
                progressReader.LoadProgress(_progressService.Progress);
        }

        private async Task InitGameWorld()
        {
            var levelData = LevelStaticData();

            await InitSpawners(levelData);
            await InitLootPieces();
            GameObject hero = await InitHero(levelData);
            // await InitLevelTransfer(levelData);
            await InitHud(hero);
            CameraFollow(hero);
        }

        private async Task InitLootPieces()
        {
            foreach (KeyValuePair<string, LootPieceData> item in _progressService.Progress.WorldData.LootData.LootPieceDataOnScene.Dictionary)
            {
                LootPiece lootPiece = await _lootCreator.CreateLoot();
                lootPiece.GetComponent<UniqueId>().Id = item.Key;
                lootPiece.Initialize(item.Value.Loot);
                lootPiece.transform.position = item.Value.Position.AsUnityVector();
            }
        }

        private async Task<GameObject> InitHero(LevelStaticData levelStaticData) =>
            await _gameFactory.CreateHero(levelStaticData.InitialHeroPosition);

        private async Task InitSpawners(LevelStaticData levelData)
        {
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
                await _gameFactory.CreateSpawner(spawnerData.Id, spawnerData.Position, spawnerData.MonsterTypeId);
        }

        // private async Task InitLevelTransfer(LevelStaticData levelData) =>
        //     await _gameFactory.CreateLevelTransfer(levelData.LevelTransfer.Position);

        private async Task InitHud(GameObject hero)
        {
            GameObject hud = await _uiFactory.CreateHud();
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        }

        private LevelStaticData LevelStaticData() =>
            _staticData.ForLevel(SceneManager.GetActiveScene().name);

        private void CameraFollow(GameObject hero) =>
            Camera.main.GetComponent<CameraFollower>().Follow(hero);
    }
}