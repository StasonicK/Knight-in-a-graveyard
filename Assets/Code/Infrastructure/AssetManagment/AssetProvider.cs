﻿using UnityEngine;

public class AssetProvider : IAssetProvider
{
    public GameObject Instantiate(string path)
    {
        var heroPrefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(heroPrefab);
    }

    public GameObject Instantiate(string path, Vector3 at)
    {
        var heroPrefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(heroPrefab, at, Quaternion.identity);
    }
}