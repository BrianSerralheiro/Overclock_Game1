﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun {
	[SerializeField]
	private Sprite lasersprite;
	private Transform laser;
	private Vector3 scale;
	private Collider2D col;

	[SerializeField]
	private Transform particles;

	void Start () {
		GameObject go=new GameObject("laserbase");
		laser=go.transform;
		laser.position=transform.position;
		laser.parent=transform;
		laser.localScale=new Vector3();
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.bullets[spriteID+(Bullet.blink ? 0 : 1)]; ;
		go=new GameObject("laserbody");
		SpriteRenderer sr= go.AddComponent<SpriteRenderer>();
		sr.sprite=lasersprite;
		col=go.AddComponent<BoxCollider2D>();
		col.enabled=false;
		go.transform.position=transform.position+Vector3.up*0.2f;
		go.transform.localScale=Vector3.one+Vector3.up*39f;
		go.transform.parent=laser;
		scale=Vector3.up + Vector3.forward;
	}
	public override void Shoot()
	{
		col.enabled=!col.enabled;
		if(scale.x<level)scale.x+=1f;
		if(scale.x>level)scale.x=level;
		ParticleManager.Emit(6, transform.position, 1);
	}
	public override void Level(int i)
	{
		if(i<4)level=i;
	}
	void Update () {
		particles.localScale=scale;
		laser.localScale=scale;
		if(scale.x>0)scale.x-=Time.deltaTime*10;
		if(scale.x<0)scale.x=0;
	}
}
