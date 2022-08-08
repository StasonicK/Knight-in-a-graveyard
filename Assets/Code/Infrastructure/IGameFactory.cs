using UnityEngine;

public interface IGameFactory
{
    GameObject CreateHero(GameObject at);
    void CreateHud();
}