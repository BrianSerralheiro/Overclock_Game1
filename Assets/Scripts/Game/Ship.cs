 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour {
	float shoottimer;
	[SerializeField]
	private ParticleSystem trail;
	[SerializeField]
	private Color[] colors;
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
	private string[] falas;
	[SerializeField]
	private Sprite[] charPics;
	[SerializeField]
	private Vector3 offset;
	[SerializeField]
	private float speed=5f;
	[SerializeField]
	private Sprite[] skins;
	[SerializeField]
	private int[] ids;
	[SerializeField]
	private Sprite[] shotSkins;
	public static bool paused;
	[SerializeField]
	private Core shield;
	[SerializeField]
	private Material specialMat;
	[SerializeField]
	private Texture2D[] specials;
	private bool shielded;

	private float damageTimer;
	private float freezeTimer;
	private float clickTime=-1;

	private SpriteRenderer _renderer;

	private int Level = 1;
	

	[SerializeField]
	private int id;
	public static int playerID;
	public static int skinID=-1;

	public float immuneTime;

	void Start()
	{
		
		InGame_HUD.shipHealth = 1;
		InGame_HUD._special = 0;
		hp=maxhp;
		_renderer = GetComponent<SpriteRenderer>();
		if(playerID != id)
		{
			gameObject.SetActive(false);
			return;
		}
		specialMat.mainTexture=specials[0];
		DialogBox.Texts(falas);
		DialogBox.Chars(charPics);
		falas=null;
		charPics=null;
		if(skinID!=-1 && Locks.Skin(id*3+skinID))
		{
			_renderer.sprite=skins[skinID];
			ParticleSystem.MainModule main = trail.main;
			main.startColor=colors[skinID];
			for(int i = 0; i<ids.Length; i++)
			{
				SpriteBase.I.bullets[ids[i]]=shotSkins[skinID*ids.Length+i];
			}
			specialMat.mainTexture=specials[skinID+1];
			specialMat=null;
			specials=null;
			shotSkins=null;
			skins=null;
			colors=null;
		}
		EnemyBase.player=transform;
		OnLevel(3);
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(immuneTime > 0) return;
		if(col.gameObject.name=="playerbullet") return;
		if(col.otherCollider.name=="laser") return;
		if(shielded)
		{
			shielded=false;
			return;
		}
		if(--hp<=0)
		{
			SoundManager.PlayEffects(10, 1, 0);
			paused = true;
			gameObject.SetActive(false);
			GameOverController.Open(this);
		}
		else
		{
			SoundManager.PlayEffects(9, 1, 0);
		}
		InGame_HUD.shipHealth = (float)hp / (float)maxhp;
		damageTimer=1;
		immuneTime=1f;
	}
	public void Revive()
	{
		gameObject.SetActive(true);
		immuneTime = 3;
		hp=maxhp;
		InGame_HUD.shipHealth =1;
		paused=false;
	}
	public void Shield()
	{
		shielded=true;
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
		if(freezeTimer > 0)
		{
			freezeTimer -= Time.deltaTime;
			if(freezeTimer<=0)
			{
				_renderer.enabled=true;
				GameObject go = new GameObject("playerbullet");
				go.AddComponent<BoxCollider2D>().size=new Vector2(10,20);
				Bullet bu= go.AddComponent<Bullet>();
				bu.damage=200;
				bu.pierce=true;
				bu.owner=name;
				bu.enabled=false;
				Destroy(go,0.1f);
			}
			return;
		}
		if(Bullet.bulletTime<=0)
		{
			Bullet.bulletTime=0.1f;
			Bullet.blink=!Bullet.blink;
		}
		Bullet.bulletTime-=Time.deltaTime;
		if(damageTimer > 0)
		{
			damageTimer -= Time.deltaTime;
			_renderer.color = Color.Lerp(Color.white,Color.red,damageTimer);
		}
		if(Input.GetKeyDown(KeyCode.Space))Special();
		if(Input.GetKeyDown(KeyCode.Alpha1))OnLevel(1);
		if(Input.GetKeyDown(KeyCode.Alpha2))OnLevel(2);
		if(Input.GetKeyDown(KeyCode.Alpha3))OnLevel(3);
		moveto.Set(Input.mousePosition.x/width*Scaler.sizeX-Scaler.x,Input.mousePosition.y/height*Scaler.sizeY*2f-Scaler.sizeY,-0.1f);
		if(shielded)shield.Add(Time.deltaTime);
		else shield.Min(Time.deltaTime);
		if(Input.GetMouseButtonDown(0))
		{
			if(Time.time<clickTime+0.5f && InGame_HUD._special >= 0)
			{
				InGame_HUD._special=0;
				Special();
			}
			clickTime=Time.time;
			if(Mathf.Abs(moveto.x-transform.position.x)>1 || Mathf.Abs(moveto.y-transform.position.y)>1)
				offset.Set(0,0,0);
			else offset=transform.position-moveto;
		}
		if(shoottimer>0) shoottimer-=Time.deltaTime;
		if(Input.GetMouseButton(0))
		{
			if(shoottimer<=0)
			{
				if(id==2)SoundManager.PlayEffects(4, 10f, Mathf.Epsilon);
				else SoundManager.PlayEffects(2 + id, 0.1f, 0.5f);
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
		SoundManager.PlayEffects(6 + id, 1f, 2f);
		switch(id)
		{
			case 0:
				ParticleManager.Emit(12,Vector3.up*-10,20,1f);
				immuneTime=4f;
				freezeTimer=3f;
				break;
			case 1:
				ParticleManager.Emit(13,Vector3.zero,200);
				immuneTime=4;
				freezeTimer=3;
				break;
			case 2:
				ParticleManager.Emit(14,Vector3.zero,5,1f);
				immuneTime=2;
				freezeTimer=1;
				break;
			case 3:
				ParticleManager.Emit(15,Vector3.zero,20,2f);
				immuneTime=3;
				freezeTimer=2;
				_renderer.enabled=false;
				break;
		}
	}
}
