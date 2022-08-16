using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private IHealth _heroHealth;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void OnDestroy()
        {
            _heroHealth.HealthChanged -= UpdateHpBar;
        }

        public void Construct(IHealth health)
        {
            _heroHealth = health;

            _heroHealth.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
        }
    }
}