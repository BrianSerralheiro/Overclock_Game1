using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour {
//	public Transform player;
	public Material bg;
	[TextArea]
	public string wave;
	public static int points;
	public static bool boss;
	private int counter;
	public float timer;

	//A3T1A4T2A0T1A7T3B3T1B4T5A0A1A2A3A4A5A6A7T1B0B7T2B3T1B4T2C0T3C8A1A3A4A6T9T9T9D0T2E3E4T2G0T3F0F2F5F7T9F1F3F4F6T9E8E9T3G1T2H7T9T9I0T5J0J7T3J8J9T2J3T1J4T5K0T3L7T1L3T1L4T5J1J3J4J6T9T9M0T5N0N7T3N3N4T5P8T5Q0T5Q7P0T5O3T1O4T2N2N3N4N5T9T9R0
	void Start()
	{
		SoundManager.Play(1);
		points=0;
		//EnemyBase.player=player;
	}

	void OnDestroy()
	{
		Cash.totalCash += points/200;
		PlayerPrefs.SetInt("cash", Cash.totalCash);
	}

	void Update()
	{
		if(Ship.paused) return;
		do
		{
			if(timer<=0 && counter<wave.Length && !boss)
			{
				Chose(wave.Substring(counter,2));
				counter+=2;
			}
		}
		while(timer<=0 && counter<wave.Length && !boss);
		if(counter>=wave.Length)SceneManager.LoadScene(0);
		if(timer>0 && !boss) timer-=Time.deltaTime;
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
				en=Spawn<Shooter>(SpriteBase.I.shooter[0]);
				break;
			case 'B':
				en=Spawn<Diver>(SpriteBase.I.diver[0]);
				break;
			case 'C':
				en=Spawn<Carrier>(SpriteBase.I.carrier[0]);
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
