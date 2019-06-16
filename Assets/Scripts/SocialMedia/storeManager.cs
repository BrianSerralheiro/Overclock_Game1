using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storeManager : MonoBehaviour 
{


	// Use this for initialization
	void OnEnable () 
	{
		facebookManager.Initialize();
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
		facebookManager.inviteFriends();
	}

	public void share()
	{
		facebookManager.shareGame();
	}

	public void playAD()
	{
		ADManager.ShowAd(adcallBack);
	}

	private void adcallBack()
	{

	}
}
