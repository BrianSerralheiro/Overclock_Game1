using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Locks {
	
	private static string skins="000000000000";
	private static string chars="000";
	private static bool premium;

	public static void Load()
	{
		if(PlayerPrefs.HasKey("skins"))skins=PlayerPrefs.GetString("skins");
		if(PlayerPrefs.HasKey("chars"))chars=PlayerPrefs.GetString("chars");
		Debug.Log("Loading " + skins);
		Debug.Log("Loading " + chars);
	}
	public static void Save()
	{
		Debug.Log("Saving " + skins);
		PlayerPrefs.SetString("skins",skins);
		PlayerPrefs.SetString("chars",chars);
		Debug.Log("Saving " + chars);

	}
	public static bool Skin(int i)
	{
		return skins[i]!='0';
	}
	public static void Skin(int i,bool b)
	{
		skins=skins.Substring(0,i)+"1"+skins.Substring(i+1,11-i);
		Save();
	}
	public static bool Char(int i)
	{
		return chars[i]!='0';
	}
	public static void Char(int i,bool b)
	{
		chars=chars.Substring(0,i)+"1"+chars.Substring(i+1,2-i);
		Save();
	}
	public static void UnlockAll()
	{
		for(int i = 0; i<skins.Length; i++)
		{
			if(i<chars.Length)Char(i);
			Skin(i);
		}
		premium=true;
	}
	public static bool IsPremium()
	{
		return premium;
	}
}
