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

	private Color introColor;

	// Use this for initialization
	void Start () 
	{
		SoundManager.Play(0);
		introColor = studioLogo.color;
		Timer = 5f;
	}
	
	// Update is called once per frame
	void Update () 
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
			SceneManager.LoadScene("MainMenu");
		}
	}
}
