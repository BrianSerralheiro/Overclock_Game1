﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : EnemyBase {
	private float timer=4;
	private Vector3 local=new Vector3(-3.5f,-0.2f);
	new void Start () {
		base.Start();
		hp=200;
	}

	public override void Position(int i)
	{
		base.Position(i);
		if(i<8)
		{
			transform.Rotate(0,0,-90f);

		}
		else 
		{
			transform.Rotate(0,0,i==8?0:180);
		}
	}
	new void Update () {
		base.Update();
		transform.Translate(Time.deltaTime,0,0);
		timer-=Time.deltaTime;
		if(timer<0)Bomb();
		if(transform.position.x<-Scaler.sizeX/2f-4.2F || transform.position.x>Scaler.sizeX/2f+4.2F || transform.position.y<-Scaler.sizeY-4.2F) Die();

	}
	void Bomb()
	{
		timer=2;
		GameObject go = new GameObject("enemy");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.bomber[1];
		go.AddComponent<BoxCollider2D>();
		Rigidbody2D r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		go.AddComponent<Bomb>();
		go.transform.parent=transform;
		go.transform.localPosition=local;
		go.transform.parent=null;
	}

}