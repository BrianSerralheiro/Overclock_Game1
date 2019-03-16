using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	[SerializeField]
	private int level;
	[SerializeField]
	private Sprite sprite;

	public void Shoot()
	{
		if(!enabled)return;
		GameObject go=new GameObject("playerbullet");
		go.AddComponent<SpriteRenderer>().sprite=sprite;
		go.AddComponent<BoxCollider2D>().isTrigger=true;
		go.AddComponent<Bullet>().time=1;
		go.transform.position=transform.position;
		go.transform.rotation=transform.rotation;
	}
	public void Level(int i)
	{
		if(level<=i)enabled=true;
		else enabled=false;
	}
}
