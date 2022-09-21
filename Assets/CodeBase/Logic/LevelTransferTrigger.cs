using CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        [SerializeField] private string _transferTo;

        private const string Player = "Player";

        private GameStateMachine _stateMachine;
        private bool _triggered;

        // [Inject]
        public void Construct(GameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

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