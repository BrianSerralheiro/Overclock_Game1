﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBase : MonoBehaviour {
	public static SpriteBase I;
	public Sprite bullet;
	public Sprite shooter;
	public Sprite diver;
	public Sprite carrier;
	public Sprite[] round;
	public Sprite[] Lasor;
	public Sprite[] grabber;
	public Sprite[] launcher;
	public Sprite[] bat;
	public Sprite[] batgirl;
	public Sprite[] header;
	public Sprite[] item;
	public Sprite[] legs;
	public Sprite[] mouth;
	public Sprite[] shooterarms;
	public Sprite[] shooterlegs;
	public Sprite[] carrierlegs;
	public Sprite[] drone;
	public Sprite[] boss1;
	public Sprite[] boss2;
	public Sprite[] boss3;
	public Material shock;
	void Start () {
		I=this;
		Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
