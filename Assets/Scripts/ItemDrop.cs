using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour 
{
	private bool health;

	// Use this for initialization
	void Start () 
	{
		health = Random.value <= 0.5;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Ship s = other.GetComponent<Ship>();
		if (s != null)
		{
			if(health)
			{
				s.hp += 1;
			}
			else
			{
				s.OnLevel();
			}
			Destroy(gameObject);
		}
	}
}
