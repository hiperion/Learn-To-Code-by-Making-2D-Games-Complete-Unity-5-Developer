﻿using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

	public void LoadLevel (string name)
	{
		Debug.Log ("New level load:" + name);
		Application.LoadLevel (name);
	}

	public void QuitRequest ()
	{
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
