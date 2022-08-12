using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveToHero _follow;
        [SerializeField] private float _cooldown;

        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;

                StopAggroCoroutine();

                SwitchFollowOn();
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private bool SwitchFollowOff() =>
            _follow.enabled = false;

        private bool SwitchFollowOn() =>
            _follow.enabled = true;
    }
}