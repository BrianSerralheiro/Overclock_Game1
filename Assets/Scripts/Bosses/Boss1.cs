using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyBase {

	private Transform wingL;
	private Transform wingR;
	private Vector3 dir=Vector3.right+Vector3.up;
	enum State
	{
		intro,
		moving,
		charging
	}
	[SerializeField]
	State state;
	private Vector3 vector = new Vector3();
	public new void Start()
	{
		base.Start();
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
			transform.Translate(0,-Time.deltaTime,0);
			if(transform.position.y<7)state=State.moving;
		}else if(state==State.moving)
		{
			if(transform.position.y>9)dir.y=-Mathf.Abs(dir.y);
			if(transform.position.x>4)dir.x=-Mathf.Abs(dir.x);
			if(transform.position.x<1)dir.x=Mathf.Abs(dir.x);
			transform.Translate(dir*Time.deltaTime);
			if(transform.position.y<1)state=State.charging;
		}
		else if(state==State.charging)
		{
			if(vector.z>45)transform.Translate(0,Time.deltaTime*(hp<200?6:4),0);
			if(transform.position.y>12){
				state=State.moving;
				float f=Random.value*(hp<200?4:2);
				dir.y=Mathf.Max(f,2f-f);
				dir.x=2f-dir.y;
			}
		}
		if(state==State.charging && vector.z<45)vector.z+=Time.deltaTime*90;
		if(state!=State.charging && vector.z>0)vector.z-=Time.deltaTime*20;
		wingL.localEulerAngles=vector;
		wingR.localEulerAngles=-vector;
	}
	public override void Position(int i)
	{
		transform.position=new Vector3(2.5f,14,0);
	}
	private new void OnCollisionEnter2D(Collision2D col)
	{
		if(vector.z>5)base.OnCollisionEnter2D(col);
	}
}
