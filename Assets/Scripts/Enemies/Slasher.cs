﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slasher : EnemyBase {

	private float timer = 3;
	private Vector3 mod =new Vector3(0,-0.5f,0.1f);
	new void Start () {
		base.Start();
		hp=40;
	}

	new void Update()
	{
		base.Update();
		timer-=Time.deltaTime;
		if(timer>1){
			if(player.position.x<transform.position.x) transform.Translate(-Time.deltaTime,0,0);
			else transform.Translate(Time.deltaTime,0,0);
		}
		if(transform.position.y>7) transform.Translate(0,-Time.deltaTime,0);
		if(timer<=0)
		{
			timer=1.5f;
			Shoot();
		}
	}
	void Shoot()
	{
		GameObject go = new GameObject("enemy");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.slasher[1];
		go.AddComponent<BoxCollider2D>();
		go.AddComponent<Slash>();
		Rigidbody2D r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		go.transform.position=transform.position+mod;
		go.transform.localScale=Vector3.one*2;
	}
}