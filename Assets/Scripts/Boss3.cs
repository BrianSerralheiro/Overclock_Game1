using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : EnemyBase {
	private Transform body;
	private Transform head;
	private SpriteRenderer henderer;
	private float timer;
	enum State
	{
		intro,
		wating,
		shooting,
		calling,
		slshing
	}
	[SerializeField]
	State state;
	// Use this for initialization
	new void Start () {
		base.Start();
		hp=2000;
		GameObject go=new GameObject("body");
		_renderer=go.AddComponent<SpriteRenderer>();
		_renderer.sprite=SpriteBase.I.boss3[1];
		body=go.transform;
		go=new GameObject("head");
		henderer=go.AddComponent<SpriteRenderer>();
		henderer.sprite=SpriteBase.I.boss3[2];
		head=go.transform;
		body.parent=head.parent=transform;
		body.localPosition=new Vector3(0,-0.44f,-0.1f);
		head.localPosition=new Vector3(0,0.8f,-0.2f);
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
		henderer.color=_renderer.color;
	}
}
