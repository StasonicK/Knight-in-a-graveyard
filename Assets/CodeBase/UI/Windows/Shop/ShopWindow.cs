using CodeBase.Services.Ads;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    class ShopWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _skullText;
        [SerializeField] private RewardedAdsItem _adsItem;

        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            base.Construct(progressService);
            _adsItem.Construct(adsService, progressService);
        }

        protected override void Initialize()
        {
            _adsItem.Initialize();
            RefreshSkullText();
        }

        protected override void SubscribeUpdates()
        {
            _adsItem.Subscribe();
            Progress.WorldData.LootData.Changed += RefreshSkullText;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            _adsItem.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshSkullText;
        }

        private void RefreshSkullText() =>
            _skullText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}