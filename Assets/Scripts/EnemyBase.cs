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
		if(col.gameObject.name=="enemybullet") return;
		if(col.gameObject.name=="enemy") return;
		if(--hp<=0)gameObject.SetActive(false);
	}
	public virtual void Position(int i)
	{
		hp=1;
		gameObject.SetActive(true);
		if(i<3)
		{
			transform.position=new Vector3(i*2.5f,11,0);
		}
		else
		{
			transform.position=new Vector3(i%2>0 ? -1 : 6,(10-(i-1)/2*2.5f),0);
		}
	}
	void OnDestroy()
	{
		if(hp<=0) EnemySpawner.points+=points;
	}
}
