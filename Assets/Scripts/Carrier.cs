using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : EnemyBase {
	private Vector3 finaalposition;
	private float timer=-2f;
	private Diver diver;

	public override void Position(int i)
	{
		base.Position(i);
		hp=100;
		if(i<3)
		{
			finaalposition=new Vector3(i*2.5f,-12,0);
		}
		else
		{
			finaalposition=new Vector3(i%2>0 ? 6 : -1,10-(i-1)/2*2.5f,0);
			transform.Rotate(0,0,i%2==0?-90:90);
		}
	}

	void Update()
	{
		base.Update();
		transform.Translate(0,-Time.deltaTime,0);
		timer+=Time.deltaTime;
		if(timer>=1 && !diver)Spawn();
		if(diver && timer<2)
		{
			diver.transform.localPosition=Vector3.up*2.5f*(timer-1)+Vector3.forward*0.1f;
		}
		if(timer>2)
		{
			diver.enabled=true;
			diver.transform.parent=null;
			diver=null;
			timer =-1;
		}
		if(transform.position.x<-2 || transform.position.x>7 || transform.position.y<-2 || transform.position.y>12)Destroy(gameObject);
	}
	void Spawn()
	{
		GameObject go=new GameObject("enemy");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.diver;
		go.AddComponent<BoxCollider2D>();
		Rigidbody2D r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		diver =go.AddComponent<Diver>();
		diver.enabled=false;
		diver.Start();
		diver.transform.position=transform.position;
		diver.transform.rotation=transform.rotation;
		diver.transform.parent=transform;
	}
}
