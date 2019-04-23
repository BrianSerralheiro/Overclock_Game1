using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
	protected int points;
	protected int hp=8;
	public static Transform player;
	protected float damageTimer;
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
			InGame_HUD._special += 0.01f;
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
		if(i<8)
		{
			transform.position=new Vector3(i-3.5f,Scaler.sizeY+2,0);
		}
		else
		{
			transform.position=new Vector3(i==8 ? -Scaler.sizeX/2f-1 : Scaler.sizeX/2+1,Scaler.sizeY-2,0);
		}
	}
	private void OnDestroy()
	{
		
	}
}
