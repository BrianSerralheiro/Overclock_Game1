using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public static class facebookManager
{
	public static void Initialize()
	{
		if(FB.IsInitialized)
		{
			FB.ActivateApp();
		}
		else
		{
			FB.Init();
		}
	}

	private static void callBack()
	{
		if(FB.IsInitialized)
		{
			FB.ActivateApp();
		}
		else
		{
			Debug.LogError("Not initialized");
		}
	}

	public static void loggOut()
	{
		FB.LogOut();
	}

	public static void logIn()
	{
		List <string> permission = new List <string>()
		{
			"public_profile","email"
		};
		FB.LogInWithReadPermissions(permission, OnLogin);
	}

	private static void OnLogin(ILoginResult result)
	{
		if(FB.IsLoggedIn)
		{
		
		}
		else
		{
			
		}
	}

	public static void shareGame()
	{
		FB.ShareLink(new System.Uri("PLAYSTORE.com"), "Space Trinity - Conqueror's Invasion", "Available Now! Free to play with 4 Characters and over 15+ SpaceShip Skins!", new System.Uri("image link"));
	}
	public static void inviteFriends()
	{
		FB.AppRequest("Space Trinity! Free to play with 4 Characters and over 15+ SpaceShip Skins!");
	}

}
