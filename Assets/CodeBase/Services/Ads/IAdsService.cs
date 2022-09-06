using System;

namespace CodeBase.Services.Ads
{
    public interface IAdsService : IService
    {
        event Action RewardedVideoReady;
        void Initialize();
        void ShowRewardedVideo(Action onVideoFinished);
        bool IsRewardedVideoReady();
    }
}