using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public Bullet[] bulletPool;
	public Shooter[] shooter;
	public Shooter2[] shooter2;
	public Diver[] diver;
	public Wall[] wall;
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
				en=Reuse(shooter);
				break;
			case 'B':
				en=Reuse(diver);
				break;
			case 'C':
				en=Reuse(wall);
				break;
			case 'D':
				en=Reuse(shooter2);
				break;
			default :
				timer=s[1]-48;
				break;
		}
		if(en) en.Position(s[1]-48);
	}
	EnemyBase Reuse(EnemyBase[]  en){
	for(int i=0;i<en.Length;i++){
		if(!en[i].gameObject.activeSelf){
			en[i].gameObject.SetActive(true);
			return en[i];
		}
	}
	Debug.Log("not enough enemies on the pool");
	return null;
}
}
