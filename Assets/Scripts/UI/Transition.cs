using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour 
{
	[SerializeField]
	private Text Stage;
	public static float Timer;
	[SerializeField]
	private Color textColor;
	private Color Transparent;
	public static int worldID;

	// Use this for initialization
	void Start () 
	{
		Transparent = new Color(0,0,0,0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Timer > 0)
		{
			Timer -= Time.deltaTime;
			Stage.text = "World "+ (worldID / 3 + 1) + " Stage " + (worldID % 3 + 1);
			Stage.color = Color.Lerp(Transparent, textColor, Mathf.PingPong(Timer,1));
		}
	}
}
