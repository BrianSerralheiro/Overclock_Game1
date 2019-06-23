using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ADs;

public class Continue : MonoBehaviour 
{
	[SerializeField]
	private Button button;
	private static Ship ship;

	private static GameObject menu;

	public void WatchAd()
	{
		adsManager.ShowAd(ship.Heal);
		gameObject.SetActive(false);
	}

	public void buyContinue()
	{
		gameObject.SetActive(false);
		ship.Heal();
	}

}
