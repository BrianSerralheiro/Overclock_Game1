using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {
	[SerializeField]
	private ParticleSystem[] systems;
	[SerializeField]
	private GameObject[] prefabs;
	private static GameObject[] _prefabs;
	private static ParticleSystem[] sys;
	private static Vector3 up=Vector3.up;
	private static Vector3 mod=Vector3.back*0.1f;
	void Start () {
		_prefabs = prefabs;
		sys=systems;
	}

	public static void Emit(int i,Transform t,int c)
	{
		sys[i].transform.forward=-t.up;
		sys[i].transform.position=t.position+mod;
		sys[i].Emit(c);
	}
	public static void Emit(int i,Vector3 p,int c)
	{
		sys[i].transform.up=up;
		sys[i].transform.position=p+mod;
		sys[i].Emit(c);
	}

	public static void InstantiatePrefab(int i, Vector3 v,float f)
	{
		Destroy(Instantiate(_prefabs[i], v,Quaternion.identity), f);
	}
}
