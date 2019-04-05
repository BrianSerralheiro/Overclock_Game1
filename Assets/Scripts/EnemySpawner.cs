using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public Transform player;
	public Material bg;
	public string wave;
	public static int points;
	private int counter;
	public float timer;

//A0A1A2t5B8B3t5C0C1C2t8A0t1A1t1A2t1A4t1A6t1A8t5D0D2
	void Start()
	{
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
				en=Reuse<Shooter>(SpriteBase.I.shooter);
				break;
			case 'B':
				en=Reuse<Diver>(SpriteBase.I.diver);
				break;
			case 'C':
				en=Reuse<Carrier>(SpriteBase.I.carrier);
				break;
			case 'D':
				en=Reuse<Boss1>(SpriteBase.I.boss1[0]);
				break;
			case 'E':
				en=Reuse<Grabber>(SpriteBase.I.grabber[0]);
				break;
			case 'F':
				en=Reuse<Round>(SpriteBase.I.round[0]);
				break;
			case 'G':
				en=Reuse<Lasor>(SpriteBase.I.Lasor[0]);
				break;
			case 'I':
				en=Reuse<Boss2>(SpriteBase.I.boss2[0]);
				break;
			case 'J':
				en=Reuse<Bat>(SpriteBase.I.bat[0]);
				break;
			case 'K':
				en=Reuse<BatGirl>(SpriteBase.I.batgirl[0]);
				break;
			case 'L':
				en=Reuse<Header>(SpriteBase.I.header[0]);
				break;
			default :
				timer=s[1]-48;
				break;
		}
		if(en) en.Position(s[1]-48);
	}
	EnemyBase Reuse<t>(Sprite sp)where t :EnemyBase
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
