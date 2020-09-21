using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	/// <summary>
	/// Состояния игры
	/// </summary>
	public enum GameStates {
		Start,
		Play,
		Paused,
		GameOver
	}

	private GameStates _gameState = GameStates.Start;

	public GameStates GameState => _gameState;

	/// <summary>
	/// Переключатель между 3D и 2D игрой. 
	/// </summary>
	private bool is3DTypeGame = false;

	/// <summary>
	/// Возвращает тип игры 3D - true, 2D - false
	/// </summary>
	public bool TypeGame => is3DTypeGame;

	#region Границы

		/// <summary>
		/// Класс получающий границы игрового поля в 3D пространстве 
		/// </summary>
		private SizeField sizeField;

		/// <summary>
		/// Левая граница игрового поля в 3D пространстве
		/// </summary>
		public float LeftBorder => sizeField.LeftBorder;

		/// <summary>
		/// Правая граница игрового поля в 3D пространстве
		/// </summary>
		public float RightBorder => sizeField.RightBorder;

		/// <summary>
		/// Верхняя граница игрового поля в 3D пространстве
		/// </summary>
		public float UpBorder => sizeField.UpBorder;

		/// <summary>
		/// Нижняя граница игрового поля в 3D пространстве
		/// </summary>
		public float DownBorder => sizeField.DownBorder;

	#endregion Границы

	/// <summary>
	/// Позиция игрока для наводки летающих тарелок и не только
	/// </summary>
	[HideInInspector] public Vector3 PlayerPos;

	/// <summary>
	/// Основной путь к сохранениям
	/// </summary>
	[HideInInspector] public string _persistentPath { get; private set; }

	/// <summary>
	/// Файл основного сохранения
	/// </summary>
	[HideInInspector] public string _saveFileName { get; private set; } = "GameData";

	/// <summary>
	/// Все сохраняемые данные игры
	/// </summary>
	[HideInInspector] private GameData gameData;

	/// <summary>
	/// Данные игры
	/// </summary>
	public GameData GameData => gameData;

	private void Awake()
	{
		sizeField = this.GetComponent<SizeField>();
		_persistentPath = Application.persistentDataPath + Path.DirectorySeparatorChar;
		gameData = RepositoryData.CreateNewGameData(_persistentPath, _saveFileName, "Default");
	}

	private void Start()
	{
		OnGameStart();
	}

	private void Update()
	{
		//Возможность переключить тип игры
		if(Input.GetKeyDown(KeyCode.F2))
			is3DTypeGame = false;
		else if(Input.GetKeyDown(KeyCode.F3))
			is3DTypeGame = true;

		//Завершаем игру если кончились жизни
		if(gameData.Life == 0 && GameState == GameStates.Play)
			OnGameOver();
	}

	/// <summary>
	/// Выход из приложения
	/// </summary>
	public void OnGameExit()
	{
		Application.Quit();
	}

	/// <summary>
	/// Событие смена статуса игры
	/// </summary>
	/// <param name="state"></param>
	private void OnSetGameState(GameStates state)
	{
		_gameState = state;
		EventManager.OnAction("ChangeGameState");
	}

	/// <summary>
	/// Игра проиграна
	/// </summary>
	public void OnGameOver()
	{
		Time.timeScale = 0; 
		OnSetGameState(GameStates.GameOver);
	}

	/// <summary>
	/// Игра начинается
	/// </summary>
	public void OnGameStart()
	{
		Time.timeScale = 0;
		gameData = RepositoryData.LoadGameData(_persistentPath, _saveFileName, gameData.Name);
		OnSetGameState(GameStates.Start);
	}

	/// <summary>
	/// Игра начинается
	/// </summary>
	public void OnNewGame()
	{
		OnGameReset();
		OnGamePlay();
	}

	/// <summary>
	/// Пауза
	/// </summary>
	public void OnGamePaused()
	{
		Time.timeScale = 0; 
		OnSetGameState(GameStates.Paused);
	}

	/// <summary>
	/// Игра запущена
	/// </summary>
	public void OnGamePlay()
	{
		Time.timeScale = 1; 
		OnSetGameState(GameStates.Play);
	}

	/// <summary>
	/// Сброс уровня игры
	/// </summary>
	public void OnGameReset()
	{
		SetScore(0,true);
		SetLife(3,true);
		SetLaser(5,true);
		EventManager.OnAction("LevelReset");
	}

	/// <summary>
	/// Смена имени игрока
	/// </summary>
	public void OnChangeName(string name)
	{
		if(name == "")
			name = "Default";
		gameData.Name = name;
		OnSaveGame();
		EventManager.OnAction("ChangeName");
	}


	/// <summary>
	/// Сохранение
	/// </summary>
	public void OnSaveGame()
	{
		RepositoryData.SaveGameData(_persistentPath, _saveFileName, gameData);
	}

	/// <summary>
	/// Изменяем очки
	/// </summary>
	public void SetScore(int value, bool reset = false)
	{
		int modifValue = gameData.Score;

		if(reset)
			modifValue = value;
		else
			modifValue = modifValue + value;

		if(modifValue < 0)
			modifValue = 0;
		if(modifValue > 99999)
			modifValue = 99999;

		gameData.Score = modifValue;

		if(gameData.Score > gameData.MaxScore)
			gameData.MaxScore = gameData.Score;

		EventManager.OnAction("ScoreChange");
	}

	/// <summary>
	/// Изменем жизни
	/// </summary>
	public void SetLife(int value, bool reset = false)
	{
		int modifValue = gameData.Life;

		if(reset)
			modifValue = value;
		else
			modifValue = modifValue + value;

		if(modifValue > 3)
			modifValue = 3;
		if(modifValue < 0)
			modifValue = 0;

		gameData.Life = modifValue;

		EventManager.OnAction("LifeChange");
	}

	/// <summary>
	/// Изменение количества смертей
	/// </summary>
	public void SetDeath(int value, bool reset = false)
	{
		int modifValue = gameData.Deaths;

		if(reset)
			modifValue = value;
		else
			modifValue = modifValue + value;

		if(modifValue < 0)
			modifValue = 0;

		gameData.Deaths = modifValue;

		EventManager.OnAction("DeathChange");
	}

	/// <summary>
	/// Изменение количество зарядов лазера
	/// </summary>
	public void SetLaser(int value, bool reset = false)
	{
		int modifValue = gameData.Laser;

		if(reset)
			modifValue = value;
		else
			modifValue = modifValue + value;

		if(modifValue < 0)
			modifValue = 0;
		if(modifValue > 5)
			modifValue = 5;

		gameData.Laser = modifValue;

		EventManager.OnAction("LaserChange");
	}
}
