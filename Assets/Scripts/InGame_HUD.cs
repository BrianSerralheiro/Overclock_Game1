using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_HUD : MonoBehaviour 
{
	[SerializeField]
	private Text scoreHUD;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		scoreHUD.text = EnemySpawner.points.ToString();
	}
}
