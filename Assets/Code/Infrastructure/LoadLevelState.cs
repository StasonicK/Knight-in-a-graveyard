using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private const string Initialpoint = "InitialPoint";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter(string sceneName) => _sceneLoader.Load(sceneName, OnLoaded);

    public void Exit()
    {
    }

    private void OnLoaded()
    {
        var initialPoint = GameObject.FindWithTag(Initialpoint);

        GameObject hero = Instantiate("Hero/Hero", at: initialPoint.transform.position);
        Instantiate("Hud/Hud");

        CameraFollowing(hero);
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