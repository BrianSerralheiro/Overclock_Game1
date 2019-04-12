using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : EnemyBase {
	private bool left;
	private Vector3 vec=Vector3.left/10;
	private Vector3 mod=Vector3.forward/10;
	private float shoottimer = 1;

	new void Start () {
		base.Start();
		hp=30;
	}
	void Shoot()
	{
		GameObject go = new GameObject("enemybullet");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.invader[1];
		go.AddComponent<BoxCollider2D>();
		go.AddComponent<Bullet>().owner=name;
		go.transform.position=transform.position+vec*(left?1:-1)+mod;
		go.transform.up=-transform.up;
		left=!left;
	}
	new void Update () {
		base.Update();
		shoottimer-=Time.deltaTime;
		if(shoottimer<=0)
		{
			shoottimer=0.5f;
			Shoot();
		}
		if(player.position.x<transform.position.x)transform.Translate(-Time.deltaTime/2,0,0);
		else transform.Translate(Time.deltaTime/2,0,0);
		if(transform.position.y>9)transform.Translate(0,-Time.deltaTime,0);
	}
}
