using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : EnemyBase {
	private Transform body;
	private Transform head;
	private Transform slash;
	private SpriteRenderer henderer;
	private SpriteRenderer slashren;
	private SpriteRenderer darkhren;
	private BoxCollider2D slashcod;
	private Color slashcol=Color.white;
	private Color darkcol=Color.black;
	private Vector3 slashscl=new Vector3(5000,0,0);
	private Vector3 slashrot=new Vector3(0,0,0);
	private float timer=1.5f;
	private float time=0;
	private Vector3 left=new Vector3(-0.64f,0.24f,-0.2f);
	private Vector3 right=new Vector3(0.64f,0.24f,-0.2f);
	enum State
	{
		intro,
		wating,
		shooting,
		calling,
		slashing
	}
	[SerializeField]
	State state;
	new void Start () {
		base.Start();
		hp=2000;
		GameObject go=new GameObject("body");
		_renderer=go.AddComponent<SpriteRenderer>();
		_renderer.sprite=SpriteBase.I.boss3[1];
		body=go.transform;
		go=new GameObject("head");
		henderer=go.AddComponent<SpriteRenderer>();
		henderer.sprite=SpriteBase.I.boss3[2];
		head=go.transform;
		body.parent=head.parent=transform;
		body.localPosition=new Vector3(0,-0.44f,-0.01f);
		head.localPosition=new Vector3(0,0.8f,-0.02f);

		go=new GameObject("slash");
		slash=go.transform;
		slashren=go.AddComponent<SpriteRenderer>();
		slashren.sprite=Sprite.Create(new Texture2D(2,2),new Rect(0,0,2,2),new Vector2(0.5f,0.5f));
		slash.localScale=slashscl;
		slashcod=go.AddComponent<BoxCollider2D>();
		slashcod.enabled=false;

		go=new GameObject("dark");
		darkhren=go.AddComponent<SpriteRenderer>();
		darkhren.sprite=Sprite.Create(new Texture2D(2,2),new Rect(0,0,2,2),new Vector2(0,0));
		//darkcol.a=0;
		darkhren.color=darkcol;
		go.transform.localScale=new Vector3(250,500);
		go.transform.position=new Vector3(0,0,-0.09f);
	}
	
	new void Update () {
		base.Update();
		henderer.color=_renderer.color;
		timer-=Time.deltaTime;
		if(state==State.intro)
		{
			transform.Translate(0,-Time.deltaTime,0);
			if(transform.position.y<8){
				state=State.slashing;
				timer=1.5f;
				slash.position=player.position;
				slashrot.z=Random.Range(-45f,45f);
				slashren.transform.eulerAngles=slashrot;
			}
		}
		else if(state==State.wating)
		{
			time+=Time.deltaTime;
			transform.Translate(Mathf.Cos(time)*Time.deltaTime,0,0);
			if(timer<=0)
			{
				float f=Random.value;
				if(f>0.5f)state=State.shooting;
				else if(f>0.2f)state=State.calling;
				else state=State.slashing;
				timer=1.5f;
				slash.position=player.position;
				slashrot.z=Random.Range(-45f,45f);
				slashren.transform.eulerAngles=slashrot;
			}
		}
		else if(state==State.shooting)
		{
			state=State.wating;
			timer=1;
			Shoot(left);
			Shoot(right);
		}
		else if(state==State.slashing)
		{
			if(timer>1f)
			{
				slashcol.a=(1.5f-timer)*2;
				darkcol.a=1;
			}
			else if(timer>0.5f)
			{
				slashscl.y=(1-timer)*200;
			}
			else if(timer>0)
			{
				slashcol=Color.red;
				slashcod.enabled=true;
			}
			else
			{
				state=State.wating;
				timer=1;
				slashcol=Color.white;
				darkcol.a=0;
				slashscl.y=0;
				slashcod.enabled=false;
			}
			slashren.color=slashcol;
			darkhren.color=darkcol;
			slash.localScale=slashscl;
		}
		else if(state==State.calling)
		{
			state=State.wating;
			Bat();
			timer=1;
		}
	}
	void Shoot(Vector3 v)
	{
		GameObject go = new GameObject("enemybullet");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.boss3[3];
		go.AddComponent<BoxCollider2D>();
		go.AddComponent<Bullet>().owner=name;
		go.transform.position=transform.position+v;
		go.transform.up=-transform.up;
		go.transform.localScale=Vector3.one*5;
	}
	void Bat()
	{
		GameObject go = new GameObject("enemy");
		go.AddComponent<Bat>().target=head.position+(player.position-head.position)*3f;
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.bat[0];
		go.AddComponent<BoxCollider2D>();
		Rigidbody2D r = go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		go.transform.position=head.position;
	}
}
