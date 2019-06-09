using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour 
{
	private float Timer;

	[SerializeField]
	private RawImage studioLogo;

	[SerializeField]
	private RawImage IntroCharacters;
	[SerializeField]
	private Image WhitePanel;
	[SerializeField]
	private Text tapStart;
	[SerializeField]
	private Text tapStartOutline;

	private Color introColor;
	private AsyncOperation loading;

	// Use this for initialization
	void Start () 
	{
		loading = SceneManager.LoadSceneAsync("MainMenu");
		loading.allowSceneActivation = false;
		introColor = studioLogo.color;
		Timer = 5f;
		ADManager.Initialize();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Timer > 0)
	  {

		Timer -= Time.deltaTime;
		introColor.a = (5 - Timer) / 2;
		if(introColor.a > 1)
		{
			introColor.a = 1;
		}

		studioLogo.color = introColor;
		
		if(Timer <= 0)
		{
			WhitePanel.gameObject.SetActive(false);
			IntroCharacters.gameObject.SetActive(true);
		}

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
		loading.allowSceneActivation = true;
	}
}
