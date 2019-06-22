using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour 
{
	[SerializeField]
	private Button button;
	[SerializeField]
	private Text countDown; 

	private static float Timer;

	private static Ship ship;

	private static GameObject menu;


	// Use this for initialization
	void Start () 
	{			
		menu = gameObject;
		gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () 
	{	
		Timer -= Time.deltaTime;
		countDown.text = Mathf.Ceil(Timer).ToString();
		button.interactable=adsManager.LoadedVideo();
		if(Timer <= 0)
		{
			Ship.paused = false;
			SceneManager.LoadScene("GameOver");			
		}
	}

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

	public static void Open(Ship s)
	{
		adsManager.RequestVideo();
		ship = s;
		menu.SetActive(true);
		Timer = 10;
	}

	public void reduceTimer()
	{
		Timer -= 1;
	}


}
