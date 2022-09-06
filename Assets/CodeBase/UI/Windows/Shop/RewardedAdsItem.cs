using CodeBase.Services.Ads;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class RewardedAdsItem : MonoBehaviour
    {
        [SerializeField] private Button _showRewardedVideo;
        [SerializeField] private GameObject[] _adsActiveObjects;
        [SerializeField] private GameObject[] _adsInactiveObjects;

        private IAdsService _adsService;
        private IPersistentProgressService _progressService;

        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            _adsService = adsService;
            _progressService = progressService;
        }

        public void Initialize()
        {
            _showRewardedVideo.onClick.AddListener(OnShowAdsClicked);

            RefreshAvailableAds();
        }

        public void Subscribe() =>
            _adsService.RewardedVideoReady += RefreshAvailableAds;

        public void Cleanup() =>
            _adsService.RewardedVideoReady -= RefreshAvailableAds;

        private void OnShowAdsClicked()
        {
            _adsService.ShowRewardedVideo(OnVideoFinished);
        }

        private void OnVideoFinished()
        {
            _progressService.Progress.WorldData.LootData.Add(_adsService.Reward);
        }

        private void RefreshAvailableAds()
        {
            bool isVideoReady = _adsService.IsRewardedVideoReady;

            foreach (GameObject adsActiveObject in _adsActiveObjects)
                adsActiveObject.SetActive(isVideoReady);

            foreach (GameObject adsInactiveObject in _adsInactiveObjects)
                adsInactiveObject.SetActive(!isVideoReady);
        }
    }
}