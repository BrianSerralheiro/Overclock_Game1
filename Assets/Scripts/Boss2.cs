using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : EnemyBase {
	private Transform clawL;
	private Transform clawR;
	private Transform elbowL;
	private Transform elbowR;
	private Transform lidT;
	private Transform lidB;
	private Transform eye;
	private Transform eyes;
	private LineRenderer lineelbowL;
	private LineRenderer lineelbowR;
	private LineRenderer lineclawL;
	private LineRenderer lineclawR;
	private Vector3 target;
	private float timer;
	private Transform moving;
	private Vector3 left=new Vector3(1.1f,-1,0);
	private Vector3 right = new Vector3(-1.1f,-1,0);
	enum State
	{
		intro,
		waiting,
		punching,
		shooting
	}
	[SerializeField]
	State state;
	new void Start () {
		base.Start();
		hp=500;
		points = 1000;
		GameObject go = new GameObject("clawL");
		go.AddComponent<EnemyBase>().SetHP(50);
		lineclawL=go.AddComponent<LineRenderer>();
		Config(lineclawL);
		Rigidbody2D r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss2[1];
		go.AddComponent<BoxCollider2D>();
		clawL=go.transform;
		go = new GameObject("elbowL");
		lineelbowL=go.AddComponent<LineRenderer>();
		Config(lineelbowL);
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss2[2];
		elbowL=go.transform;
		go = new GameObject("clawR");
		lineclawR=go.AddComponent<LineRenderer>();
		Config(lineclawR);
		go.AddComponent<EnemyBase>().SetHP(50);
		r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		SpriteRenderer sr= go.AddComponent<SpriteRenderer>();
		sr.sprite=SpriteBase.I.boss2[1];
		sr.flipX=true;
		go.AddComponent<BoxCollider2D>();
		clawR=go.transform;
		go = new GameObject("elbowR");
		lineelbowR=go.AddComponent<LineRenderer>();
		Config(lineelbowR);
		sr=go.AddComponent<SpriteRenderer>();
		sr.sprite=SpriteBase.I.boss2[2];
		sr.flipX=true;
		elbowR=go.transform;
		Vector3 v=new Vector3(2,-4,0);
		clawL.position=transform.position+v;
		v.x=-2;
		clawR.position=transform.position+v;
		go = new GameObject("lidB");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss2[3];
		lidB=go.transform;
		go = new GameObject("lidT");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss2[4];
		lidT=go.transform;
		lidB.parent=lidT.parent=transform;
		lidT.localPosition=new Vector3(0,-0.1f,0.1f);
		lidB.localPosition=new Vector3(0,-0.6f,0.1f);
		go = new GameObject("eye");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss2[5];
		eye=go.transform;
		eye.parent=transform;
		eye.localPosition=new Vector3(0,-0.3f,0.2f);
		go = new GameObject("eyes");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss2[6];
		eyes=go.transform;
		eyes.parent=transform;
		eyes.localPosition=new Vector3(0,0.46f,0);
	}

	// Update is called once per frame
	protected new void Update(){
		base.Update();
		if(state==State.intro)
		{
			transform.Translate(0,-Time.deltaTime,0);
			if(transform.position.y<8) state=State.waiting;
			timer=5;
		}
		else if(state==State.waiting)
		{
			timer-=Time.deltaTime;
			if(timer<0)state=State.punching;
		}
		else if(state==State.shooting)
		{

		}
		else if(state==State.punching)
		{

		}

		if(clawL){
			MoveElbow(elbowL,transform.position+left,clawL.position,true);
			Chock(lineclawL,elbowL.position,clawL.position);
			Chock(lineelbowL,transform.position+left,elbowL.position);
		}
		else elbowL.gameObject.SetActive(false);
		if(clawR){
			MoveElbow(elbowR,transform.position+right,clawR.position,false);
			Chock(lineclawR,elbowR.position,clawR.position);
			Chock(lineelbowR,transform.position+right,elbowR.position);
		}
		else elbowR.gameObject.SetActive(false);

	}

	private void Config(LineRenderer l)
	{
		l.positionCount=10;
		l.widthMultiplier=0.1f;
		l.material=SpriteBase.I.shock;
		//l.material=Material.
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
	protected new void OnDestroy()
	{
		base.OnDestroy();
		Destroy(elbowL.gameObject);
		Destroy(elbowR.gameObject);
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
}
