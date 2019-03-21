using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
	int points;
	protected int hp=8;
	public static int count;
	private float damageTimer;
	protected SpriteRenderer _renderer;

	protected void Start()
	{
		_renderer = GetComponent<SpriteRenderer>();
	}

	public void Update()
	{
		if(damageTimer > 0)
		{
			damageTimer -= Time.deltaTime;
			_renderer.color = Color.Lerp(Color.white,Color.red,damageTimer);
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.name=="enemybullet") return;
		if(col.gameObject.name=="enemy") return;
		int i=0;
		Bullet bull=col.gameObject.GetComponent<Bullet>();
		if(bull)i=bull.damage;
		hp-=i;
		if(hp<=0)Destroy(gameObject);
		damageTimer = 1;
	}
	public virtual void Position(int i)
	{
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
		if(hp<=0)
		{
			EnemySpawner.points+=points;
			if(Random.value <= 0.5)
			{
				GameObject go = new GameObject("ItemDrop");
				go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.item;
				go.AddComponent<BoxCollider2D>().isTrigger = true;
				go.AddComponent<ItemDrop>();
				go.transform.position = transform.position;
			}
		}
	}
}
