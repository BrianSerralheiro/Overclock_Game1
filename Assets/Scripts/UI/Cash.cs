using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cash : MonoBehaviour 
{
	[SerializeField]
	private Text cash;

	public static int totalCash;
	
	void Start () 
	{
		totalCash = PlayerPrefs.GetInt("cash");
	}
	
	void Update () 
	{
		cash.text = totalCash.ToString();
	}
}
