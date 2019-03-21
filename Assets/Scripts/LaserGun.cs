using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun {
	[SerializeField]
	private Material lasermat;
	[SerializeField]
	private Sprite lasersprite;
	private Transform laser;
	private Vector3 scale;
	private Collider2D col;
	// Use this for initialization
	void Start () {
		GameObject go=new GameObject("laserbase");
		laser=go.transform;
		laser.position=transform.position;
		laser.parent=transform;
		go.AddComponent<SpriteRenderer>().sprite=sprite;
		go=new GameObject("laserbody");
		SpriteRenderer sr= go.AddComponent<SpriteRenderer>();
		sr.material=lasermat;
		sr.sprite=lasersprite;
		col=go.AddComponent<BoxCollider2D>();
		go.transform.position=transform.position+Vector3.up*0.335f;
		go.transform.localScale=Vector3.one+Vector3.up*29f;
		go.transform.parent=laser;
		scale=Vector3.up;
	}
	public override void Shoot()
	{
		col.enabled=!col.enabled;
		if(scale.x<level)scale.x+=1f;
		if(scale.x>level)scale.x=level;
	}
	public override void Level(int i)
	{
		if(i<4)level=i;
	}
	void Update () {
		laser.localScale=scale;
		if(scale.x>0)scale.x-=Time.deltaTime*10;
		if(scale.x<0)scale.x=0;
	}
}
