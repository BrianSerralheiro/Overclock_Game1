	using System;
	using System.Collections.Generic;
	using GoogleMobileAds.Api;
	using UnityEngine;
	namespace ADs{
	public static class adsManager 
	{
		private static string appID="ca-app-pub-1044537920504306~1035225471";
		private static RewardBasedVideoAd video;
		private static BannerView Banner;
		public delegate void Dele();
		private static Dele OnReward;
		public static void RequestVideo()
		{
			/*video=RewardBasedVideoAd.Instance;
			video.OnAdFailedToLoad += OnLoadFailed;
			video.OnAdRewarded +=HandleReward;
			video.OnAdLoaded += OnLoadVideo;*/
			//ID REAL: ca-app-pub-1044537920504306/7358635834
			string id ="ca-app-pub-3940256099942544/5224354917";
			//video=RewardBasedVideoAd.Instance;
			//ONLY FOR RELEASE
			//AdRequest request=new AdRequest.Builder().Build();
			AdRequest request = new AdRequest.Builder().AddTestDevice("351862082077759").Build();
			//video.OnAdRewarded +=HandleReward;
			video.LoadAd(request,id);
		}

		public static void RequestBanner()
		{
			if(Banner != null)
			{
				Banner.Destroy();
			}
			//ID REAL: ca-app-pub-1044537920504306/4423806149
			string id ="ca-app-pub-3940256099942544/6300978111";
			Banner= new BannerView(id, AdSize.SmartBanner, AdPosition.Top);
			//ONLY FOR RELEASE
			//AdRequest request=new AdRequest.Builder().Build();
			AdRequest request =new AdRequest.Builder().AddTestDevice("351862082077759").Build();
			Banner.LoadAd(request);
		}
		public static bool LoadedVideo()
		{
			return video!=null && video.IsLoaded();
		}
		public static bool ShowAd(Dele d)
		{
			if(LoadedVideo())
			{
				video.Show();
				OnReward=d;
				testWarning.Open("Showing");
				return true;
			}
			return false;
		}
		public static void HandleReward(object sender,Reward args)
		{
			//reward here!!!
			SoundManager.PlayEffects(1);
			if(OnReward!=null){
				OnReward();
					testWarning.Open("reward given");
			}
			else testWarning.Open("reward empty");
			Application.Quit();
		}

		public static void ShowBanner()
		{
			Banner.Show();
		}
		public static void CloseBanner()
		{
			Banner.Destroy();
		}
		public static void Initialize()
		{
			MobileAds.Initialize(appID);
			video=RewardBasedVideoAd.Instance;
			video.OnAdCompleted += OnAdCompleted;
			video.OnAdFailedToLoad += OnLoadFailed;
			video.OnAdRewarded +=HandleReward;
			video.OnAdLoaded += OnLoadVideo;
		}
		private static void OnLoadVideo (object o, EventArgs a)
		{
			testWarning.Open("video loaded");
		}

		private static void OnAdCompleted (object o, EventArgs a)
		{
			testWarning.Open("video ended");
		}

		private static void OnLoadFailed(object o, AdFailedToLoadEventArgs a)
		{
			testWarning.Open(o.ToString()+":"+a.Message);
		}

		} }