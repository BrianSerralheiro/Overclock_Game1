using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun {
	[SerializeField]
	private Sprite[] lasersprite;
	private bool blink;
	private Transform laser;
	private Vector3 scale;
	private float timer;
	private Collider2D col;
	private SpriteRenderer ren;

	[SerializeField]
	private Transform particles;

	void Start () {
		GameObject go=new GameObject("laser");
		laser=go.transform;
		laser.position=transform.position;
		laser.parent=transform;
		laser.localScale=new Vector3(1,7);
		ren=go.AddComponent<SpriteRenderer>();
		ren.sprite=lasersprite[(Bullet.blink ? 0 : 1)];
		col=go.AddComponent<BoxCollider2D>();
		ren.enabled=false;
		scale=Vector3.up*7 + Vector3.forward;
	}
	public override void Shoot()
	{
		col.enabled=!col.enabled;
		if(scale.x<level)scale.x+=1f;
		if(scale.x>level)scale.x=level;
		if(timer<1)timer+=0.05f;
		if(timer>1)timer=1;
		if(timer<0)timer=0;
		blink=!blink;
		ren.sprite=lasersprite[Mathf.RoundToInt(timer)*2+(Bullet.blink ? 0 : 1)];
		ParticleManager.Emit(6, transform.position, 1);
	}
	public override void Level(int i)
	{
		if(i<4) laser.localScale+=Vector3.right;
	}
	void Update () {
		//particles.localScale=scale;
		//laser.localScale=scale;
		//timer=col.enabled?1:0;
		ren.enabled=timer>0;
		ren.sprite=lasersprite[Mathf.RoundToInt(timer)*2+(Bullet.blink ? 0 : 1)];
		if(timer>0)timer-=Time.deltaTime*2;
		if(scale.x>0)scale.x-=Time.deltaTime*10;
		if(scale.x<0)scale.x=0;
	}
}
