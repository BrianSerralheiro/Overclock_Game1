using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
	protected int points;
	protected int hp=8;
	public static Transform player;
	private float damageTimer;
	protected SpriteRenderer _renderer;

	protected void Start()
	{
		_renderer = GetComponent<SpriteRenderer>();
	}
	public void SetHP(int i)
	{
		hp=i;
	}
	public void Update()
	{
		if(damageTimer > 0)
		{
			damageTimer -= Time.deltaTime;
			_renderer.color = Color.Lerp(Color.white,Color.red,damageTimer);
		}
	}
	public void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.name=="enemybullet") return;
		if(col.gameObject.name=="enemy") return;
		int i=1;
		Bullet bull=col.gameObject.GetComponent<Bullet>();
		if(bull)i=bull.damage;
		hp-=i;
		if(hp<=0)Die();
		damageTimer = 1;
	}
	protected virtual void Die()
	{
		Destroy(gameObject);
		if(hp<=0)
		{
			EnemySpawner.points+=points;
			if(Random.value <= 0.5)
			{
				GameObject go = new GameObject("ItemDrop");
				go.AddComponent<SpriteRenderer>();
				go.AddComponent<ItemDrop>();
				go.transform.position = transform.position;
			}
		}
	}
	public virtual void Position(int i)
	{
		if(i<3)
		{
			transform.position=new Vector3(1+i*1.5f,11,0);
		}
		else
		{
			transform.position=new Vector3(i%2>0 ? -1 : 6,(10-(i-1)/2*2.5f),0);
		}
	}
	private void OnDestroy()
	{
		
	}
}
