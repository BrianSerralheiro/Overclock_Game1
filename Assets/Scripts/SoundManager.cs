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

	// Use this for initialization
	void Start () 
	{
		GameObject go = new GameObject("AudioSource");
		soundPlayer = go.AddComponent<AudioSource>();
		soundPlayer.loop = true;
		DontDestroyOnLoad(go);
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

}
