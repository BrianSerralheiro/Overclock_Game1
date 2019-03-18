using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public Bullet[] bulletPool;
	public Sprite shooter;
	public Sprite shooter2;
	public Sprite diver;
	public Sprite wall;
	public Transform player;
	public UnityEngine.UI.RawImage bg;
	public string wave;
	public static int points;
	private int counter;
	public float timer;

//A0A1A2t5B8B3t5C0C1C2t8A0t1A1t1A2t1A4t1A6t1A8t5D0D2
	void Start()
	{
		points=0;
		Shooter.bulletPool=bulletPool;
		Shooter.player=player;
		Diver.player=player;
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
		Rect rect= bg.uvRect;
		rect.y+=Ship.speed*Time.deltaTime;
		if(bg.uvRect.y>1) rect.y-=1;
		bg.uvRect=rect;

	}
	void Chose(string s)
	{
		EnemyBase en=null;
		switch(s[0])
		{
			case 'A':
				en=Reuse<Shooter>(shooter);
				break;
			case 'B':
				en=Reuse<Diver>(diver);
				break;
			case 'C':
				en=Reuse<Wall>(wall);
				break;
			case 'D':
				en=Reuse<Shooter2>(shooter2);
				break;
			default :
				timer=s[1]-48;
				break;
		}
		if(en) en.Position(s[1]-48);
	}
	EnemyBase Reuse<t>(Sprite sp)where t :EnemyBase{
	GameObject go=new GameObject("enemy");
	go.AddComponent<SpriteRenderer>().sprite=sp;
	go.AddComponent<BoxCollider2D>();
	go.AddComponent<Rigidbody2D>().gravityScale=0;
		return go.AddComponent<t>();
}
}
