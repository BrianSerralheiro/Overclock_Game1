using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	public LineRenderer line;
	public Vector3[] pos;
	// Use this for initialization
	void Start () {
		line=GetComponent<LineRenderer>();
		pos=new Vector3[line.positionCount];
		line.GetPositions(pos);
	}
	
	// Update is called once per frame
	void Update () {
		//float f=Mathf.Sin(Time.time);
		for(int i = 0; i<pos.Length;i++)
		{
			Vector3 v=line.GetPosition(i);
			v.x=pos[i].x+Random.value*0.3f;
			v.y=pos[i].y+Random.value*0.3f;
			line.SetPosition(i,v);
		}
		//line.SetPositions(pos);
	}
}
