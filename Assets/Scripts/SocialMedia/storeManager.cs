using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADs;

public class storeManager : MonoBehaviour 
{


	// Use this for initialization
	void OnEnable () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void buyStars(int i)
	{

	}

	public void inviteFriends()
	{

	}

	public void share()
	{
		Application.OpenURL("https://www.facebook.com/sharer/sharer?u=www.facebook.com/OverclockEntretenimento");
	}

	public void playAD()
	{
		adsManager.ShowAd(adcallBack);
	}

	private void adcallBack()
	{

	}

	public void facebookPage()
	{
		Application.OpenURL("https://www.facebook.com/OverclockEntretenimento/");
	}
}
