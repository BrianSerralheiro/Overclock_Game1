using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBase : MonoBehaviour {
	public static SpriteBase I;
	public Sprite bullet;
	public Sprite shooter;
	public Sprite shooter2;
	public Sprite diver;
	public Sprite carrier;
	public Sprite item;
	public Sprite[] legs;
	public Sprite[] mouth;
	public Sprite[] shooterarms;
	public Sprite[] shooterlegs;
	public Sprite[] carrierlegs;
	void Start () {
		I=this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
