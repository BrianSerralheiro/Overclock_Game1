using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFBat : EnemyBase {
	private bool avoid;
	private float timer=5;
	private int ap;
	private float sprite;
	new void Start () {
		base.Start();
		explosionID = 10;
		hp=2000;
		points=20000;
	}
	
	new void Update ()
	{
		//if(Ship.paused) return;
		base.Update();
		if(timer>0)timer-=Time.deltaTime;
		if(sprite>0)sprite-=Time.deltaTime;
		_renderer.flipX=transform.position.x<player.position.x;
		transform.Translate(0,-Time.deltaTime/2,0);
		if(sprite<=0)_renderer.sprite=SpriteBase.I.MFBat[Mathf.RoundToInt(Mathf.PingPong(Time.time*3,1f))];
		if(avoid)
		{
			if(timer<0)
			{
				timer=0.2f;
				transform.position=Random.insideUnitCircle*4;
				if(ap++>7){
					avoid=false;
					timer=3;
					ap=0;
				}
			}
		}
		else
		{
			if(timer<0)
			{
				timer=1;
				if(ap++==0)transform.position=player.position+Vector3.up*5;
				Shoot();
				if(ap>2){
					avoid=true;
					ap=0;}
			}
		}
	}
	void Shoot()
	{
		GameObject go = new GameObject("enemy");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.bullets[18];
		go.AddComponent<BoxCollider2D>();
		go.AddComponent<Slash>().spriteID=18;
		Rigidbody2D r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		go.transform.position=transform.position;
		go.transform.localScale=Vector3.right*3+Vector3.up*2;
		sprite=0.3f;
		_renderer.sprite=SpriteBase.I.MFBat[2];
		//go.transform.localScale=Vector3.one*2;
	}
}
