using UnityEngine;

public interface IGameFactory : IService
{
    GameObject CreateHero(GameObject at);
    void CreateHud();
}