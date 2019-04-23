using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : EnemyBase {
	private float timer=-2f;
	private Diver diver;

	private Transform[] legs;
	private Vector3 vector = new Vector3();

	public new void Start()
	{
		base.Start();
		hp=100;
		points = 150;
		legs=new Transform[6];
		for(int i = 0; i<6; i++)
		{
			GameObject go = new GameObject("leg"+i);
			go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.carrierlegs[i];
			legs[i]=go.transform;
			go.transform.parent=transform;
			go.transform.rotation=transform.rotation;
		}
		legs[0].localPosition=new Vector3(0.45f,0.3f,0.1f);
		legs[1].localPosition=new Vector3(-0.45f,0.3f,0.1f);
		legs[2].localPosition=new Vector3(0.7f,1.7f,0.1f);
		legs[3].localPosition=new Vector3(-0.7f,1.7f,0.1f);
		legs[4].localPosition=new Vector3(0.9f,2.9f,0.1f);
		legs[5].localPosition=new Vector3(-0.9f,2.9f,0.1f);
	}
	public override void Position(int i)
	{
		base.Position(i);
		if(i>=8)
		{
			transform.Rotate(0,0,i==8?90:-90);
		}
	}

	new void Update()
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
		vector.Set(0,0,Mathf.PingPong(Time.time*50,45f));
		legs[0].localEulerAngles=vector;
		legs[1].localEulerAngles=-vector;
		legs[2].localEulerAngles=-vector;
		legs[3].localEulerAngles=vector;
		legs[4].localEulerAngles=vector;
		legs[5].localEulerAngles=-vector;

		if(transform.position.x<-Scaler.sizeX/2f-4.2F || transform.position.x>Scaler.sizeX/2f+4.2F || transform.position.y<-Scaler.sizeY-4.2F) Die();
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
