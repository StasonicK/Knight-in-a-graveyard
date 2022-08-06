using System;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private Game _game;
    
    private void Awake()
    {
        _game = new Game();
        
        DontDestroyOnLoad(this);
    }
}
