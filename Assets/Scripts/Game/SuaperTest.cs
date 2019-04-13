using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuaperTest : MonoBehaviour {
	public GameObject red;
	public GameObject green;
	public GameObject blue;
	public static int id;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(++id>2)id=0;
			red.SetActive(id==0);
			green.SetActive(id==1);
			blue.SetActive(id==2);
			if(id==0){
				EnemyBase.player=red.transform;
			}
			if(id==1){
				EnemyBase.player=green.transform;
			}
			if(id==2){
				EnemyBase.player=blue.transform;
			}
		}
	}
}
