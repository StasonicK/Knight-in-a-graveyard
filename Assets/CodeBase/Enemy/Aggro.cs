using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver TriggerObserver;
        [SerializeField] private Follow _follow;
        [SerializeField] private float _cooldown;

        private bool _hasAggroTarget;
        private WaitForSeconds _switchFollowOffAfterCooldown;
        private Coroutine _aggroCoroutine;

        private void Start()
        {
            _switchFollowOffAfterCooldown = new WaitForSeconds(_cooldown);

            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            _follow.enabled = false;
        }

        private void OnDestroy()
        {
            TriggerObserver.TriggerEnter -= TriggerEnter;
            TriggerObserver.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            if (_hasAggroTarget) return;

            StopAggroCoroutine();

            SwitchFollowOn();
        }

        private void TriggerExit(Collider obj)
        {
            if (!_hasAggroTarget) return;

            _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine == null) return;

            StopCoroutine(_aggroCoroutine);
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return _switchFollowOffAfterCooldown;

            SwitchFollowOff();
        }

        private void SwitchFollowOn()
        {
            _hasAggroTarget = true;
            _follow.enabled = true;
        }

        private void SwitchFollowOff()
        {
            _follow.enabled = false;
            _hasAggroTarget = false;
        }
    }
}