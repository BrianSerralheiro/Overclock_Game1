using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour 
{
	private int id;
	private bool set;

	private Vector3 dir;

	void Start () 
	{
		dir =  new Vector3(Random.value,-2 - Random.value, 0);
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
		if(transform.position.y>Scaler.sizeY-4)dir.y=-Mathf.Abs(dir.y);
		if(transform.position.x>Scaler.sizeX/2f-2)dir.x=-Mathf.Abs(dir.x);
		if(transform.position.x<-Scaler.sizeX/2f+2)dir.x=Mathf.Abs(dir.x);
		transform.Translate(dir * Time.deltaTime * 3);
		if(transform.position.y<-Scaler.sizeY)dir.Set(Random.value,0.5f + Random.value,0);
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
				InGame_HUD._special=1;
			}
			Destroy(gameObject);
		}
	}
}
