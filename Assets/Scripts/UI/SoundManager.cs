using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour 
{
	[SerializeField]
	private AudioClip[] _songs;
	[SerializeField]
	private AudioClip[] _sounds;
	
	private AudioSource soundPlayer;

	private static SoundManager _soundManager;

	private static float volumeSFX = 1;

	// Use this for initialization
	void Awake() 
	{
		if(_soundManager)
		{
			Destroy(gameObject);
			return;
		}
		GameObject go = new GameObject("AudioSource");
		soundPlayer = go.AddComponent<AudioSource>();
		soundPlayer.loop = true;
		DontDestroyOnLoad(go);
		DontDestroyOnLoad(gameObject);
		_soundManager = this;
		//Destroy(this);
	}

	public static void Play (int i)
	{
		if(_soundManager == null)
		{
			Debug.LogWarning("SoundManager nao inicializado");
			return;
		}
		_soundManager.soundPlayer.clip = _soundManager._songs[i%_soundManager._songs.Length];
		_soundManager.soundPlayer.Play();
	}

	public static void PlayEffects (int i)
	{
		if(_soundManager == null)
		{
			Debug.LogWarning("SoundEffects nao inicializado");
			return;
		}
		float f = _soundManager.soundPlayer.volume;
		_soundManager.soundPlayer.volume = 1;
		_soundManager.soundPlayer.PlayOneShot(_soundManager._sounds[i],volumeSFX);
		_soundManager.soundPlayer.volume = f;

	}

	public static void VolumeMusic(float i)
	{
		_soundManager.soundPlayer.volume = i;
	}

	public static void VolumeSFX(float i)
	{

		volumeSFX = i;
	}

}
