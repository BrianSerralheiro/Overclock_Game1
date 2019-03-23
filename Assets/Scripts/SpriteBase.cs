using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBase : MonoBehaviour {
	public static SpriteBase I;
	public Sprite bullet;
	public Sprite shooter;
	public Sprite[] Round;
	public Sprite diver;
	public Sprite carrier;
	public Sprite[] grabber;
	public Sprite item;
	public Sprite[] legs;
	public Sprite[] mouth;
	public Sprite[] shooterarms;
	public Sprite[] shooterlegs;
	public Sprite[] carrierlegs;
	public Sprite[] boss1;
	void Start () {
		I=this;
		Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
