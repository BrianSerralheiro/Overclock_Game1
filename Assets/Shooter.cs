using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyBase
{
	public static Bullet[]  bulletPool;
	private int position;
	private Vector3 finalpoint;
	float shoottimer=1;
	float lifetimer;
	public static Transform player;

public override void Position(int i)
{
	base.Position(i);
	position=i;
	if(i<3)
	{
		finalpoint=new Vector3(64+i*64,512-32,0);
	}
	else
	{
		finalpoint=new Vector3(i%2>0 ? 64 : 192,(512-(i-1)/2*128),0);
	}
}
void Update()
{
	if(Ship.paused) return;
	if(position>=0)
	{
		transform.Translate((finalpoint-transform.position)*Time.deltaTime,Space.World);
		transform.up=finalpoint-transform.position;
		if((finalpoint-transform.position).sqrMagnitude<2)
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
	for(int i=0; i<bulletPool.Length; i++){
		if(!bulletPool[i].gameObject.activeSelf)
		{
			bulletPool[i].Position(transform);

			break;
		}
	}
}
}
