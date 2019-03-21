using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diver : EnemyBase
{
	public static Transform player;
	private Vector3 rotation=Vector3.zero;
	private Transform mouthL;
	private Transform mouthR;
	private Transform legL;
	private Transform legR;
	private Vector3 vector = new Vector3();

	public void Start()
	{
		base.Start();
		hp=12;
		if(legL)return;
		GameObject go = new GameObject("legL");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.legs[0];
		legL=go.transform;
		go=new GameObject("legR");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.legs[1];
		legR=go.transform;
		go=new GameObject("mouthL");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.mouth[0];
		mouthL=go.transform;
		go=new GameObject("mouthR");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.mouth[1];
		mouthR=go.transform;
		mouthL.parent=mouthR.parent=legL.parent=legR.parent=transform;
		mouthL.localPosition=new Vector3(0.2f,-0.5f,0.1f);
		mouthR.localPosition=new Vector3(-0.2f,-0.5f,0.1f);
		legL.localPosition=new Vector3(0.05f,0.5f,0.1f);
		legR.localPosition=new Vector3(-0.05f,0.5f,0.1f);
	}
	void Update()
	{
		base.Update();
		if(Ship.paused) return;
		rotation.z=Mathf.Atan2(transform.position.x-player.position.x,player.position.y-transform.position.y)*Mathf.Rad2Deg+180;
		transform.eulerAngles=rotation;
		transform.Translate(0,-3*Time.deltaTime,0);

		vector.Set(0,0,Mathf.PingPong(Time.time*100,45f));
		mouthL.localEulerAngles=vector;
		mouthR.localEulerAngles=-vector;
		legL.localEulerAngles=-vector*2;
		legR.localEulerAngles=vector*2;
	}
}
