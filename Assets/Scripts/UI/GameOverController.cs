using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ADs;

public class GameOverController : MonoBehaviour 
{
	[SerializeField]
	private Image panel;
	[SerializeField]
	private Image image;
	[SerializeField]
	private Text counter;
	[SerializeField]
	private Text LevelCleared;
	[SerializeField]
	private Text Score;
	[SerializeField]
	private Text Stars;
	[SerializeField]
	private Continue cont;
	public Sprite[] chars;
	public Sprite[] panels;

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
		panel.sprite=panels[Ship.playerID];
		image.sprite=chars[Ship.playerID];
		Score.text = EnemySpawner.points.ToString();
		int cashStars = EnemySpawner.points /200;
		EnemySpawner.points=0;
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
		cont.ship=ship;
		cont.Active=gameObject.SetActive;
		cont.gameObject.SetActive(true);
		adsManager.CloseBanner();
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
