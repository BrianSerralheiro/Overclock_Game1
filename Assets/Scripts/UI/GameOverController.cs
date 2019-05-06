using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	// Use this for initialization
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
	}

	public void ReturnMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
