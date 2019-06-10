using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
public static class ADManager 
{
	private static string appID="ca-app-pub-1044537920504306~1035225471";
	private static RewardBasedVideoAd video;
	private static BannerView Banner;
	public delegate void Dele();
	private static Dele OnReward;
	public static void RequestVideo()
	{
		//ID REAL: ca-app-pub-1044537920504306/7358635834
		string id ="ca-app-pub-3940256099942544/5224354917";
		video=RewardBasedVideoAd.Instance;
		//ONLY FOR RELEASE
		//AdRequest request=new AdRequest.Builder().Build();
		AdRequest request =new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
		video.OnAdRewarded +=HandleReward;
		video.LoadAd(request,id);
	}

	public static void RequestBanner()
	{
		//ID REAL: ca-app-pub-1044537920504306/4423806149
		string id ="ca-app-pub-3940256099942544/6300978111";
		Banner= new BannerView(id, AdSize.SmartBanner, AdPosition.Top);
		//ONLY FOR RELEASE
		//AdRequest request=new AdRequest.Builder().Build();
		AdRequest request =new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
		Banner.OnAdLoaded += OnLoaded;
		Banner.LoadAd(request);
	}
	public static void ShowAd(Dele d)
	{
		if(video.IsLoaded()){
			video.Show();
			OnReward=d;
		}
	}
	public static void HandleReward(object sender,Reward args)
	{
		//reward here!!!
		if(OnReward!=null)OnReward();
	}

	public static void OnLoaded(object sender, EventArgs args)
    {
        Banner.Show();
		Debug.Log("Ta funfando");
    }
	public static void CloseBanner()
	{
		Banner.Destroy();
	}
	public static void Initialize()
	{
		MobileAds.Initialize(appID);
	}
}