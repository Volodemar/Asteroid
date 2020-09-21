using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : AsteroidGameObject
{
	[SerializeField] private Text PlayerLifeText  = null;
	[SerializeField] private Text PlayerScoreText = null;
	[SerializeField] private Text LaserCountText = null;

	public override void GetAction(string resourceID)
	{
		switch (resourceID)
		{
			case "ScoreChange":
				OnScoreChangeText();
				break;

			case "LifeChange":
				OnLifeChangeText();
				break;

			case "LaserChange":
				OnLaserChangeText();
				break;
		}
	}

	/// <summary>
	/// Изменить отображаемое количество лазера
	/// </summary>
	private void OnLaserChangeText()
	{
		string countLaser = "";
		for(int i=0;i<GM.GameData.Laser;i++)
			countLaser = countLaser + "I";
		LaserCountText.text = countLaser;
	}

	/// <summary>
	/// Изменить отображаемое количество очков
	/// </summary>
	public void OnScoreChangeText()
	{
		PlayerScoreText.text = GM.GameData.Score.ToString();
	}

	/// <summary>
	/// Изменить отображаемое количество жизней
	/// </summary>
	public void OnLifeChangeText()
	{
		string stLifes = "";
		for (int i = 0; i < GM.GameData.Life; i++)
			stLifes = stLifes + "A";
		PlayerLifeText.text = stLifes;
	}
}
