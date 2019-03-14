using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private float timer;
	public int time=1;
	public void Position(Transform t)
	{
		gameObject.SetActive(true);
		transform.position=t.position+t.up*30;
		transform.up=t.up;
		timer=time;
	}

	void Update()
	{
		if(Ship.paused) return;
		transform.Translate(0,Time.deltaTime*100,0);
		timer-=Time.deltaTime;
		if(timer<=0) gameObject.SetActive(false);
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		gameObject.SetActive(false);
	}
	
}
