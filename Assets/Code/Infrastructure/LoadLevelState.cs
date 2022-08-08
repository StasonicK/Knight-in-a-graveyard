﻿using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private const string Initialpoint = "InitialPoint";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
    }

    public void Enter(string sceneName)
    {
        _loadingCurtain.Show();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() => _loadingCurtain.Hide();

    private void OnLoaded()
    {
        GameObject hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(Initialpoint));

        _gameFactory.CreateHud();

        CameraFollowing(hero);

        _stateMachine.Enter<GameLoopState>();
    }

    private void CameraFollowing(GameObject hero) => Camera.main.GetComponent<CameraFollowing>().Follow(hero);
}