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
	private float ratio;

	//Help positioning
	Vector3 pos;
	
	void Start ()
	{
		Locks.Load();
		ratio = (float)Screen.width / (float)Screen.height * 10;
		SoundManager.Play(0);
		ADManager.RequestVideo();
	}
	void Update () 
	{
		if(MainMenuUI.position.x != MenuPositionX)
		{
			if(Mathf.Abs(MainMenuUI.position.x - MenuPositionX)< 0.01f)
			{
			EventSystem.SetActive(true);
			//MainMenuUI.gameObject.SetActive(MenuPositionX == 0);
			SettingsMenuUI.gameObject.SetActive(MenuPositionX == -ratio);
			ShopMenuUI.gameObject.SetActive(MenuPositionX == ratio ||(MenuPositionX == ratio * 2 && !SocialMenuUI.gameObject.activeSelf));
			SocialMenuUI.gameObject.SetActive(MenuPositionX == ratio * 2);
			pos.x=MenuPositionX;
		    MainMenuUI.position = pos;
			pos.x += ratio;
			SettingsMenuUI.position = pos;
			pos.x -= ratio * 2;
			ShopMenuUI.position = pos;
			pos.x -= ratio;
			SocialMenuUI.position = pos;

			}
			else
			{
			pos = MainMenuUI.position;
			//Move x position based on the menu position
			pos.x +=(MenuPositionX - pos.x) * Time.deltaTime * 5;
			MainMenuUI.position = pos;
			pos.x += ratio;
			SettingsMenuUI.position = pos;
			pos.x -= ratio * 2;
			ShopMenuUI.position = pos;
			pos.x -= ratio;
			SocialMenuUI.position = pos;
			}
		}
	}

	public void SwitchMenu(int i)
	{
		SoundManager.PlayEffects(0);
		if(i == 0)
		{
			if(MenuPositionX == ratio *2)ShopMenuUI.gameObject.SetActive(true);	
			MenuPositionX = 0;
			MainMenuUI.gameObject.SetActive(true);
			EventSystem.SetActive(false);
		}
		else if (i == 1)
		{
			MenuPositionX = -ratio;
			SettingsMenuUI.gameObject.SetActive(true);
			EventSystem.SetActive(false);
		}
		else if (i == 2)
		{
			MenuPositionX = ratio;
			ShopMenuUI.gameObject.SetActive(true);
			EventSystem.SetActive(false);
		}
		else if (i == 3)
		{
			MenuPositionX = ratio * 2;
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
