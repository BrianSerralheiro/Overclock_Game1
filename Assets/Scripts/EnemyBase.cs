using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
	int points;
	protected int hp=1;
	public static int count;
	void Start()
	{
		//Position(count++);
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(--hp<=0) gameObject.SetActive(false);
	}
	public virtual void Position(int i)
	{
		hp=1;
		gameObject.SetActive(true);
		if(i<3)
		{
			transform.position=new Vector3(i*128,550,0);
		}
		else
		{
			transform.position=new Vector3(i%2>0 ? -64 : 320,(512-(i-1)/2*128),0);
		}
	}
	void OnDisable()
	{
		if(hp<=0) EnemySpawner.points+=points;
	}
}
