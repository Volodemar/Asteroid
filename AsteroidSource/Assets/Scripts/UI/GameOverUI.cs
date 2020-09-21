using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : AsteroidGameObject
{
	[SerializeField] private Text PlayerScoreText = null;
	[SerializeField] private Text BestScoreText = null;
	[SerializeField] private GameObject GroupTop = null;
	[SerializeField] private GameObject GroupEnterName = null;

	public override void GetAction(string resourceID)
	{
		if(GM.GameState == GameManager.GameStates.GameOver)
		{
			switch (resourceID)
			{
				case "ChangeGameState":
					OnScoreChangeText();
					OnBestScoreChangeText();
					OnScoreCheckTop();
					break;

				case "ChangeName":
					GroupEnterName.SetActive(false);
					GroupTop.SetActive(true);
					OnScoreChangeTopList();
					break;
			}
		}
	}

	/// <summary>
	/// Проверяем надо ли вводить имя и отображаем результаты
	/// </summary>
	public void OnScoreCheckTop()
	{
		//Текущий топ
		int currentTop = -1;
		for(int i=0;i<GM.GameData.TopListScores.Count;i++)
		{
			int scoreCurTop = int.Parse(GM.GameData.TopListScores[i]);
			if(GM.GameData.Score > scoreCurTop)
			{
				currentTop = i;
				break;
			}
		}

		if(currentTop != -1)
		{
			GroupEnterName.SetActive(true);
			GroupTop.SetActive(false);
		}
		else
		{
			GroupEnterName.SetActive(false);
			GroupTop.SetActive(true);
			OnScoreChangeTopList();
		}
	}

	/// <summary>
	/// Выводим ТОП игроков
	/// </summary>
	public void OnScoreChangeTopList()
	{
		if(GroupTop != null)
		{
			for(int i=0;i<GroupTop.transform.childCount;i++)
			{
				TopListElement element = GroupTop.transform.GetChild(i).GetComponent<TopListElement>();
				element.TopText.text = GM.GameData.TopListNames[i];
				element.TopScore.text = GM.GameData.TopListScores[i];

				//Хотел сделать красиво, но шрифт имеет разный размер цыфр нет времени подбирать новый (потом включить)
				//string score = "";
				//int scoreLength = GM.GameData.TopListScores[i].Length;
				//for(int n=0;n<5-scoreLength;n++)
				//	score = score + "0";
				//score = score + GM.GameData.TopListScores[i];
				//element.TopScore.text = score;
			}
		}
	}

	/// <summary>
	/// Изменить отображаемое количество очков
	/// </summary>
	public void OnScoreChangeText()
	{
		//Формирование строки очки
		string score = "";
		int scoreLength = GM.GameData.Score.ToString().Length;
		for(int i=0;i<5-scoreLength;i++)
			score = score + "0";
		score = score + GM.GameData.Score.ToString();
		PlayerScoreText.text = score;
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
}
