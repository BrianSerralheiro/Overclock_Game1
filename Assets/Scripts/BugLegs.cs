using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugLegs : MonoBehaviour {
	private Transform legL;
	private Transform legR;
	private Vector3 vector = new Vector3();
	void Start()
	{
		GameObject go = new GameObject("legL");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.legs[0];
		legL=go.transform;
		go=new GameObject("legR");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.legs[1];
		legR=go.transform;
		legL.parent=transform;
		legR.parent=transform;
		legL.localPosition=new Vector3(0.05f,0.5f,0.1f);
		legR.localPosition=new Vector3(-0.05f,0.5f,0.1f);
	}
	void Update()
	{
		vector.Set(0,0,Mathf.PingPong(Time.time*200,45f));
		legL.localEulerAngles=-vector;
		legR.localEulerAngles=vector;
	}
}
