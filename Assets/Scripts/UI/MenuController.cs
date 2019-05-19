using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour 
{
	[SerializeField]
	RectTransform MainMenuUI;
	[SerializeField]
	RectTransform SettingsMenuUI;
	[SerializeField]
	RectTransform ShopMenuUI;
	[SerializeField]
	RectTransform SocialMenuUI;
	[SerializeField]
	GameObject EventSystem;
	[SerializeField]
	GameObject SettingsGroup;
	[SerializeField]
	GameObject CreditsGroup;
	private float MenuPositionX;
	[SerializeField]
	GameObject[] characterIDButton;

	//Help positioning
	Vector3 pos;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(MainMenuUI.position.x != MenuPositionX)
		{
			if(Mathf.Abs(MainMenuUI.position.x - MenuPositionX)< 0.01f)
			{
			EventSystem.SetActive(true);
			MainMenuUI.gameObject.SetActive(MenuPositionX == 0);
			SettingsMenuUI.gameObject.SetActive(MenuPositionX == -6.67f);
			ShopMenuUI.gameObject.SetActive(SocialMenuUI.gameObject.activeSelf || (!SettingsMenuUI.gameObject.activeSelf && MenuPositionX == 6.67f));
			SocialMenuUI.gameObject.SetActive(MenuPositionX == 6.67f * 2);
			pos.x=MenuPositionX;
		    MainMenuUI.position = pos;
			pos.x += 6.67f;
			SettingsMenuUI.position = pos;
			pos.x -= 6.67f * 2;
			ShopMenuUI.position = pos;
			pos.x -= 6.67f;
			SocialMenuUI.position = pos;

			}
			else
			{
			pos = MainMenuUI.position;
			//Move x position based on the menu position
			pos.x +=(MenuPositionX - pos.x) * Time.deltaTime * 5;
			MainMenuUI.position = pos;
			pos.x += 6.67f;
			SettingsMenuUI.position = pos;
			pos.x -= 6.67f * 2;
			ShopMenuUI.position = pos;
			pos.x -= 6.67f;
			SocialMenuUI.position = pos;
			}
		}
	}

	public void SwitchMenu(int i)
	{
		SoundManager.PlayEffects(0);
		if(i == 0)
		{
			MenuPositionX = 0;
			MainMenuUI.gameObject.SetActive(true);
			EventSystem.SetActive(false);
		}
		else if (i == 1)
		{
			MenuPositionX = -6.67f;
			SettingsMenuUI.gameObject.SetActive(true);
			EventSystem.SetActive(false);
		}
		else if (i == 2)
		{
			MenuPositionX = 6.67f;
			ShopMenuUI.gameObject.SetActive(true);
			EventSystem.SetActive(false);
		}
		else if (i == 3)
		{
			MenuPositionX = 6.67f * 2;
			SocialMenuUI.gameObject.SetActive(true);
			EventSystem.SetActive(false);
		}
	}

	public void CreditsSwitch(int i)
	{
		SettingsGroup.SetActive(i==0);
		CreditsGroup.SetActive(i==1);
		SoundManager.PlayEffects(0);
	}

	public void SwitchCharacter(int i)
	{
		for (int j = 0;j < 12; j++)
		{
			characterIDButton[j].SetActive(i == j%4);
		}
		Ship.playerID = i;
		SoundManager.PlayEffects(0);
	}
}
