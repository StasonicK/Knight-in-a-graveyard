using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.IAP;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItemsContainer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _shopUnavailableObjects;
        [SerializeField] private Transform _parent;

        private const string ShopItemPath = "ShopItem";

        private IIAPService _iapService;
        private IPersistentProgressService _progressService;
        private IAssets _assets;
        private List<GameObject> _shopItems;

        public void Construct(IIAPService iapService, IPersistentProgressService progressService, IAssets assets)
        {
            _iapService = iapService;
            _progressService = progressService;
            _assets = assets;
        }

        public void Initialize() =>
            RefreshAvailableItems();

        public void Subscribe()
        {
            _iapService.Initialized += RefreshAvailableItems;
            _progressService.Progress.PurchaseData.Changed += RefreshAvailableItems;
        }

        public void Cleanup()
        {
            _iapService.Initialized -= RefreshAvailableItems;
            _progressService.Progress.PurchaseData.Changed -= RefreshAvailableItems;
        }

        private async void RefreshAvailableItems()
        {
            UpdateUnavailableObjects();

            if (_iapService.IsInitialized)
                return;

            foreach (GameObject shopItem in _shopItems)
                Destroy(shopItem);

            foreach (ProductDescription productDescription in _iapService.Products())
            {
                GameObject shopItemObject = await _assets.Instantiate(ShopItemPath, _parent);
                ShopItem shopItem = shopItemObject.GetComponent<ShopItem>();

                _shopItems.Add(shopItem.gameObject);
            }
        }

        private void UpdateUnavailableObjects()
        {
            foreach (GameObject shopUnavailableObject in _shopUnavailableObjects)
                shopUnavailableObject.SetActive(!_iapService.IsInitialized);
        }
    }

    public class ShopItem : MonoBehaviour
    {
    }
}