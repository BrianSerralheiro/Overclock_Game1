using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour 
{
	[SerializeField]
	private RawImage IntroCharacters;
	[SerializeField]
	private Text tapStart;
	[SerializeField]
	private Text tapStartOutline;
	[SerializeField]
	private AudioSource audioHandler;

	private Color introColor;
	private AsyncOperation loading;

	void Start () 
	{
		ADManager.Initialize();
	}
	
	void Update () 
	{
		if(loading != null)
		{
			tapStart.text = (loading.progress * 100) + "%";
			tapStartOutline.text = tapStart.text;
			audioHandler.volume = 1f - loading.progress;
		}
		else
		{
		  introColor = tapStart.color;
		  introColor.a = Mathf.Abs(Mathf.Cos(Time.time * 2));
		  tapStart.color = introColor;
		  introColor = tapStartOutline.color;
		  introColor.a = Mathf.Abs(Mathf.Cos(Time.time * 2));
		  tapStartOutline.color = introColor;
		}
	}

	public void OnTap()
	{
		loading = SceneManager.LoadSceneAsync("MainMenu");
	}
}
