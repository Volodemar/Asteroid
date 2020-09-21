using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : AsteroidGameObject
{

	[SerializeField] private GameObject UIGame = null;
	[SerializeField] private GameObject UIGameOver = null;
	[SerializeField] private GameObject UIMenu = null;

	public override void Awake()
	{
		base.Awake();
	}

	public override void GetAction(string resourceID)
	{
		switch (resourceID)
		{
			case "ChangeGameState":

				if(GM.GameState == GameManager.GameStates.Play)
					UIGame.SetActive(true);
				else
					UIGame.SetActive(false);

				if(GM.GameState == GameManager.GameStates.GameOver)
					UIGameOver.SetActive(true);
				else
					UIGameOver.SetActive(false);

				if(GM.GameState == GameManager.GameStates.Start)
					UIMenu.SetActive(true);
				else
					UIMenu.SetActive(false);

				break;
		}
	}
}
