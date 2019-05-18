﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : EnemyBase {
	private EnemyBase clawL;
	private EnemyBase clawR;
	private Transform elbowL;
	private Transform elbowR;
	private Transform lidT;
	private Transform lidB;
	private Transform eye;
	private Core eyes;
	private LineRenderer lineelbowL;
	private LineRenderer lineelbowR;
	private LineRenderer lineclawL;
	private LineRenderer lineclawR;
	private Vector3[] pos={new Vector3(-0.5f,0),new Vector3(-0.15f,-0.06f),new Vector3(0.15f,-0.06f), new Vector3(0.5f,0)};
	private Vector3 target;
	private float timer;
	private float time;
	private Transform moving;
	private Vector3 vectorB=new Vector3(0,-0.6f,0.1f);
	private Vector3 vectorT=new Vector3(0,-0.1f,0.1f);
	private Vector3 left=new Vector3(1.1f,-1,0);
	private Vector3 right = new Vector3(-1.1f,-1,0);
	enum State
	{
		intro,
		waiting,
		punching,
		shooting,
		dead
	}
	[SerializeField]
	State state;
	new void Start () {
		damageEffect = true;
		base.Start();
		EnemySpawner.boss=true;
		hp=500;
		points = 1000;
		GameObject go = new GameObject("enemy");
		clawL=go.AddComponent<EnemyBase>();
		clawL.SetHP(150);
		lineclawL=go.AddComponent<LineRenderer>();
		Config(lineclawL);
		Rigidbody2D r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss2[1];
		go.AddComponent<BoxCollider2D>();
		go = new GameObject("elbowL");
		lineelbowL=go.AddComponent<LineRenderer>();
		Config(lineelbowL);
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss2[2];
		elbowL=go.transform;
		go = new GameObject("enemy");
		lineclawR=go.AddComponent<LineRenderer>();
		Config(lineclawR);
		clawR=go.AddComponent<EnemyBase>();
		clawR.SetHP(150);
		r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		SpriteRenderer sr= go.AddComponent<SpriteRenderer>();
		sr.sprite=SpriteBase.I.boss2[1];
		sr.flipX=true;
		go.AddComponent<BoxCollider2D>();
		go = new GameObject("elbowR");
		lineelbowR=go.AddComponent<LineRenderer>();
		Config(lineelbowR);
		sr=go.AddComponent<SpriteRenderer>();
		sr.sprite=SpriteBase.I.boss2[2];
		sr.flipX=true;
		elbowR=go.transform;
		clawL.transform.position=transform.position+left;
		clawR.transform.position=transform.position+right;
		go = new GameObject("lidB");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss2[3];
		lidB=go.transform;
		go = new GameObject("lidT");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss2[4];
		lidT=go.transform;
		lidB.parent=lidT.parent=transform;
		lidT.localPosition=vectorT;
		lidB.localPosition=vectorB;
		go = new GameObject("eye");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss2[5];
		eye=go.transform;
		eye.parent=transform;
		eye.localPosition=new Vector3(0,-0.3f,0.2f);
		go = new GameObject("eyes");
		eyes=go.AddComponent<Core>().Set(SpriteBase.I.boss2[6],new Color(0.5f,0.1f,0.05f));
		go.transform.parent=transform;
		go.transform.localPosition=new Vector3(0,0.46f,0);
	}

	// Update is called once per frame
	protected new void Update(){
		if(Ship.paused) return;
		base.Update();
		timer-=Time.deltaTime;
		if(state==State.intro)
		{
			transform.Translate(0,-Time.deltaTime*2,0);
			if(clawL){
				clawL.transform.position=transform.position+left;
				clawL.SetHP(150);
			}
			if(clawR){
				clawR.transform.position=transform.position+right;
				clawR.SetHP(150);
			}
			if(transform.position.y<Scaler.sizeY/2f){state=State.waiting;
			timer=3;}
		}
		else if(state==State.waiting)
		{
			if(vectorB.y<-0.6f) vectorB.y+=Time.deltaTime/10;
			if(clawL)clawL.transform.position=Vector3.MoveTowards(clawL.transform.position,transform.position+left,5*Time.deltaTime);
			if(clawR) clawR.transform.position=Vector3.MoveTowards(clawR.transform.position,transform.position+right,5*Time.deltaTime);
			if(timer<0){
				if(clawL || clawR){
					if(Random.value<=0.3f){
						state=State.shooting;
						timer=2;
					}else{
					state=State.punching;
					target=transform.position+(player.position-transform.position).normalized*10;
					if(clawL && clawR)moving=Random.value<0.5f?clawR.transform:clawL.transform;
					else
					{
						if(clawL)moving=clawL.transform;
						if(clawR)moving=clawR.transform;
					}
					}
				}
				else
				{
					state=State.shooting;
					timer=3;
				}
			}
		}
		else if(state==State.shooting)
		{
			if(vectorB.y>=-1f)vectorB.y-=Time.deltaTime/10;
			if(!clawL && !clawR){
				transform.Translate(Mathf.Cos(time)*Time.deltaTime*4,0,0);
				time+=Time.deltaTime;
			}
			if(timer<0)
			{
				for(int i = 0; i<4; i++)
				{
					Shoot(i);
				}
				timer=0.6f;
				eyes.Set(1);
				if(clawL || clawR)state=State.waiting;
			}
		}
		else if(state==State.punching)
		{
			if(moving)
			{
				moving.position=Vector3.MoveTowards(moving.position,target,10*Time.deltaTime);
				if((target-moving.position).sqrMagnitude<1f)
				{
					state=State.waiting;
					timer=3;
				}
			}
			else
			{
				state=State.waiting;
				timer=1;
			}
		}
		else if(state==State.dead)
		{
			ParticleManager.Emit(0,(Vector3)Random.insideUnitCircle*1.5f+transform.position,1);
			if(timer<0)
			{
				EnemySpawner.boss=false;
				Destroy(gameObject);
				Destroy(elbowL.gameObject);
				Destroy(elbowR.gameObject);
			}
		}
		eyes.Min(Time.deltaTime*2);
		lidB.localPosition=vectorB;
		vectorT.y=-(lidB.localPosition.y+0.7f);
		lidT.localPosition=vectorT;
		if(clawL){
			MoveElbow(elbowL,transform.position+left,clawL.transform.position,true);
			Chock(lineclawL,elbowL.position,clawL.transform.position);
			Chock(lineelbowL,transform.position+left,elbowL.position);
		}
		else elbowL.gameObject.SetActive(false);
		if(clawR){
			MoveElbow(elbowR,transform.position+right,clawR.transform.position,false);
			Chock(lineclawR,elbowR.position,clawR.transform.position);
			Chock(lineelbowR,transform.position+right,elbowR.position);
		}
		else elbowR.gameObject.SetActive(false);

	}

	private void Config(LineRenderer l)
	{
		l.positionCount=10;
		l.widthMultiplier=0.1f;
		l.material=SpriteBase.I.shock;
	}
	private void Chock(LineRenderer render,Vector3 v1,Vector3 v2)
	{
		Vector3 v=(v2-v1)/10;
		render.SetPosition(0,v1);
		render.SetPosition(9,v2);
		for(int i = 1; i<9; i++)
		{
			Vector3 v3=v1+v*i;
			v3.x+=Random.Range(-0.05f,0.05f);
			v3.y+=Random.Range(-0.05f,0.05f);
			v3.z=0.1f;
			render.SetPosition(i,v3);
		}
	}
	void Shoot(int i)
	{
		GameObject go = new GameObject("enemybullet");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.bullets[10];
		go.AddComponent<BoxCollider2D>();
		Bullet bu=go.AddComponent<Bullet>();
		bu.owner=name;
		bu.spriteID=10;
		go.transform.position=eyes.transform.position+pos[i]+Vector3.back*0.5f;
		go.transform.up=Vector3.down;
	}
	protected override void Die()
	{
		state=State.dead;
		timer=3;
		EnemySpawner.points+=1000;
	}
	private void MoveElbow(Transform t,Vector3 v1,Vector3 v2,bool b)
	{
		float d = (v2-v1).magnitude;
		Vector3 mid = (v2-v1)/2+v1;
		if(d>5)
		{
			t.position=mid;

		}
		else
		{
			Vector3 v = v1-v2;
			Vector3 f = new Vector3();
			v.Normalize();
			f.x=v.y+Mathf.Sin(Time.time)/5;
			f.y=-v.x+Mathf.Cos(Time.time)/5;
			f.z=v.z;
			if(b)f*=-1;
			t.position=mid-f*(5-d)/2;
		}
	}
	private new void OnCollisionEnter2D(Collision2D col)
	{
		if(state==State.dead) return;
		if(vectorB.y<-0.7f) base.OnCollisionEnter2D(col);
	}
	public override void Position(int i)
	{
		transform.position=new Vector3(0,Scaler.sizeY+5,0);
	}
}
