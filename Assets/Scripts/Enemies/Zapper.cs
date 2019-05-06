using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zapper : EnemyBase {
	private Transform zap;
	private LineRenderer line;
	private float timer = 5;
	private Vector3 rot = new Vector3();
	private Vector3 scale = Vector3.one;
	new void Start () {
		base.Start();
		explosionID = 3;
		hp=80;
		GameObject go=new GameObject("zap");
		line=go.AddComponent<LineRenderer>();
		line.useWorldSpace=true;
		line.positionCount=5;
		line.widthMultiplier=0.1f;
		BoxCollider2D col= go.AddComponent<BoxCollider2D>();
		col.size=new Vector2(1,12);
		col.offset=new Vector2(0,6f);
		go.transform.parent=transform;
		go.transform.localPosition=new Vector3(0,1,0.1f);
		go.SetActive(false);
		go=new GameObject("energy");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.zapper[1];
		zap=go.transform;
		zap.parent=transform;
		zap.localPosition=new Vector3(0,1,-0.1f);
		zap.gameObject.SetActive(false);
	}
	new void OnCollisionEnter2D(Collision2D col)
	{
		if(col.otherCollider.name=="zap") return;
		base.OnCollisionEnter2D(col);
	}
	new void Update () {
		if(Ship.paused) return;
		base.Update();
		timer-=Time.deltaTime;
		if(timer>1){
			transform.Translate(0,-Time.deltaTime/2,0,Space.World);
			rot.Set(0,0,Mathf.Atan2(transform.position.x-player.position.x,player.position.y-transform.position.y)*Mathf.Rad2Deg);
			transform.eulerAngles=rot;
		}else if(timer >0.1f)
		{
			zap.gameObject.SetActive(true);
			scale.x=scale.y=0.9f-timer;
			zap.localScale=scale;
		}else if(timer >0)
		{
			if(!line.gameObject.activeSelf){
				line.gameObject.SetActive(true);
				Vector3 v=zap.position;
				Vector3 f=transform.up*3;
				line.SetPosition(0,v);
				for(int i = 1; i<4; i++)
				{
					v+=f;
					line.SetPosition(i,v+new Vector3(Random.value/2-0.5f,Random.value/10-0.1f));
				}
				line.SetPosition(4,v+f);

			}
		}
		else
		{
			timer=3;
			line.gameObject.SetActive(false);
			zap.gameObject.SetActive(false);
		}
	}
}
