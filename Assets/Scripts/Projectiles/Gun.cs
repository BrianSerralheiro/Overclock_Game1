using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	[SerializeField]
	protected int level;
	[SerializeField]
	protected Sprite sprite;
	[SerializeField]
	private int damage=1;
	[SerializeField]
	private bool pierce;

	public virtual void Shoot()
	{
		if(!gameObject.activeSelf)return;
		GameObject go=new GameObject("playerbullet");
		go.AddComponent<SpriteRenderer>().sprite=sprite;
		go.AddComponent<BoxCollider2D>();
		Bullet bull= go.AddComponent<Bullet>();
		bull.owner=transform.parent.name;
		bull.damage=damage;
		bull.pierce=pierce;
		go.transform.position=transform.position;
		go.transform.rotation=transform.rotation;
	}
	public virtual void Level(int i)
	{
		gameObject.SetActive(level<=i);
	}
}
