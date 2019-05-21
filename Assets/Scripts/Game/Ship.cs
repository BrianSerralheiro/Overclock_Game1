 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour {
	float shoottimer;
	[SerializeField]
	private Gun[] guns;
	[SerializeField]
	Transform burst;
	[SerializeField]
	private float firerate=0.5f;
	[SerializeField]
	private int maxhp = 1;
	private int hp;
	private float width=Screen.width;
	private float height=Screen.height;
	[SerializeField]
	private Vector3 moveto;
	[SerializeField]
	private Vector3 offset;
	[SerializeField]
	private float speed=5f;
	public static bool paused;
	private GameObject shield;

	private float damageTimer;

	private SpriteRenderer _renderer;

	private int Level = 1;

	private bool playerSpecial;

	[SerializeField]
	private int id;
	public static int playerID;

	public float immuneTime;

	void Start()
	{
		InGame_HUD.shipHealth = 1;
		InGame_HUD._special = 0;
		//speed=5f;
		//OnLevel(1);
		if(playerID != id)
		{
			gameObject.SetActive(false);
			return;
		}
		EnemyBase.player=transform;
		hp=maxhp;
		_renderer = GetComponent<SpriteRenderer>();
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(immuneTime > 0) return;
		if(col.gameObject.name=="playerbullet") return;
		if(col.otherCollider.name=="laser") return;
		if(shield)
		{
			Destroy(shield);
			return;
		}
		if(--hp<=0)
		{
			paused = true;
			gameObject.SetActive(false);
			Continue.Open(this);
		} 
		InGame_HUD.shipHealth = (float)hp / (float)maxhp;
		damageTimer = 1;
	}
	public void Heal()
	{
		gameObject.SetActive(true);
		immuneTime = 3;
		hp=maxhp;
		InGame_HUD.shipHealth =1;
	}
	public void Shield()
	{
		if(shield)return;
		GameObject go=new GameObject("shield");
		shield=go;
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.shield;
		go.transform.parent=transform;
		go.transform.localPosition=new Vector3(0,0,-0.1f);
	}
	void Update()
	{
		if(paused)
		{
			return;
		}
		if(immuneTime > 0)
		{
			immuneTime -= Time.deltaTime;
		}
		if(Bullet.bulletTime<=0)
		{
			Bullet.bulletTime=0.3f;
			Bullet.blink=!Bullet.blink;
		}
		Bullet.bulletTime-=Time.deltaTime;
		if(damageTimer > 0)
		{
			damageTimer -= Time.deltaTime;
			_renderer.color = Color.Lerp(Color.white,Color.red,damageTimer);
		}
		if(Input.GetKeyDown(KeyCode.Alpha1))OnLevel(1);
		if(Input.GetKeyDown(KeyCode.Alpha2))OnLevel(2);
		if(Input.GetKeyDown(KeyCode.Alpha3))OnLevel(3);
		moveto.Set(Input.mousePosition.x/width*Scaler.sizeX-Scaler.x,Input.mousePosition.y/height*Scaler.sizeY*2f-Scaler.sizeY,-0.1f);

		if(Input.GetMouseButtonDown(0))
		{
			if(Mathf.Abs(moveto.x-transform.position.x)>1 || Mathf.Abs(moveto.y-transform.position.y)>1)
				offset.Set(0,0,0);
			else offset=transform.position-moveto;
		}
		if(shoottimer>0) shoottimer-=Time.deltaTime;
		if(Input.GetMouseButton(0))
		{
			if(shoottimer<=0)
			{
				shoottimer=firerate;
				foreach(Gun gun in guns)
				{
					gun.Shoot();
				}
			}
			float f= moveto.y+offset.y-transform.position.y;
			Vector3 v= burst.localScale;
			v.Set(1,f>0.1?5:f<-0.1?1:3,1);
			burst.localScale=v;
			f = moveto.x+offset.x-transform.position.x;
			v = transform.localEulerAngles;
			v.Set(0,f>0.5 ? -35f : f<-0.5 ? 35f : 0,0);
			transform.localEulerAngles=v;
			if(Mathf.Abs(moveto.x+offset.x-transform.position.x)>0.1f || Mathf.Abs(moveto.y+offset.y-transform.position.y)>0.1f)
			 transform.Translate((moveto+offset-transform.position).normalized*speed*Time.deltaTime);
			if(offset==Vector3.zero && !(Mathf.Abs(moveto.x-transform.position.x)>1 || Mathf.Abs(moveto.y-transform.position.y)>1))
				offset=transform.position-moveto;
			v=transform.position;
			v.z=-0.1f;
			transform.position=v;
		}
	}
	void OnLevel(int i)
	{
		foreach(Gun gun in guns)
		{
			gun.Level(i);
		}
	}

	public void OnLevel()
	{
		OnLevel(++Level);
	}


	public void Special()
	{
		playerSpecial = true;
	}
}
