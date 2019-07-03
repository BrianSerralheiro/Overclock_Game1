using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ADs;

public class Continue : MonoBehaviour 
{
	public delegate void Del(bool b);
	public Del Active; 
	[SerializeField]
	private Text continueLog;
	[SerializeField]
	private Button button;
	public Ship ship;
	private int continues;
	private void Update()
	{
		button.interactable=adsManager.LoadedVideo();
	}
	public void Continues(int i)
	{
		continues=i;
	}
	private void OnEnable()
	{
		continueLog.text="Continues Left: "+continues;
	}
	public void WatchAd()
	{
		SoundManager.PlayEffects(0);
		continues--;
		if(adsManager.ShowAd(ship.Heal))
		{
			gameObject.SetActive(false);
			Active(false);
		}
	}
	public bool HasContinue()
	{
		return continues>0;
	}
	public void buyContinue()
	{
		SoundManager.PlayEffects(0);
		continues--;
		gameObject.SetActive(false);
		Active(false);
		ship.Heal();
	}

}
