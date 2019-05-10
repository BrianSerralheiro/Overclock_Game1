using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Header : EnemyBase {
	private Vector3[] dir={Vector3.right,Vector3.down,Vector3.left};
	private int  prev=1;
	private Vector3 rot;
	private float timer;
	new void Start () {
		base.Start();
		explosionID = 9;
		hp=50;
		points=180;
		GameObject go = new GameObject("head");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.header[0];
		go.transform.parent=transform;
		go.transform.position=transform.position;
		go.transform.localEulerAngles=new Vector3(0f,90,0);
	}
	
	new void Update () {
		if(Ship.paused) return;
		base.Update();
		timer+=Time.deltaTime;
		if(timer>1)
		{
			timer=0;
			int i=0;
			do
			{
				i=Random.Range(0,3);
			}while(i==prev);
			prev=i;
			Shoot();
		}
		rot.Set(0,90*timer,0);
		transform.eulerAngles=rot;
		transform.Translate(dir[prev]*Time.deltaTime*3);
		if(transform.position.x<-Scaler.sizeX/2f-2 || transform.position.x>Scaler.sizeX/2f+2 || transform.position.y<-Scaler.sizeY)Destroy(gameObject);
	}
	void Shoot()
	{
		GameObject go = new GameObject("enemybullet");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.header[1];
		go.AddComponent<BoxCollider2D>();
		go.AddComponent<Bullet>().owner=transform.name;
		go.transform.position=transform.position;
		Vector3 rotation = new Vector3(0,0,Mathf.Atan2(transform.position.x-player.position.x,player.position.y-transform.position.y)*Mathf.Rad2Deg);
		go.transform.eulerAngles=rotation;
	}

}
