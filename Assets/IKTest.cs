using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTest : MonoBehaviour {
	public Vector3 elbow;
	public Transform target;
	public Vector3 mid;
	public float distance;
	// Use this for initialization
	void Start () {
		distance=2;
	}
	
	// Update is called once per frame
	void Update () {
		float d=(target.position-transform.position).magnitude;
		mid = (target.position-transform.position)/2+transform.position;
		if(d>distance)
		{
			elbow=mid;

		}
		else
		{
			Vector3 v = transform.position-target.position;
			Vector3 v2=new Vector3();
			v.Normalize();
			v2.x=v.y;
			v2.y=-v.x;
			v2.z=v.z;
			elbow=mid-v2*(distance-d)/2;
		}
		Debug.DrawLine(transform.position,elbow);
		Debug.DrawLine(elbow,target.position);
		Debug.DrawLine(mid,elbow);

		Vector3 v1=transform.position-target.position;
		v1.Normalize();
		Vector3 f=new Vector3();
		f.x=v1.y;
		f.y=-v1.x;
		f.z=v1.z;

		Debug.DrawLine(target.position,transform.position,Color.red);
		Debug.DrawLine(target.position,target.position+ f,Color.blue);
	}
}
