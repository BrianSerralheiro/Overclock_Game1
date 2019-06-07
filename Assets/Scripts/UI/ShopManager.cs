using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour 
{

	[SerializeField]
	private PopUp pop;
	[SerializeField]
	private Text warn;
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
	void Start () 
	{
		
	}
	public void BuySkin(int i)
	{
		if(skinPrices[i]<=Cash.totalCash)
		{
			price=skinPrices[i];
			cha=false;
			id=i;
			pop.Open("Buy skin "+skinNames[i]+" for "+price+" stars?",Confirm);
		}
		else
		{
			warn.text="You need "+skinPrices[i]+" stars in order to get this skin!";
			warn.transform.parent.gameObject.SetActive(true);
		}
	}
	public void BuyChar(int i)
	{
		if(charPrices[i]<=Cash.totalCash)
		{
			price=charPrices[i];
			cha=true;
			id=i;
			pop.Open("Buy pilot "+charNames[i]+" for "+price+" stars?",Confirm);
		}
		else
		{
			warn.text="You need "+charPrices[i]+" stars in order to get this pilot!";
			warn.transform.parent.gameObject.SetActive(true);
		}
	}
	public void Confirm()
	{
		Cash.totalCash-=price;
		if(cha)Locks.Char(id,true);
		else Locks.Skin(id,true);
		gameObject.BroadcastMessage("OnEnable");
	}
}
