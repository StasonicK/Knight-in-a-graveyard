using System.Collections;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private GameObject _skull;
        [SerializeField] private GameObject _pickupVfxPrefab;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickupPopup;

        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;

        public void Construct(WorldData worldData) =>
            _worldData = worldData;

        public void Initialize(Loot loot) =>
            _loot = loot;

        private void OnTriggerEnter(Collider other) =>
            Pickup();

        private void Pickup()
        {
            if (_picked)
                return;

            _picked = true;

            UpdateWorldData();
            HideSkull();
            PlayPickupVfx();
            ShowText();
            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldData()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void HideSkull()
        {
            _skull.SetActive(false);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }

        private void PlayPickupVfx()
        {
            Instantiate(_pickupVfxPrefab, transform.position, Quaternion.identity);
        }

        private void ShowText()
        {
            _lootText.text = $"{_loot.Value}";
            _pickupPopup.SetActive(true);
        }
    }
}