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
	RectTransform CreditsUI;
	[SerializeField]
	GameObject EventSystem;

	[SerializeField]
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
			CreditsUI.gameObject.SetActive(MenuPositionX == -13.34f);
			pos.x=MenuPositionX;
		    MainMenuUI.position = pos;
			pos.x += 6.67f;
			SettingsMenuUI.position = pos;
			pos.x += 6.67f;
			CreditsUI.position = pos;
			}
			else
			{
			pos = MainMenuUI.position;
			//Move x position based on the menu position
			pos.x +=(MenuPositionX - pos.x) * Time.deltaTime * 5;
			MainMenuUI.position = pos;
			pos.x += 6.67f;
			SettingsMenuUI.position = pos;
			pos.x += 6.67f;
			CreditsUI.position = pos;
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
}
