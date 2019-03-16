using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuaperTest : MonoBehaviour {
	public GameObject red;
	public GameObject blue;
	public bool flag;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			flag=!flag;
			red.SetActive(!flag);
			blue.SetActive(flag);
		}
	}
}
