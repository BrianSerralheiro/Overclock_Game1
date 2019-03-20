using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMouth : MonoBehaviour {
	private Transform mouthL;
	private Transform mouthR;
	private Vector3 vector=new Vector3();
	// Use this for initialization
	void Start () {
		GameObject go=new GameObject("mouthL");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.mouth[0];
		mouthL=go.transform;
		go=new GameObject("mouthR");
		go.AddComponent<SpriteRenderer>().sprite=SpriteBase.I.mouth[1];
		mouthR=go.transform;
		mouthL.parent=transform;
		mouthR.parent=transform;
		mouthL.localPosition=new Vector3(0.2f,-0.5f,0.1f);
		mouthR.localPosition=new Vector3(-0.2f,-0.5f,0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		vector.Set(0,0,Mathf.PingPong(Time.time*100,45f));
		mouthL.localEulerAngles=vector;
		mouthR.localEulerAngles=-vector;
	}
}
