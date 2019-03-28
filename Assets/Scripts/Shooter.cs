using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyBase
{
	private int position;
	private Vector3 finalpoint;
	private float shoottimer=1;
	private float lifetimer;

	private Transform armL;
	private Transform armR;
	private Transform legL;
	private Transform legR;
	private Vector3 vector = new Vector3();
	protected void Start()
	{
		base.Start();
		points = 100;
		_renderer.flipY=true;
		lifetimer=5;
		if(legL) return;
		GameObject go = new GameObject("legL");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.shooterlegs[0];
		legL=go.transform;
		go=new GameObject("legR");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.shooterlegs[1];
		legR=go.transform;
		go=new GameObject("armL");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.shooterarms[0];
		armL=go.transform;
		go=new GameObject("armR");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.shooterarms[1];
		armR=go.transform;
		armL.parent=armR.parent=legL.parent=legR.parent=transform;
		armL.localPosition=new Vector3(-0.2f,0.6f,0.1f);
		armR.localPosition=new Vector3(0.2f,0.6f,0.1f);
		legL.localPosition=new Vector3(0,-0.5f,0.1f);
		legR.localPosition=new Vector3(0,-0.5f,0.1f);
	}

	public override void Position(int i)
{
	base.Position(i);
	position=i;
	if(i<3)
	{
		finalpoint=new Vector3((i+1)*1.25f,9.5f,0);
	}
	else
	{
		finalpoint=new Vector3(i%2>0 ? 1 : 4, 9,0);
	}
}
void Update()
{
	if(Ship.paused) return;
	if(position>=0)
	{
		transform.Translate((finalpoint-transform.position).normalized*Time.deltaTime,Space.World);
		transform.up=finalpoint-transform.position;
		if((finalpoint-transform.position).sqrMagnitude<0.005f)
		{
			transform.position=finalpoint;
			position=-1;
		}
	}
	else
	{
		transform.eulerAngles=new Vector3(0,0,Mathf.Atan2(transform.position.x-player.position.x,player.position.y-transform.position.y)*Mathf.Rad2Deg);
		if(shoottimer>0) shoottimer-=Time.deltaTime;
	}
		vector.Set(0,0,180+Mathf.PingPong(Time.time*100,35f));
		armL.localEulerAngles=vector;
		armR.localEulerAngles=-vector;
		legL.localEulerAngles=-vector;
		legR.localEulerAngles=vector;
}
void LateUpdate()
{
	if(shoottimer<=0)
	{
		shoottimer=1.5f;
		Shoot();
	}
}
	void Shoot()
	{
		GameObject go = new GameObject("enemybullet");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.bullet;
		go.AddComponent<BoxCollider2D>();
		go.AddComponent<Bullet>().owner=transform.name;
		go.transform.position=transform.position;
		go.transform.rotation=transform.rotation;
	}
}
