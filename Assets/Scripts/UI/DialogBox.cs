using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {
	private float chars;
	[SerializeField]
	private Sprite[] box;
	[SerializeField]
	private float cps;
	private static string[] texts;
	private static int id;
	private static UnityAction on;
	private Text text;
	private Image image;
	void Start () {
		text=GetComponentInChildren<Text>();
		image=GetComponentInChildren<Image>();
		if(!text || !image)Debug.LogError("Dialog bx needs both image and text components in children");
		if(text)image.sprite=box[Ship.playerID];
		box=null;
		on=On;
	}
	public static void Texts(string[] s)
	{
		texts=s;
	}
	public static void Text(int i)
	{
		id=i;
		on();
	}
	private void On()
	{
		Ship.paused=true;
		gameObject.SetActive(true);
		text.text="";
		chars=0;
	}
	public void Close()
	{
		if(chars>texts[id].Length){
			Ship.paused=false;
			gameObject.SetActive(false);
		}
		else chars=texts[id].Length;
	}
	void Update () {
		if(chars<texts[id].Length){
			chars+=Time.deltaTime;
			if(text.text.Length<Mathf.FloorToInt(chars))text.text=texts[id].Substring(0,Mathf.FloorToInt(chars));
		}

	}
}
