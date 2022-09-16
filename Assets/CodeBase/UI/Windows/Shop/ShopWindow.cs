using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Ads;
using CodeBase.Services.IAP;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    class ShopWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _skullText;
        [SerializeField] private RewardedAdsItem _adsItem;
        [SerializeField] private ShopItemsContainer _shopItemsContainer;

        public void Construct(IAdsService adsService, IPersistentProgressService progressService, IIAPService iapService, IAssets assets)
        {
            base.Construct(progressService);
            _adsItem.Construct(adsService, progressService);
            _shopItemsContainer.Construct(iapService, progressService, assets);
        }

        protected override void Initialize()
        {
            _adsItem.Initialize();
            _shopItemsContainer.Initialize();
            RefreshSkullText();
        }

        protected override void SubscribeUpdates()
        {
            _adsItem.Subscribe();
            _shopItemsContainer.Subscribe();
            Progress.WorldData.LootData.Changed += RefreshSkullText;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            _adsItem.Cleanup();
            _shopItemsContainer.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshSkullText;
        }

        private void RefreshSkullText() =>
            _skullText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}