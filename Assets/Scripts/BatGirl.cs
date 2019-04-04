using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatGirl : EnemyBase {

	private Transform wingL;
	private Transform wingR;
	private SpriteRenderer render;
	private Sprite left;
	private Sprite closed;
	private Vector3 vector = new Vector3();
	private Vector3 pos = new Vector3(0.35f,0.66f,0.1f);
	private float timer;
	new void Start () {
		base.Start();
		hp=50;
		GameObject go = new GameObject("wingL");
		render=go.AddComponent<SpriteRenderer>();
		left=SpriteBase.I.batgirl[2];
		closed=SpriteBase.I.batgirl[4];
		render.sprite=left;
		wingL=go.transform;
		go = new GameObject("wingR");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.batgirl[3];
		wingR=go.transform;
		wingL.parent=wingR.parent=transform;
		wingL.localPosition=new Vector3(0.35f,0.66f,0.1f);
		wingR.localPosition=new Vector3(-0.35f,0.66f,0.1f);
	}
	
	new void Update(){
		base.Update();
		timer-=Time.deltaTime;
		vector.Set(0,0,Mathf.PingPong(Time.time*300,-45f)+60f);
		if(timer<2)
		{
			render.sprite=closed;
			wingR.gameObject.SetActive(false);
			vector.Set(0,0,0);
			pos.z=-0.1f;
		}
		if(timer<0)
		{
			render.sprite=left;
			wingR.gameObject.SetActive(true);
			pos.z=0.1f;
			timer=5;
		}
		wingL.localPosition=pos;
		wingL.localEulerAngles=vector;
		wingR.localEulerAngles=-vector;
	}
}
