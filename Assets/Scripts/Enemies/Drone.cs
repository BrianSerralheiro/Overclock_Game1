using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : EnemyBase {
	private int id;
	private Vector3 dir=Vector3.right;
	new void Start () {
		base.Start();
		hp=80;
		id=Random.Range(0,3);
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
		if(transform.position.y>7)transform.Translate(0,-Time.deltaTime,0);
		else transform.Translate(dir*(id==1?-1:1)*Time.deltaTime);
		if(transform.position.x<-1 || transform.position.x>6)Die();
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
