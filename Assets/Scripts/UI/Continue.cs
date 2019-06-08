using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour 
{
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
		if(Timer <= 0)
		{
			Ship.paused = false;
			SceneManager.LoadScene("GameOver");			
		}
	}

	public void WatchAd()
	{
		ship.Heal();
		Ship.paused = false;
		gameObject.SetActive(false);
		/* if(ad advertisment.isReady)
		{
			
		}
		
		
		
		 */
	}

	public void buyContinue()
	{
		gameObject.SetActive(false);
		Ship.paused = false;
		ship.Heal();
	}

	public static void Open(Ship s)
	{
		ship = s;
		menu.SetActive(true);
		Timer = 10;
	}

	public void reduceTimer()
	{
		Timer -= 1;
	}


}
