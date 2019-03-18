using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diver : EnemyBase
{
public static Transform player;
private Vector3 rotation=Vector3.zero;

public override void Position(int i)
{
	base.Position(i);
	hp=2;

}
void Update()
{
	base.Update();
	if(Ship.paused) return;
	rotation.z=Mathf.Atan2(transform.position.x-player.position.x,player.position.y-transform.position.y)*Mathf.Rad2Deg;
	transform.eulerAngles=rotation;
	transform.Translate(0,3*Time.deltaTime,0);

}
}
