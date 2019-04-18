using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_HUD : MonoBehaviour 
{
	[SerializeField]
	private Text scoreHUD;

	public static float shipHealth = 1;

	[SerializeField]
	private RectTransform lifeFill;
	
	public static float _special;

	[SerializeField]
	private RectTransform specialFill;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		lifeFill.localScale = Vector3.right * shipHealth + Vector3.up;
		scoreHUD.text = EnemySpawner.points.ToString();
		if(_special > 1)
		{
			_special = 1;
		}
		specialFill.localScale = Vector3.right * _special + Vector3.up;
	}
}
