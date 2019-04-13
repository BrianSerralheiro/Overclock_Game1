using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

	[SerializeField]
	private GameObject eventSystem;

	private float Timer;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Timer > 0)
		{
			Timer -= Time.deltaTime;
		}
		if(Timer < 0)
		{
			SceneManager.LoadScene("cen");
		}
	}

	public void StartButton()
	{
		Timer = 2;
		eventSystem.SetActive(false);
		SoundManager.PlayEffects(1);
	}

	public void Store()
	{
		SoundManager.PlayEffects(0);
	}

	public void Settings()
	{
		SoundManager.PlayEffects(0);
	}
}
