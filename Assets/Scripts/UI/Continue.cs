using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ADs;

public class Continue : MonoBehaviour 
{
	public delegate void Del(bool b);
	public Del Active; 
	[SerializeField]
	private Button button;
	public Ship ship;

	

	public void WatchAd()
	{
		adsManager.ShowAd(ship.Heal);
		gameObject.SetActive(false);
		Active(false);
	}

	public void buyContinue()
	{
		gameObject.SetActive(false);
		Active(false);
		ship.Heal();
	}

}
