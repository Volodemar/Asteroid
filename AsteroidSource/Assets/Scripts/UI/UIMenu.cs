using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : AsteroidGameObject
{
	[SerializeField] private Text BestScoreText = null;
	[SerializeField] private Text TotalDeathText = null;

	private void Start()
	{
		OnBestScoreChangeText();
	}

	public override void GetAction(string resourceID)
	{
		switch (resourceID)
		{
			case "ChangeGameState":
				OnBestScoreChangeText();
				OnTotalDeathChangeText();
				break;

			case "ScoreChange":
				OnBestScoreChangeText();
				break;
			
			case "DeathChange":
				OnTotalDeathChangeText();
				break;
		}
	}

	/// <summary>
	/// Изменить отображаемое количество очков
	/// </summary>
	public void OnBestScoreChangeText()
	{
		//Формирование строки очки
		string bestScore = "";
		int maxScoreLength = GM.GameData.MaxScore.ToString().Length;
		for(int i=0;i<5-maxScoreLength;i++)
			bestScore = bestScore + "0";
		bestScore = bestScore + GM.GameData.MaxScore.ToString();
		BestScoreText.text = bestScore;
	}

	/// <summary>
	/// Изменить отображаемое количество очков
	/// </summary>
	public void OnTotalDeathChangeText()
	{
		//Формирование строки очки
		string death = "";
		int deathLength = GM.GameData.Deaths.ToString().Length;
		for(int i=0;i<5-deathLength;i++)
			death = death + "0";
		death = death + GM.GameData.Deaths.ToString();
		TotalDeathText.text = death;
	}
}
