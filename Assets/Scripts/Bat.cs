﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyBase {
	private Transform wingL;
	private Transform wingR;
	private Vector3 vector=new Vector3();
	public Vector3 target=new Vector3();

	new void Start () {
		base.Start();
		hp=10;
		GameObject go=new GameObject("wingL");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.bat[1];
		wingL=go.transform;
		go = new GameObject("wingR");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.bat[2];
		wingR=go.transform;
		wingL.parent=wingR.parent=transform;
		wingL.localPosition=new Vector3(0.1f,-0.1f,0.1f);
		wingR.localPosition=new Vector3(-0.1f,-0.1f,0.1f);
	}
	public override void Position(int i)
	{
		base.Position(i);
		if(i<3)
		{
			target.Set(1+i*1.5f,5,0);
		}
		else
		{
			target.Set(i%2>0 ? 6 : -1,(8-(i-1)/2*2.5f),0);
		}
	}
	new void Update () {
		base.Update();
		vector.Set(0,0,Mathf.PingPong(Time.time*300,-45f)+60f);
		wingL.localEulerAngles=vector;
		wingR.localEulerAngles=-vector;
		Vector3 pos=transform.position;
		pos=Vector3.MoveTowards(pos,target,Time.deltaTime*3);
		if((target-pos).sqrMagnitude<0.2f)target=pos+(player.position-pos).normalized*5;
		transform.position=pos;
	}
}