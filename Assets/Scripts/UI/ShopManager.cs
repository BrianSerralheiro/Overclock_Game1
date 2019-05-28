using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

	[SerializeField]
	private int[] skinPrices;
	[SerializeField]
	private int[] charPrices;
	void Start () {
		
	}
	public void BuySkin(int i)
	{
		if(skinPrices[i]<=Cash.totalCash)
		{
			Cash.totalCash-=skinPrices[i];
			Locks.Skin(i,true);
		}
		gameObject.SetActive(false);
		gameObject.SetActive(true);
	}
	public void BuyChar(int i)
	{
		if(charPrices[i]<=Cash.totalCash)
		{
			Cash.totalCash-=charPrices[i];
			Locks.Char(i,true);
		}
		gameObject.SetActive(false);
		gameObject.SetActive(true);
	}
}
