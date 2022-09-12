using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private GameObject _deathVFX;

        public event Action Died;

        private void Start() =>
            _health.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _health.HealthChanged -= HealthChanged;
            _animator.PlayDeath();
            SpawnDeathVfx();
            StartCoroutine(DestroyTimer());
            Died?.Invoke();
        }

        private void SpawnDeathVfx() =>
            Instantiate(_deathVFX, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}