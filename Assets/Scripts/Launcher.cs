using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : EnemyBase {
	private float timer=5;
	private Transform rocket;
	private Transform burst;
	private Vector3 rot;
	private Vector3 scale=Vector3.one;
	private Vector3 pos=new Vector3(0,0.4f,0.1f);
	new void Start () {
		base.Start();
		hp=40;
		Create();
		timer=5;
	}
	private void Create()
	{
		timer=3;
		GameObject go = new GameObject("enemy");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.launcher[1];
		go.AddComponent<Missile>().SetHP(30);
		go.AddComponent<BoxCollider2D>();
		Rigidbody2D r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		rocket=go.transform;
		rocket.position=transform.position;
		go = new GameObject("burst");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.launcher[2];
		burst=go.transform;
		burst.parent=rocket;
		burst.localPosition=new Vector3(0,-0.5f);
		scale.y=0;
		burst.localScale=scale;
	}
	new void Update () {
		base.Update();
		timer-=Time.deltaTime;
		if(transform.position.y>-1)transform.Translate(0,-Time.deltaTime/2,0);
		else Die();
		if(timer>0 && rocket)
		{
			rocket.position=transform.position+pos;
			scale.y=0;
		}
		if(timer <0)
		{
			if(rocket)
			{
				scale.y=1;
				burst.localScale=scale;
				rot.z=(Mathf.Atan2(rocket.position.x-player.position.x,player.position.y-rocket.position.y)*Mathf.Rad2Deg);
				rocket.eulerAngles=rot;
				rocket.Translate(0,Time.deltaTime*4,0);
			}
			else Create();
		}

	}
	protected override void Die()
	{
		Destroy(gameObject);
		Destroy(rocket.gameObject);
	}
}
