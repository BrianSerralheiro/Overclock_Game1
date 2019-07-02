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
	[SerializeField]
	public Graphic[] graphics;
	private static float Timer;

	private static Ship ship;

	private static GameObject menu;

	private bool highscore;

	[SerializeField]
	private Text gameoverTEXT;
	
	[SerializeField]
	private GameObject noContinues;
	[SerializeField]
	private Image gameoverDialog;

	private string fullText;

	private float charCount;
	private Color color=Color.white;
	void Start () 
	{			
		menu = gameObject;
		gameObject.SetActive(false);
		cont.Continues(Locks.IsPremium() ? 4 : 2);
	}

	void Update ()
	{
		if(Timer>0)
		{
			Timer-=Time.deltaTime;
			color.a=(2f-Timer)/2f;
			foreach(Graphic g in graphics)
			{
				g.color=color;
			}
			if(Timer<=0)gameoverDialog.gameObject.SetActive(true);
		}
		else if(charCount<fullText.Length)
		{
			charCount+=Time.deltaTime * 14;
			if(gameoverTEXT.text.Length<Mathf.FloorToInt(charCount))gameoverTEXT.text=fullText.Substring(0,Mathf.FloorToInt(charCount));
		}

	}
	
	void OnEnable () 
	{
		if(PlayerPrefs.GetInt("highscore") <= EnemySpawner.points)
		{
			highscore = true;
			PlayerPrefs.SetInt("highscore", EnemySpawner.points);
		}
		else
		{
			highscore = false;
		}
		charCount = 0;
		gameoverTEXT.text = "";
		fullText = highscore ? DialogBox.GetText(5):DialogBox.GetText(6);
		panel.sprite=panels[Ship.playerID];
		image.sprite=chars[Ship.playerID * 2 + (highscore ? 1:0)];
		Score.text = EnemySpawner.points.ToString();
		int cashStars = EnemySpawner.points /200;
		EnemySpawner.points=0;
		Stars.text = cashStars.ToString();
		Cash.totalCash += cashStars;
		gameoverDialog.sprite = DialogBox.getBox();
		gameoverTEXT.fontSize = Screen.height / 39;
	}

	public void Close()
	{
		if(charCount>=fullText.Length)
		{
			gameoverDialog.gameObject.SetActive(false);
		}
		else
		{
			charCount=fullText.Length;
			gameoverTEXT.text=fullText.Substring(0,Mathf.FloorToInt(charCount));
		}
	}
	
	public static void Open(Ship s)
	{
		//adsManager.RequestVideo();
		ship = s;
		menu.SetActive(true);
		Timer = 2f;
	}

	public void RevivePopUp()
	{
		if(cont.HasContinue()){
			SoundManager.PlayEffects(0);
			cont.ship=ship;
			cont.Active=gameObject.SetActive;
			cont.gameObject.SetActive(true);
			adsManager.CloseBanner();
		}
		else
		{
			//show warning;
			SoundManager.PlayEffects(11);
			noContinues.SetActive(true);
		}
	}
	public void QuitGame()
	{
		adsManager.CloseBanner();
		SoundManager.PlayEffects(0);
		Ship.paused = false;
		SceneManager.LoadScene("MainMenu");
	}
}
