using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	}

	public void playAD()
	{
		ADManager.ShowAd(adcallBack);
	}

	private void adcallBack()
	{

	}

	public void facebookPage()
	{
		Application.OpenURL("https://www.facebook.com/OverclockEntretenimento/");
	}
}
