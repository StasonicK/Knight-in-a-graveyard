using System;
using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private GameObject _skull;
        [SerializeField] private GameObject _pickupVfxPrefab;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickupPopup;

        private WorldData _worldData;
        private Loot _loot;

        private const float DelayBeforeDestroying = 1.5f;

        private string _id;
        private bool _pickedUp;

        public event Action Picked;

        private void Start() =>
            _id = GetComponent<UniqueId>().Id;

        public void Construct(WorldData worldData) =>
            _worldData = worldData;

        public void Initialize(Loot loot) =>
            _loot = loot;

        private void OnTriggerEnter(Collider other)
        {
            if (!_pickedUp)
            {
                _pickedUp = true;
                Pickup();
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_pickedUp)
                return;

            LootPieceDataDictionary lootPieceDataOnScene = progress.WorldData.LootData.LootPieceDataOnScene;

            if (!lootPieceDataOnScene.Dictionary.ContainsKey(_id))
                lootPieceDataOnScene.Dictionary
                    .Add(_id, new LootPieceData(transform.position.AsVectorData(), _loot));
        }

        private void Pickup()
        {
            UpdateWorldData();
            HideSkull();
            PlayPickupVfx();
            ShowText();

            Destroy(gameObject, DelayBeforeDestroying);
        }

        private void UpdateWorldData()
        {
            UpdateCollectedLootAmount();
            RemoveLootPieceFromSavedPieces();
        }

        private void RemoveLootPieceFromSavedPieces()
        {
            LootPieceDataDictionary savedLootPieces = _worldData.LootData.LootPieceDataOnScene;

            if (savedLootPieces.Dictionary.ContainsKey(_id))
                savedLootPieces.Dictionary.Remove(_id);
        }

        private void UpdateCollectedLootAmount() =>
            _worldData.LootData.Collect(_loot);

        private void HideSkull() =>
            _skull.SetActive(false);


        private void PlayPickupVfx() =>
            Instantiate(_pickupVfxPrefab, transform.position, Quaternion.identity);

        private void ShowText()
        {
            _lootText.text = $"{_loot.Value}";
            _pickupPopup.SetActive(true);
        }
    }
}