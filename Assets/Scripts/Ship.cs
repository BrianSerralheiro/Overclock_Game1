using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
	float shoottimer;
	Transform target;
	[SerializeField]
	Transform gun;
	[SerializeField]
	Transform burst;
	int hp = 1;
	public Bullet[] bulletPool;
	private int width=Screen.width;
	private int height=Screen.height;
	private Vector3 moveto;
	[SerializeField]
	private Vector3 offset;

	public static float speed;
	public static bool paused;
	void Start()
	{
		speed=5f;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(--hp<=0) gameObject.SetActive(false);
	}
	void Update()
	{
		//if(shoottimer<=0)shoottimer=3;
		moveto.Set(Input.mousePosition.x/width*5f,Input.mousePosition.y/height*10f,0);

		if(Input.GetMouseButtonDown(0))
		{
			if(Mathf.Abs(moveto.x-transform.position.x)>1 || Mathf.Abs(moveto.y-transform.position.y)>1)
				offset.Set(0,0,0);
			else offset=transform.position-moveto;
		}
		if(Input.GetMouseButton(0))
		{
			float f= moveto.y+offset.y-transform.position.y;
			Vector3 v= burst.localScale;
			v.Set(1,f>0.1?5:f<-0.1?1:3,1);
			burst.localScale=v;
			f = moveto.x+offset.x-transform.position.x;
			v = transform.localEulerAngles;
			v.Set(0,f>0.5 ? -35f : f<-0.5 ? 35f : 0,0);
			transform.localEulerAngles=v;
			if(Mathf.Abs(moveto.x+offset.x-transform.position.x)>0.1f || Mathf.Abs(moveto.y+offset.y-transform.position.y)>0.1f)
			 transform.Translate((moveto+offset-transform.position).normalized*speed*Time.deltaTime);
			if(offset==Vector3.zero && !(Mathf.Abs(moveto.x-transform.position.x)>1 || Mathf.Abs(moveto.y-transform.position.y)>1))
				offset=transform.position-moveto;
		}
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
