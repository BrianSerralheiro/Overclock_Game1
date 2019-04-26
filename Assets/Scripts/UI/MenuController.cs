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
	GameObject EventSystem;
	[SerializeField]
	GameObject SettingsGroup;
	[SerializeField]
	GameObject CreditsGroup;
	private float MenuPositionX;

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
			pos.x=MenuPositionX;
		    MainMenuUI.position = pos;
			pos.x += 6.67f;
			SettingsMenuUI.position = pos;
			}
			else
			{
			pos = MainMenuUI.position;
			//Move x position based on the menu position
			pos.x +=(MenuPositionX - pos.x) * Time.deltaTime * 5;
			MainMenuUI.position = pos;
			pos.x += 6.67f;
			SettingsMenuUI.position = pos;
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
	}

	public void CreditsSwitch(int i)
	{
		SettingsGroup.SetActive(i==0);
		CreditsGroup.SetActive(i==1);
		SoundManager.PlayEffects(0);
	}
}
