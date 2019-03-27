using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour 
{
	private bool health;

	private int id;

	// Use this for initialization
	void Start () 
	{
		id = Random.Range(0 , 1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(0,-Time.deltaTime,0);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Ship s = other.GetComponent<Ship>();
		if (s != null)
		{
			if(id == 0)
			{
				s.Heal(1);
			}
			else if(id == 1)
			{
				s.OnLevel();
			}
			else if(id == 2)
			{
				s.Special();
			}
			Destroy(gameObject);
		}
	}
}
