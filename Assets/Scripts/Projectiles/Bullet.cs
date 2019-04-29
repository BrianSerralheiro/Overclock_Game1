using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private float timer;
	public string owner;
	public int damage;
	public bool pierce;
	private void Start()
	{
		timer=2;
	}
	void Update()
	{
		if(Ship.paused) return;
		ParticleManager.Emit(2,transform.position,1);
		transform.Translate(0,Time.deltaTime*14,0);
		timer-=Time.deltaTime;
		if(timer<=0) Destroy(gameObject);
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.name!=owner && !pierce){
			Destroy(gameObject);
			ParticleManager.Emit(1,transform,5);
		}
	}
	
}
