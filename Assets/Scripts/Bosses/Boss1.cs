using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyBase {

	private Transform wingL;
	private Transform wingR;
	private float speed=4;
	private Vector3 dir=Vector3.right+Vector3.up;
	enum State
	{
		intro,
		moving,
		charging,
		dead
	}
	[SerializeField]
	State state;
	private Vector3 vector = new Vector3();
	public new void Start()
	{
		base.Start();
		EnemySpawner.boss=true;
		hp=600;
		points = 1000;
		GameObject go = new GameObject("wingL");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss1[1];
		wingL=go.transform;
		go = new GameObject("wingR");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss1[2];
		wingR=go.transform;
		wingL.parent=wingR.parent=transform;
		wingL.localPosition=new Vector3(0.7f,1.9f,-0.1f);
		wingR.localPosition=new Vector3(-0.7f,1.9f,-0.1f);
	}


	new void Update()
	{
		base.Update();
		if(state==State.intro)
		{
			transform.Translate(0,-Time.deltaTime*2,0);
			if(transform.position.y<0)state=State.moving;
		}else if(state==State.moving)
		{
			if(transform.position.y>Scaler.sizeY-4)dir.y=-Mathf.Abs(dir.y);
			if(transform.position.x>Scaler.sizeX/2f-2)dir.x=-Mathf.Abs(dir.x);
			if(transform.position.x<-Scaler.sizeX/2f+2)dir.x=Mathf.Abs(dir.x);
			transform.Translate(dir*Time.deltaTime*(speed/4));
			if(transform.position.y<-Scaler.sizeY+2)state=State.charging;
		}
		else if(state==State.charging)
		{
			if(vector.z>45)transform.Translate(0,Time.deltaTime*speed,0);
			if(transform.position.y>Scaler.sizeY){
				state=State.moving;
				float f=Random.value*(hp<200?4:2);
				dir.y=Mathf.Max(f,2f-f);
				dir.x=2f-dir.y;
			}
		}
		else if(state==State.dead)
		{
			transform.Translate(0,-Time.deltaTime*3,0,Space.World);
			transform.Rotate(0,0,Time.deltaTime*3);
			if(transform.position.y<-Scaler.sizeY-4){
				Destroy(gameObject);
				EnemySpawner.boss=false;
			}
		}
		if(state==State.charging && vector.z<45)vector.z+=Time.deltaTime*90;
		if(state!=State.charging && vector.z>0)vector.z-=Time.deltaTime*20;
		if(state!=State.dead){
			wingL.localEulerAngles=vector;
			wingR.localEulerAngles=-vector;
		}
	}
	protected override void Die()
	{
		state=State.dead;
		EnemySpawner.points+=1000;
	}
	public override void Position(int i)
	{
		transform.position=new Vector3(0,Scaler.sizeY+5,0);
	}
	private new void OnCollisionEnter2D(Collision2D col)
	{
		if(state==State.dead)return;
		if(vector.z>5)base.OnCollisionEnter2D(col);
		speed=hp<200 ? 12 : 8;
	}
}
