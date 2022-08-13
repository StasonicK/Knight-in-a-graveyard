using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown = 3f;

        private IGameFactory _factory;
        private Transform _heroTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _factory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void UpdateCooldown()
        {
            if (!CooldownUp())
                _attackCooldown -= Time.deltaTime;
        }


        private void OnHeroCreated() => _heroTransform = _factory.HeroGameObject.transform;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private void OnAttack()
        {
        }

        private void OnAttackEnded()
        {
            _currentAttackCooldown = _attackCooldown;
            _isAttacking = false;
        }

        private bool CooldownUp() => _attackCooldown <= 0;

        private bool CanAttack()
        {
            return !_isAttacking && CooldownUp();
        }
    }
}