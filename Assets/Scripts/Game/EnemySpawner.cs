using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public Transform player;
	public Material bg;
	[TextArea]
	public string wave;
	public static int points;
	private int counter;
	public float timer;

	//J1T1J0T1J2T3K1T9L1T6J1J0J2T9M1
	void Start()
	{
		SoundManager.Play(1);
		points=0;
		EnemyBase.player=player;
	}

	void OnDestroy()
	{
		Cash.totalCash += points/100;
		PlayerPrefs.SetInt("cash", Cash.totalCash);
	}

	void Update()
	{
		if(!player.gameObject.activeSelf) Application.Quit();
		if(Ship.paused) return;
		do
		{
			if(timer<=0 && counter<wave.Length)
			{
				Chose(wave.Substring(counter,2));
				counter+=2;
			}
		}
		while(timer<=0 && counter<wave.Length);
		if(counter==wave.Length) Application.Quit();
		if(timer>0) timer-=Time.deltaTime;
		Vector2 v= bg.mainTextureOffset;
		v.y+=Time.deltaTime;
		if(v.y>1) v.y-=1;
		bg.mainTextureOffset=v;

	}

	void Chose(string s)
	{
		EnemyBase en=null;
		switch(s[0])
		{
			case 'A':
				en=Spawn<Shooter>(SpriteBase.I.shooter);
				break;
			case 'B':
				en=Spawn<Diver>(SpriteBase.I.diver);
				break;
			case 'C':
				en=Spawn<Carrier>(SpriteBase.I.carrier);
				break;
			case 'D':
				en=Spawn<Boss1>(SpriteBase.I.boss1[0]);
				break;
			case 'E':
				en=Spawn<Grabber>(SpriteBase.I.grabber[0]);
				break;
			case 'F':
				en=Spawn<Round>(SpriteBase.I.round[0]);
				break;
			case 'G':
				en=Spawn<Lasor>(SpriteBase.I.Lasor[0]);
				break;
			case 'H':
				en=Spawn<Launcher>(SpriteBase.I.launcher[0]);
				break;
			case 'I':
				en=Spawn<Boss2>(SpriteBase.I.boss2[0]);
				break;
			case 'J':
				en=Spawn<Bat>(SpriteBase.I.bat[0]);
				break;
			case 'K':
				en=Spawn<BatGirl>(SpriteBase.I.batgirl[0]);
				break;
			case 'L':
				en=Spawn<Header>(SpriteBase.I.header[0]);
				break;
			case 'M':
				en=Spawn<Boss3>(SpriteBase.I.boss3[0]);
				break;
			case 'N':
				en=Spawn<Invader>(SpriteBase.I.invader[0]);
				break;
			case 'O':
				en=Spawn<Slasher>(SpriteBase.I.slasher[0]);
				break;
			case 'P':
				en=Spawn<Bomber>(SpriteBase.I.bomber[0]);
				break;
			case 'Q':
				en=Spawn<Zapper>(SpriteBase.I.zapper[0]);
				break;
			case 'R':
				en=Spawn<Boss4>(SpriteBase.I.boss4[0]);
				break;
			case 'S':
				en=Spawn<Drone>(SpriteBase.I.drone[SuaperTest.id]);
				break;
			default :
				timer=s[1]-48;
				break;
		}
		if(en) en.Position(s[1]-48);
	}
	EnemyBase Spawn<t>(Sprite sp)where t :EnemyBase
	{
		GameObject go=new GameObject("enemy");
		go.AddComponent<SpriteRenderer>().sprite=sp;
		go.AddComponent<BoxCollider2D>();
		Rigidbody2D r= go.AddComponent<Rigidbody2D>();
		r.isKinematic=true;
		r.useFullKinematicContacts=true;
		return go.AddComponent<t>();
	}
}
