using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

	[SerializeField]
	private Text text;
	[SerializeField]
	private GameObject eventSystem;

	private float Timer;
	private AsyncOperation loading;
	
	void Start () 
	{
		//PlayerPrefs.DeleteAll();
	}
	
	void Update () 
	{
		if(loading!=null)
		{
			text.text=Mathf.Round((loading.progress+0.1f)*100)+"%";
		}
	}

	public void StartButton()
	{
		eventSystem.SetActive(false);
		loading = SceneManager.LoadSceneAsync("cen");
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
