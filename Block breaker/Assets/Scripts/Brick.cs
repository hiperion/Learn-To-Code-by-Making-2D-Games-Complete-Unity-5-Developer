﻿using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{

	public AudioClip crack;
	public Sprite[] hitSprites;
	public static int breakcableCount = 0;
	public GameObject smoke;
	private int timesHit;
	private LevelManager levelManager;
	private bool isBreakable;
	// Use this for initialization
	void Start ()
	{
		isBreakable = (this.tag == "Breakable");
		//Keep track of breakable bricks
		if (isBreakable) {
			breakcableCount++;	
			print (breakcableCount);
		}
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{

		bool isBreakable = (this.tag == "Breakable");
		AudioSource.PlayClipAtPoint (crack, transform.position, 0.01f);
		if (isBreakable) {
			HandleHits ();	
		}

	}

	void HandleHits ()
	{
		timesHit++;
		int maxHits = hitSprites.Length + 1;
		if (timesHit >= maxHits) {
			breakcableCount--;
			Debug.Log (breakcableCount);
			print (breakcableCount);
			levelManager.BrickDestroyed ();
			PuffSmoke ();
			Destroy (gameObject);
		} else {
			LoadSprites ();
		}
	}

	void PuffSmoke ()
	{
		GameObject smokePuff = Instantiate (smoke, gameObject.transform.position, Quaternion.identity) as GameObject;
		smoke.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer> ().color;

	}

	void LoadSprites ()
	{
		int spriteIndex = timesHit - 1;
		if (hitSprites [spriteIndex] != null) {
			this.GetComponent<SpriteRenderer> ().sprite = hitSprites [spriteIndex];
		} else {
			Debug.LogError ("Brick sprite missing");
		}
	}

	//TODO Remove this method once we can actually win!
	void SimulateWin ()
	{
		levelManager.LoadNextLevel ();
	}
}
