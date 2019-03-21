using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : EnemyBase
{


	public override void Position(int i){
		transform.position=new Vector3(43+i*86,550,0);
		hp=4;
	}

	void Update(){
		if(Ship.paused) return;
		//transform.Translate(0,-Ship.speed*512*Time.deltaTime,0);
		if(transform.position.y<-100) gameObject.SetActive(false);

	}
}
