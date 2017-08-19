﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text scoreText;
	public DeathMenu deathMenu;

	private float score = 0.0f;
	private int difficultyLevel = 1;
	private int maxDifficultyLevel = 10;
	private int scoreToNextLevel = 10;
	private bool isDead = false;

	void Start () {
		
	}
	
	void Update () {

		if (isDead)
			return;
		
		if (score >= scoreToNextLevel)
			LevelUp ();

		score += Time.deltaTime * difficultyLevel;
		scoreText.text = ((int)score).ToString ();
	}

	void LevelUp()
	{
		if (difficultyLevel == maxDifficultyLevel)
			return;
		

		scoreToNextLevel *= 2;
		difficultyLevel++;
		GetComponent<PlayerMotor>().SetSpeed (difficultyLevel);
	}

	public void OnDeath()
	{
		isDead = true;

		if(PlayerPrefs.GetFloat("Highscore") < score){
			PlayerPrefs.SetFloat ("Highscore", score);
		}
		deathMenu.ToggleEndMenu (score);
	}
}
