using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharLock : MonoBehaviour {

	[SerializeField]
	private int charId;
	void OnEnable()
	{
		Button b = GetComponent<Button>();
		if(b)
		{
			 b.interactable=Locks.Char(charId);
		}
		else Debug.LogError("CharLock needs a button to work");
	}
}
