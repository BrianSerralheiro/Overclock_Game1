using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diver : EnemyBase
{
public static Transform player;
private Vector3 rotation=Vector3.zero;
	public void Start()
	{
		base.Start();
		hp=12;
		gameObject.AddComponent<BugMouth>();
		gameObject.AddComponent<BugLegs>();
	}
	void Update()
	{
		base.Update();
		if(Ship.paused) return;
		rotation.z=Mathf.Atan2(transform.position.x-player.position.x,player.position.y-transform.position.y)*Mathf.Rad2Deg+180;
		transform.eulerAngles=rotation;
		transform.Translate(0,-3*Time.deltaTime,0);

	}
}
