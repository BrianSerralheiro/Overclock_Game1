using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
	float shoottimer;
	Transform target;
	[SerializeField]
	Transform gun;
	int hp = 1;
	public Bullet[] bulletPool;
	public static float speed;
	public static bool paused;
	void Start()
	{
		speed=0.2f;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(--hp<=0) gameObject.SetActive(false);
	}
	void Update()
	{
		//if(shoottimer<=0)shoottimer=3;
		paused=true;
		if(Input.GetMouseButton(0))
		{
			paused=false;
			transform.position=new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
		}
		if(paused) return;
		if(shoottimer>0) shoottimer-=Time.deltaTime;
		if(target)
		{
			if(!target.gameObject.activeSelf) target=null;
			else gun.eulerAngles=new Vector3(0,0,Mathf.Atan2(gun.position.x-target.position.x,target.position.y-gun.position.y)*Mathf.Rad2Deg);

		}
	}
	void LateUpdate()
	{
		if(shoottimer<=0 && target)
		{
			shoottimer=0.5f;
			Shoot();
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(target==col.transform) target=null;
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if(col.gameObject.CompareTag("bullet")) return;
		if(!target ||
		(col.transform.position-transform.position).sqrMagnitude<(target.position-transform.position).sqrMagnitude)
			target=col.transform;
	}
	void Shoot()
	{
		for(int i = 0; i<bulletPool.Length; i++)
		{
			if(!bulletPool[i].gameObject.activeSelf)
			{
				bulletPool[i].Position(gun);

				break;
			}
		}
	}
}
