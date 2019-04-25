using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : EnemyBase {
	private Vector3 pos;
	private float speed;
	new void Start () {
		base.Start();
		hp=5;
		pos=transform.position;
	}
	
	new void Update () {
		base.Update();
		pos=Vector3.MoveTowards(pos,player.position,Time.deltaTime*speed);
		speed+=Time.deltaTime;
		if(speed>8)speed=5;
		transform.position=pos;
		transform.Rotate(0,0,Time.deltaTime*90);
	}
	public new void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.name.Contains("Ship")) Explode();
		base.OnCollisionEnter2D(col);
	}
	protected override void Die()
	{
		Destroy(gameObject);
	}
	private void Explode()
	{
		Destroy(gameObject);
		Debug.Log("BOOM");
	}
}
