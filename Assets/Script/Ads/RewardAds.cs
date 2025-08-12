using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

public class RewardAds : MonoBehaviour
{
#if UNITY_ANDROID
    private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/5224354917"; // ID test Rewarded
#elif UNITY_IPHONE
    private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/1712485313";
#else
    private const string AD_UNIT_ID = "unused";
#endif

    private RewardedAd rewardedAd;

    void Start()
    {
        // Khởi tạo AdMob SDK
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob initialized");
            LoadAd(); // Tải quảng cáo ngay khi khởi động
        });
    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.H))
    //    {
    //        ShowAd();
    //    }    
    //}

    /// <summary>
    /// Tải quảng cáo Rewarded
    /// </summary>
    public void LoadAd()
    {
        Debug.Log("Loading rewarded ad...");

        AdRequest adRequest = new AdRequest();
        RewardedAd.Load(AD_UNIT_ID, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.LogError("Failed to load ad: " + error);
                return;
            }

            rewardedAd = ad;
            Debug.Log("Rewarded ad loaded successfully");

            ListenToAdEvents(rewardedAd);
        });
    }



    /// <summary>
    /// Hiển thị quảng cáo nếu đã sẵn sàng
    /// </summary>
    public void ShowAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log($"User earned reward: {reward.Amount} {reward.Type}");
                // TODO: Cộng thưởng cho người chơi ở đây (vàng, item, v.v.)
            });
        }
        else
        {
            Debug.Log("Ad not ready yet, loading again...");
            LoadAd();
        }
    }

    /// <summary>
    /// Lắng nghe các sự kiện của quảng cáo
    /// </summary>
    private void ListenToAdEvents(RewardedAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log($"Ad paid: {adValue.Value} micros ({adValue.CurrencyCode})");
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Ad impression recorded");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("Ad clicked");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Ad opened full screen content");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Ad closed full screen content");
            LoadAd(); // Tự động load lại sau khi đóng
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Ad failed to open full screen content: " + error);
        };
    }

    /// <summary>
    /// Huỷ quảng cáo khi không dùng
    /// </summary>
    private void OnDestroy()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }
    }
}
