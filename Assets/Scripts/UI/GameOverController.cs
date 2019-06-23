using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ADs;

public class GameOverController : MonoBehaviour 
{
	[SerializeField]
	private Text LevelCleared;
	[SerializeField]
	private Text Score;
	[SerializeField]
	private Text Stars;
    [SerializeField]
	private int shipID;
	[SerializeField]
	private Continue cont;

	private static float Timer;

	private static Ship ship;

	private static GameObject menu;
	
		void Start () 
	{			
		menu = gameObject;
		gameObject.SetActive(false);

	}
	
	void OnEnable () 
	{
		if(shipID != Ship.playerID)
		{
			gameObject.SetActive(false);
			return;
		}
		Score.text = EnemySpawner.points.ToString();
		int cashStars = EnemySpawner.points /200;
		Stars.text = cashStars.ToString();
		Cash.totalCash += cashStars;
		adsManager.RequestBanner();
	}
	
	public static void Open(Ship s)
	{
		//adsManager.RequestVideo();
		ship = s;
		menu.SetActive(true);
		Timer = 10;
	}

	public void RevivePopUp()
	{
		cont.gameObject.SetActive(true);
		adsManager.CloseBanner();
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
