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
	
	void Start () 
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

	public void ReturnMenu()
	{
		adsManager.CloseBanner();
		SceneManager.LoadScene("MainMenu");
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
