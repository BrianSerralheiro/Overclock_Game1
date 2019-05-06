using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : EnemyBase {
	private int id;
	private Vector3 dir=Vector3.right;
	new void Start () {
		base.Start();
		explosionID = 1;
		hp=80;
		id=Random.Range(0,3);
	}
	
	// Update is called once per frame
	new void Update () {
		if(Ship.paused) return;
		base.Update();
		if(transform.position.y>Scaler.sizeY/2)transform.Translate(0,-Time.deltaTime,0);
		else transform.Translate(dir*(id==1?-1:1)*Time.deltaTime);
		if(transform.position.x<-Scaler.sizeX/2f-1 || transform.position.x>Scaler.sizeX/2f+1)Die();
	}
	public override void Position(int i)
	{
		base.Position(i%8);
	}
	protected override void Die()
	{
		Destroy(gameObject);
		if(hp<=0)
		{
			EnemySpawner.points+=points;
			GameObject go = new GameObject("ItemDrop");
			go.AddComponent<SpriteRenderer>();
			ItemDrop item= go.AddComponent<ItemDrop>();
			item.Set(id);
			go.transform.position = transform.position;
		}
	}
}
