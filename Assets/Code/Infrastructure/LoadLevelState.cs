using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private const string Initialpoint = "InitialPoint";
    private const string HeroPath = "Hero/Hero";
    private const string HudPath = "Hud/Hud";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;

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
        var initialPoint = GameObject.FindWithTag(Initialpoint);

        GameObject hero = Instantiate(HeroPath, at: initialPoint.transform.position);
        Instantiate(HudPath);

        CameraFollowing(hero);

        _stateMachine.Enter<GameLoopState>();
    }

    private static GameObject Instantiate(string path)
    {
        var heroPrefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(heroPrefab);
    }

    private static GameObject Instantiate(string path, Vector3 at)
    {
        var heroPrefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(heroPrefab, at, Quaternion.identity);
    }

    private void CameraFollowing(GameObject hero) => Camera.main.GetComponent<CameraFollowing>().Follow(hero);
}