using System;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        [SerializeField] private string _transferTo;

        private const string Player = "Player";

        private IGameStateMachine _stateMachine;

        private bool _triggered;

        private void Awake() =>
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();

        private void OnTriggerEnter(Collider other)
        {
            if (_triggered)
                return;

            if (other.CompareTag(Player))
            {
                _stateMachine.Enter<LoadLevelState, string>(_transferTo);
                _triggered = true;
            }
        }
    }
}