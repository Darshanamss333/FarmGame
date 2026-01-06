/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ApplovinAds : MonoBehaviour
    {
        public static ApplovinAds _Instance
        {
            get
            {
                if (ApplovinAds.LocalInstance == null)
                {
                    GameObject _new = new GameObject();
                    _new.name = "ApplovinAds";
                    ApplovinAds _Instance = _new.AddComponent<ApplovinAds>();
                    return _Instance;
                }
                else
                {
                    return ApplovinAds.LocalInstance;
                }

            }
        }

        public static ApplovinAds LocalInstance;

        private void Awake()
        {
            if (!LocalInstance)
            {
                LocalInstance = this;
                Init();
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        void Init()
        {
            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
                // AppLovin SDK is initialized, start loading ads
                InitializeTopBannerAds();
                ShowTopBannerAds();

                //InitializeBottomBannerAds();
                //ShowBannerAds2();

                //LoadRewardedAd();
                LoadInterstitialAd();
            };

            MaxSdk.SetSdkKey("L2eosqfQ_klSMoXpfXOpMOTZRqQ0AvAVxpIncyCbfy31karO7DCTbl22ZT2kMCEH0X3cTFD8BH9ws-I-BUSoEU");
            MaxSdk.SetUserId("USER_ID");
            MaxSdk.InitializeSdk();
            //tang = 50;
        }

        //-------------------------------------------------------------
        string TopbannerAdUnitId = "c7af5a54286c351b"; // Retrieve the ID from your account
        void InitializeTopBannerAds()
        {
            MaxSdk.CreateBanner(TopbannerAdUnitId, MaxSdkBase.BannerPosition.TopCenter);
            MaxSdk.SetBannerBackgroundColor(TopbannerAdUnitId, Color.white);
        }

        bool _topbaneer;
        public void ShowTopBannerAds()
        {
            MaxSdk.ShowBanner(TopbannerAdUnitId);
            _topbaneer = true;
        }

        public void HideTopBannerAds()
        {
            MaxSdk.HideBanner(TopbannerAdUnitId);
            _topbaneer = false;
        }

        //-----------------------------------------------------------
        string BotombannerAdUnitId = "6535c3d60683a999"; // Retrieve the ID from your account
        void InitializeBottomBannerAds()
        {
            MaxSdk.CreateBanner(BotombannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
            MaxSdk.SetBannerBackgroundColor(BotombannerAdUnitId, Color.white);
        }

        public void ShowBottomBannerAds()
        {
            MaxSdk.ShowBanner(BotombannerAdUnitId);
        }

        public void HideBottomBannerAds()
        {
            MaxSdk.HideBanner(BotombannerAdUnitId);
        }



        //-----------------------------------------------------------
        string rewardedAdUnitId = "3c83544b1e3c3c59";
        void LoadRewardedAd()
        {
            MaxSdk.LoadRewardedAd(rewardedAdUnitId);
        }

        public void ShowRewardedAds()
        {
            MaxSdk.ShowRewardedAd(rewardedAdUnitId);
        }

        public bool IsRewardedLoad()
        {
            if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //-----------------------------------------------------------
        string intestitialId
        {
            get
            {
                if(Application.platform == RuntimePlatform.Android)
                {
                    return "ce08b048322da5b7";
                }
                else
                {
                    return "547cebf09bd66e1a";
                }
   
            }
        }
        void LoadInterstitialAd()
        {
            MaxSdk.LoadInterstitial(intestitialId);
        }

        public void ShowInterstitialAds()
        {
            if (MaxSdk.IsInterstitialReady(intestitialId))
            {
                MaxSdk.ShowInterstitial(intestitialId);
                LoadInterstitialAd();
            }
        }


        int FaildCount = 0;
        public void ShowIfValid()
        {
            FaildCount += 1;

            if(FaildCount > 2)
            {
                ShowInterstitialAds();
                FaildCount = 0;
            }
        }

        public bool IsInterstitialLoad()
        {
            if (MaxSdk.IsInterstitialReady(intestitialId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool Show;
        private void Update()
        {
            
            if(Show == false && IsInterstitialLoad() && GameManager._Instance._Data._RemoveAds == false)
            {
                ShowInterstitialAds();
                Show = true;
            }
            
            Reload();
            bannerUpdate();
        }
        

        void bannerUpdate()
        {
            if(GameManager._Instance._Data._RemoveAds && _topbaneer)
            {
                HideTopBannerAds();
            }
        }


        void Reload()
        {
            if(IsRewardedLoad() == false)
            {
                //LoadRewardedAd();
            }

            if(IsInterstitialLoad() == false)
            {
                LoadInterstitialAd();
            }
        }
    }

}
*/