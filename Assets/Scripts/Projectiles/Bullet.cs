using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private float timer;
	public int time=1;
	public string owner;
	public int damage;
	public bool pierce;
	private void Start()
	{
		timer=1;
	}
	void Update()
	{
		if(Ship.paused) return;
		transform.Translate(0,Time.deltaTime*10,0);
		timer-=Time.deltaTime;
		if(timer<=0) Destroy(gameObject);
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.name!=owner && !pierce)Destroy(gameObject);
	}
	
}
