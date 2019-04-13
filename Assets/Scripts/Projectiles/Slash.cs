using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {
	
	
	void Update () {
		transform.Translate(0,-Time.deltaTime*5,0);
	}
	public void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.name.Contains("Ship"))Destroy(gameObject);
	}
}
