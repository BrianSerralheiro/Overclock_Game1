using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Locks {
	
	private static string skins="000000000000";
	private static string chars="000";

	public static void Load()
	{
		if(PlayerPrefs.GetString("skins")!="")skins=PlayerPrefs.GetString("skins");
		if(PlayerPrefs.GetString("chars")!="")skins=PlayerPrefs.GetString("chars");

	}
	public static void Save()
	{
		PlayerPrefs.SetString("skins",skins);
		PlayerPrefs.SetString("chars",chars);
	}
	public static bool Skin(int i)
	{
		return skins[i]!='0' || true;
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
}
