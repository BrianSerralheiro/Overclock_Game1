using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun {
	[SerializeField]
	private Sprite[] lasersprites;
	private Sprite[] lasersprite=new Sprite[4];
	private Transform laser;
	private float timer;
	private Collider2D col;
	private SpriteRenderer ren;
	

	void Start () {
		int f=0;
		if(Ship.skinID!=-1 && Locks.Skin(6+Ship.skinID))f=(Ship.skinID+1)*4;
		for(int i = 0; i<lasersprite.Length; i++)
		{
			lasersprite[i]=lasersprites[f+i];
		}
		lasersprites=null;
		GameObject go=new GameObject("laser");
		laser=go.transform;
		laser.position=transform.position;
		laser.parent=transform;
		laser.localScale=new Vector3(1,7);
		ren=go.AddComponent<SpriteRenderer>();
		ren.sprite=lasersprite[(Bullet.blink ? 0 : 1)];
		col=go.AddComponent<BoxCollider2D>();
		ren.enabled=false;
	}
	public override void Shoot()
	{
		col.enabled=!col.enabled;
		timer+=Time.deltaTime*10;
		if(timer>2)timer=2;
		if(timer<0)timer=0;
		ren.sprite=lasersprite[(timer>1?2:0)+(Bullet.blink ? 0 : 1)];
		ParticleManager.Emit(6, transform.position, 1);
	}
	public override void Level(int i)
	{
		if(i<4) laser.localScale=Vector3.up*7 +Vector3.right*i;
	}
	void Update(){
		ren.enabled=timer>0;
		ren.sprite=lasersprite[(timer>1 ? 2 : 0)+(Bullet.blink ? 0 : 1)];
		if(timer>0)timer-=Time.deltaTime*5f;
	}
}
