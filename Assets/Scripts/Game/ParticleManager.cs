using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {
	[SerializeField]
	private ParticleSystem[] systems;
	private static ParticleSystem[] sys;
	void Start () {
		sys=systems;
	}

	public static void Emit(int i,Transform t,int c)
	{
		sys[i].transform.position=t.position;
		sys[i].Emit(c);
	}
	public static void Emit(int i,Vector3 p,int c)
	{
		sys[i].transform.position=p;
		sys[i].Emit(c);
	}
}
