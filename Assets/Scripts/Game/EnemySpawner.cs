using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ADs;

public class EnemySpawner : MonoBehaviour {
	public Material bg;
	[TextArea]
	public string wave;
	public static int points;
	public static bool boss;
	private int counter;
	public float timer;
	[SerializeField]
	private Texture[] mundos;
	private int worldID;
	private int scroll=60;
	private float transfer=1;

	//A3T1A4T2A0T1A7T3B3T1B4T5A0A1A2A3A4A5A6A7T1B0B7T2B3T1B4T2C0T3C8A1A3A4A6T9T9T9D0T2E3E4T2G0T3F0F2F5F7T9F1F3F4F6T9E8E9T3G1T2H7T9T9I0T5J0J7T3J8J9T2J3T1J4T5K0T3L7T1L3T1L4T5J1J3J4J6T9T9M0T5N0N7T3N3N4T5P8T5Q0T5Q7P0T5O3T1O4T2N2N3N4N5T9T9R0
	
	/*
	Level Design
	World 1: 
	Stage 1 ID0: V0T1U0T4A1A6T2B2T1B2T1B2T1B2T2C8T9T5A2A5A8A9T3B1T1B0T1B0T1B0T1B0T2A0A3T1B7T1B7T1B7T1B7T4C6A0A7T5A0A3T9C9T9
	Stage 2 ID1: U1T9T1B0B1B2B3B4B5B6B7B8B9T3C7A0A2T5A0A2T5A1A3A4A7A8A9T9B1B2B3B4T9B5B6B7B8B9T9B0B1B2B3B4B5T5B6B7B8B9T4C8C9A1A6T9T4U2T4T4
	Stage 3 ID2: U2B4B5B6B7B9T1A0A1A2T9C8C7T9T2B8B9B3A1A6A8T9B0B2B4B6A0A2B8C9T9A0A4A8B1B8T5C5A3A5B1B7T9V1D0T1T1

	World 2:
	Stage 1 ID3: U3F2F6T5E0E1E2E3E8T4G0G7T5H0H3H7T5T9E0E1E2E3E4E5E6E7E8T9H0H3E1E3E6T9G7E0E1E2T9T9
	Stage 2 ID4: U4F0F7T1F3F4T9E0E2E4E6E8F0F7T9G7E0E1E2E3E4H0T9F0F7E2E3E4H7T9H0H7E2E4E5E6E7T9T9
	Stage 3 ID5: U5G1F2F3F4T9H2H6E0E2E3E4E5T9E1E3E5E7E9T1E0E2E4E6E8T9F0F7T1F1F6T1F2F5T1F3F4T9T4G1E8E9E3H1H6F3T9T9V2I0T9T9

	World 3:
	Stage 1 ID6: U6L0J1J2J3J4J5J6T9T5K4L1L6T9T2J0J1J2J3J4J5J6J7J8J9T9T2J9J8J7J6J5J4J3J2J1J0T9T2J0J2J4J6K5T9K2K6T9T9
	Stage 2 ID7: U7J1J2J3J4J5J6J7J8J9L0L7T9T5L0L4J0J2J4J6J8T9T5K0K7T9T7J9J1J2J3J4J5J6J7J8J9T9J9J8J7J6J5J4J3J2J1J0T9T5L0L2L3L4L5T9T9
	Stage 3 ID8: U8J1J2J3J4J5J6J7J8J9K0T9T4J0J1J2J3J4J5J6J7J8J9T3J9J8J7J6J5J4J3J2J1J0T4J1J2J3J4J5J6J7J8J9T9T4L1L2L3L4L5T9L0L4K5J0J2J4J6J8T5J1J2J3J4K0K4T5K2J1J2J3J4J5J6J7T9V3M0T9T9

	World 4:
	Stage 1 ID9  :
	Stage 2 ID;  :
	Stage 3 ID:  :
	Final Stage:
	 */
	void Start()
	{
		boss = false;
		Boss4.last=false;
		SoundManager.Play(1);
		bg.mainTexture=mundos[worldID];
		bg.mainTextureOffset=bg.mainTextureOffset=Vector2.one;
		bg.color=Color.white;
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
		//if(counter>=wave.Length)SceneManager.LoadScene(1);
		if(timer>0 && !boss) timer-=Time.deltaTime;
		Vector2 v= bg.mainTextureOffset;
		v.y+=Time.deltaTime/scroll;
		if(worldID==2)	
		if(v.y>0.7f) v.y=0.7f;
		else
			if(v.y>1) v.y-=1;
		bg.mainTextureOffset=v;
		if(transfer<1)
		{
			transfer-=Time.deltaTime*2;
			float t=Mathf.Abs(transfer);
			bg.color=new Color(t,t,t);
			if(transfer<0 && bg.mainTexture!=mundos[worldID]){
				bg.mainTexture= mundos[worldID];
				bg.mainTextureOffset=Vector2.zero;
			}
			if(transfer<-1)transfer=1;
		}
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
				en=Spawn<Drone>(SpriteBase.I.drone[Ship.playerID]);
				break;
			case 'U':
				SoundManager.PlayEffects(22);
				Transition.Timer = 5;
				Transition.worldID = s[1]-48;
				if(worldID!=Transition.worldID/3)
				{
					worldID=Transition.worldID/3;
					transfer-=Time.deltaTime;
					scroll=worldID==2?scroll=300:60;
				}
				break;
			case 'V':
				DialogBox.Text(s[1]-48);
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
