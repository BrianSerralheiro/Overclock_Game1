using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBase : MonoBehaviour {
	public static SpriteBase I;
	public Sprite bullet;
	public Sprite shooter;
	public Sprite shooter2;
	public Sprite diver;
	public Sprite wall;
	public Sprite item;
	void Start () {
		I=this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
