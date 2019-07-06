	using System;
	using System.Collections.Generic;
	using GoogleMobileAds.Api;
	using UnityEngine;
	namespace ADs{
	public static class adsManager 
	{
		private static string appID="ca-app-pub-1044537920504306~1035225471";
		private static RewardBasedVideoAd video;

		public delegate void Dele();
		private static bool revive;
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

		public static bool LoadedVideo()
		{
			return video!=null && video.IsLoaded();
		}
		public static void ShowAd(bool d)
		{
			if(LoadedVideo())
			{
				video.Show();
				revive=d;
			}
		}
		public static void HandleReward(object sender,Reward reward)
		{
			RequestVideo();
			if(revive)
			{
				EnemyBase.player.GetComponent<Ship>().Revive();
			}
			else {
				Cash.totalCash += 20;
				Warning.Open("Received 20 stars!");
			}
		}

		public static void Initialize()
		{
			MobileAds.Initialize(appID);
			video=RewardBasedVideoAd.Instance;
			video.OnAdCompleted += OnAdCompleted;
			video.OnAdFailedToLoad += OnLoadFailed;
			video.OnAdRewarded += HandleReward;
			video.OnAdLoaded += OnLoadVideo;
		}
		private static void OnLoadVideo (object o, EventArgs a)
		{
		}

		private static void OnAdCompleted (object o, EventArgs a)
		{

		}

		private static void OnLoadFailed(object o, AdFailedToLoadEventArgs a)
		{
			Warning.Open(o.ToString()+":"+a.Message);
		}

		} }