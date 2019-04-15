using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour 
{
	[SerializeField]
	private Slider musicSlider;

	[SerializeField]
	private Slider sfxSlider;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		SoundManager.VolumeMusic(musicSlider.value);
		SoundManager.VolumeSFX(sfxSlider.value);
	}

}
