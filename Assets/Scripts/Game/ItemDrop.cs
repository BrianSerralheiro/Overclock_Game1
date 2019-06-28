using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour 
{
	private int id;
	private bool set;

	void Start () 
	{
		Set(Random.Range(0 , 2));
	}
	public void Set(int i)
	{
		if(set)return;
		set=true;
		id = i;
		GetComponent<SpriteRenderer>().sprite = SpriteBase.I.item[id];
		gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
	}
	void Update () 
	{
		if(Ship.paused) return;
		transform.Translate(0,-Time.deltaTime*4,0);
		if(transform.position.y<-Scaler.sizeY)Destroy(gameObject);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Ship s = other.GetComponent<Ship>();
		if (s != null)
		{
			SoundManager.PlayEffects(21);
			if(id == 0)
			{
				s.Shield();
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
