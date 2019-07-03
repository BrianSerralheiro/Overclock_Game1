using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testWarning : MonoBehaviour {

	private string text;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		GUI.Box(new Rect (0, Screen.height / 3, Screen.width, Screen.height), text);
		if(GUI.Button(new Rect(0,0,Screen.width / 5,Screen.height / 10), "close"))
		{
			Destroy(gameObject);
		}			
	}


	public static void Open (string s)
	{
		GameObject g = new GameObject("message");
		testWarning t = g.AddComponent<testWarning>();
		t.text = s;
		
	}
}
