using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

	[SerializeField]
	private PopUp pop;
	[SerializeField]
	private int[] skinPrices;
	[SerializeField]
	private string[] skinNames;
	[SerializeField]
	private int[] charPrices;
	[SerializeField]
	private string[] charNames;
	private int price;
	private int id;
	private bool cha;
	void Start () {
		
	}
	public void BuySkin(int i)
	{
		if(skinPrices[i]<=Cash.totalCash)
		{
			price=skinPrices[i];
			cha=false;
			id=i;
			pop.Open("Buy skin "+skinNames[i]+" for "+price,Confirm);
			Debug.Log("Waiting buy "+ id+" "+price+" stars");
		}
	}
	public void BuyChar(int i)
	{
		if(charPrices[i]<=Cash.totalCash)
		{
			price=charPrices[i];
			cha=true;
			id=i;
			pop.Open("Buy pilot "+charNames[i]+" for "+price+" stars",Confirm);
			Debug.Log("Waiting buy "+ id+" "+price);
		}
	}
	public void Confirm()
	{
		Cash.totalCash-=price;
		if(cha)Locks.Char(id,true);
		else Locks.Skin(id,true);
		Debug.Log("Confirming buy "+ id+" "+price);
		//gameObject.SetActive(false);
		//gameObject.SetActive(true);
		gameObject.BroadcastMessage("OnEnable");
		pop.Close();
	}
}
