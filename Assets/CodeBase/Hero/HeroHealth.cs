using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private HeroAnimator _animator;

        private State _state;

        public float Current
        {
            get => _state.CurrentHP;
            set => _state.CurrentHP = value;
        }

        public float Max
        {
            get => _state.MaxHP;
            set
            {
                if (_state.CurrentHP != value)
                {
                    _state.MaxHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public Action HealthChanged;

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHP = Current;
            progress.HeroState.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            _animator.PlayHit();
        }
    }
}