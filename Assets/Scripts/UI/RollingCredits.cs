using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCredits : MonoBehaviour 
{

	[SerializeField]
	private RectTransform rollingCredits;

	// Use this for initialization
	void Start () 
	{
		rollingCredits = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		rollingCredits.Translate(0, Time.deltaTime * Screen.height / 7, 0);
	}
}
