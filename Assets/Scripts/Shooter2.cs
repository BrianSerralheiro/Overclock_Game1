using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter2 : Shooter
{
private int prev;

public override void Position(int i)
{
	if(i==1) i=2;
	base.Position(i);
	hp=2;
	prev=i;
}

void OnCollisionEnter2D(Collision2D col)
{
	if(--hp<=0)
	{
		gameObject.SetActive(false);
		return;
	}
	Vector3 v = transform.position;
	Position((prev+1)%9);
	transform.position=v;
	hp=1;
}
	
}
