using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diver : EnemyBase
{
public static Transform player;



public override void Position(int i)
{
	base.Position(i);
	hp=2;

}
void Update()
{
	if(Ship.paused) return;
	transform.eulerAngles=new Vector3(0,0,Mathf.Atan2(transform.position.x-player.position.x,player.position.y-transform.position.y)*Mathf.Rad2Deg);
	transform.Translate(0,50*Time.deltaTime,0);

}
}
