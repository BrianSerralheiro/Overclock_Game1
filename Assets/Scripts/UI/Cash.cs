using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cash : MonoBehaviour 
{
	[SerializeField]
	private Text cash;

	public static int totalCash;

	// Use this for initialization
	void Start () 
	{
		totalCash = PlayerPrefs.GetInt("cash");
	}
	
	// Update is called once per frame
	void Update () 
	{
		cash.text = totalCash.ToString();
	}
}
