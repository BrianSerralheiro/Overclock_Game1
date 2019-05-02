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
	private RectTransform fillMask;
	private RectTransform lifeFill;

	public static float _special;

	[SerializeField]
	private RectTransform specialFill;
	Vector3 helper=Vector3.one;
	
	void Start () 
	{
		lifeFill=fillMask.GetChild(0) as RectTransform;
	}
	
	void Update () 
	{
		helper.x=shipHealth;
		fillMask.localScale =helper;
		helper.x=1f/shipHealth;
		lifeFill.localScale=helper;
		scoreHUD.text = EnemySpawner.points.ToString();
		if(_special > 1)
		{
			_special = 1;
		}
		helper.x=_special;
		specialFill.localScale =helper;
	}
}
