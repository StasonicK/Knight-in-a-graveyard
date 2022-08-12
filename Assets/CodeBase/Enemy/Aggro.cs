using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveToHero _follow;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj) => SwitchFollowOn();

        private void TriggerExit(Collider obj) => SwitchFollowOff();

        private bool SwitchFollowOff() =>
            _follow.enabled = false;

        private bool SwitchFollowOn() =>
            _follow.enabled = true;
    }
}