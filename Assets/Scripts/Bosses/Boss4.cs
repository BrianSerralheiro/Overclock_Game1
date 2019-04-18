using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : EnemyBase {
	enum State
	{
		intro=0,
		waiting=1,
		shot=2,
		slash=3,
		bomb=4,
		zap=5
	}
	[SerializeField]
	State state;
	private float timer=0.2f;
	private float time=0;
	private float ap=1f;
	private State prev;
	private bool left;
	private Vector3 vec = Vector3.left/2;
	private Vector3 mod = Vector3.forward/10;
	private Vector3 pos = new Vector3();
	private Vector3 rot = new Vector3();
	private Vector3 scale = Vector3.one;
	private Vector3 local =new Vector3(0,1,-0.1f);
	private Transform zap;
	private LineRenderer line;
	private Sprite[] screens;
	private Sprite screen;
	private SpriteRenderer screenren;
	private float screentimer;
	new void Start(){
		base.Start();
		hp=1000;
		screens=SpriteBase.I.screens;
		GameObject go=new GameObject("screen");
		screenren=go.AddComponent<SpriteRenderer>();
		go.transform.parent=transform;
		go.transform.localPosition=new Vector3(0.01f,-0.05f,-0.01f);
		go=new GameObject("energy");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.zapper[1];
		zap=go.transform;
		zap.parent=transform;
		zap.localPosition=local;
		zap.gameObject.SetActive(false);
		go = new GameObject("zap");
		line=go.AddComponent<LineRenderer>();
		line.useWorldSpace=true;
		line.positionCount=10;
		line.widthMultiplier=0.1f;
		BoxCollider2D col = go.AddComponent<BoxCollider2D>();
		col.size=new Vector2(1,11);
		col.offset=new Vector2(0,6f);
		go.transform.parent=zap;
		go.transform.localPosition=new Vector3();
		go.SetActive(false);
		//Screen(1,2.5f);
	}
	new void OnCollisionEnter2D(Collision2D col)
	{
		if(col.otherCollider.name=="zap") return;
		if(state!=State.intro)base.OnCollisionEnter2D(col);
		if(damageTimer>0)Screen(1,0.5f);
	}
	new void Update () {
		base.Update();
		timer-=Time.deltaTime;
		if(screen!=null)
		{
			screenren.sprite=screen;
			screentimer-=Time.deltaTime;
			if(screentimer<=0.1f)screenren.sprite=screens[0];
			if(screentimer<=0)screenren.sprite=screen=null;
		}
		if(state==State.intro)
		{
			transform.Translate(0,-Time.deltaTime,0);
			if(timer<=0)
			{
				timer=0.2f;
				screenren.sprite=screenren.sprite==screens[3] ? screens[2] : screens[3];
			}
			if(transform.position.y<8){
				state=State.waiting;
				timer=1;
				Screen(0,0.1f);
			}
		}
		else if(state==State.waiting)
		{
			if(transform.position.x<2.4f)transform.Translate(Time.deltaTime*2,0,0);
			else if(transform.position.x>2.6f)transform.Translate(-Time.deltaTime*2,0,0);
			if(transform.position.y<8f) transform.Translate(0,Time.deltaTime*2,0);
			if(timer<=0)
			{
				do
					state=(State)Random.Range(2,6);
				while(state==prev);
				prev=state;
				if(state==State.zap)timer=4;
			}
		}
		else if(state==State.shot)
		{
			if(player.position.x<transform.position.x) transform.Translate(-Time.deltaTime/2,0,0);
			else transform.Translate(Time.deltaTime/2,0,0);
			if(timer<=0)
			{
				Shoot();
				timer=0.5f;
				ap-=0.1f;
			}
			if(ap<=0)
			{
				state=State.waiting;
				ap=1;
			}
		}
		else if(state==State.slash)
		{
			if(timer<=0)
			{
				Slash();
				timer=1f;
				ap-=0.4f;
			}
			if(ap<=0)
			{
				state=State.waiting;
				ap=1;
			}
		}
		else if(state==State.bomb)
		{
			transform.Translate(Mathf.Cos(time)*Time.deltaTime*2,0,0);
			time+=Time.deltaTime;
			if(timer<=0)
			{
				Bomb();
				timer=2f;
				ap-=0.3f;
			}
			if(ap<=0)
			{
				state=State.waiting;
				ap=1;
				time=0;
				timer=3;
			}
		}
		else if(state==State.zap)
		{
			if(timer>1)
			{
				pos=Vector3.MoveTowards(transform.position,player.position,Time.deltaTime);
				transform.position=pos;
				rot.Set(0,0,Mathf.Atan2(zap.position.x-player.position.x,player.position.y-zap.position.y)*Mathf.Rad2Deg);
				zap.eulerAngles=rot;
			}
			else if(timer >0.1f)
			{
				zap.gameObject.SetActive(true);
				scale.x=scale.y=0.9f-timer;
				zap.localScale=scale;
				Screen(5,1);
			}
			else if(timer >0)
			{
				if(!line.gameObject.activeSelf)
				{
					line.gameObject.SetActive(true);
					Vector3 v = zap.position;
					Vector3 f = zap.up;
					line.SetPosition(0,v);
					for(int i = 1; i<9; i++)
					{
						v+=f;
						line.SetPosition(i,v+new Vector3(Random.value/2-0.5f,Random.value/10-0.1f));
					}
					line.SetPosition(9,v+f);
				}
			}
			else
			{
				timer=3;
				line.gameObject.SetActive(false);
				zap.gameObject.SetActive(false);
				state=State.waiting;
			}
		}
	}
	void Shoot()
	{
		GameObject go = new GameObject("enemybullet");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.invader[1];
		go.AddComponent<BoxCollider2D>();
		go.AddComponent<Bullet>().owner=name;
		go.transform.position=transform.position+vec*(left ? 1 : -1)+mod;
		go.transform.up=-transform.up;
		go.transform.localScale=Vector3.one*4f;
		left=!left;
		Screen(5,0.8f);

	}
	void Slash()
	{
		GameObject go = new GameObject("enemy");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.slasher[1];
		go.AddComponent<BoxCollider2D>();
		go.AddComponent<Slash>();
		Rigidbody2D r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		go.transform.position=transform.position+local;
		go.transform.localScale=Vector3.one*2;
		Screen(5,1);

	}
	void Bomb()
	{
		GameObject go = new GameObject("enemy");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.bomber[1];
		go.AddComponent<BoxCollider2D>();
		Rigidbody2D r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		go.AddComponent<Bomb>();
		go.transform.parent=transform;
		go.transform.localPosition=vec*(left ? 1 : -1)+mod;
		go.transform.parent=null;
		left=!left;
		Screen(5,1);

	}
	private void Screen(int i,float f)
	{
		if(screentimer>0)return;
		screen=screens[i];
		screentimer=f;
		screenren.sprite=screens[0];
	}
}
