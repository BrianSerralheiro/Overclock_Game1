using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyBase
{
	private int position;
	private Vector3 finalpoint;
	float shoottimer=1;
	float lifetimer;
	public static Transform player;
	protected void Start()
	{
		base.Start();
		_renderer.flipY=true;
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
