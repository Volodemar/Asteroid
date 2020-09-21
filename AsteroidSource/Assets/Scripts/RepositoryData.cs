using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Репозиторий игрока для сохранений данных игры 
/// </summary>
public static class RepositoryData
{
	/// <summary>
	/// Инициализирует нового персонажа с созданием сохранений
	/// </summary>
	/// <param name="persistentPath">Путь к сохранению</param>
	/// <param name="saveFileName">Название сохранения</param>
	/// <param name="playerName">Имя персонажа</param>
	public static GameData CreateNewGameData(string persistentPath, string saveFileName, string playerName)
	{
		string path = persistentPath + saveFileName + ".json";

		//Создаем директорию если таковая отсутствует
		if (!Directory.Exists(persistentPath))
			Directory.CreateDirectory(persistentPath);

		GameData gameData = new GameData(playerName, path);

		//Сохранить дефолтные данные пользователя
		SaveGameData(persistentPath, saveFileName, gameData);

		return gameData;
	}

	/// <summary>
	/// Сохраняет данные по персонажу
	/// </summary>
	public static void SaveGameData(string persistentPath, string saveFileName, GameData gameData)
	{
		string path = persistentPath + saveFileName + ".json";

		JSONNode node = JSONNode.Parse("{ }");

		//Сохраняем статы
		node["name"]			= gameData.Name.ToString();
		node["life"]			= gameData.Life.ToString();
		node["score"]			= gameData.Score.ToString();
		node["laser"]			= gameData.Laser.ToString();

		//Сохраняем списки значений
		node["tags"]			= MyJsonTools.ConvertListToJStr(gameData.Tags);

		//Сохранение топ лист данных
		StaticSaveTopListData(persistentPath + saveFileName, gameData);

		//Сохранение статичных данных игрока
		StaticSaveData(persistentPath + saveFileName, gameData);

		StreamWriter sw = new StreamWriter(path);
		sw.Write(node.ToString());
		sw.Close();
	}

	private static void StaticSaveTopListData(string path, GameData gameData)
	{
		string staticFile = path + "TopList.json";

		//Сохранение статичных данных
		GameData loadStatic = StaticLoadTopListData(path, gameData);
		int    currentScore = gameData.Score;
		string currentName  = gameData.Name;
		for(int i=0;i<loadStatic.TopListScores.Count;i++)
		{
			int    topScore = int.Parse(loadStatic.TopListScores[i]);
			string topName  = loadStatic.TopListNames[i];

			if(topScore > currentScore)
			{
				continue;
			}
			else
			{
				//Сохраним прошлые значения
				int    lastCurrentScore = currentScore;
				string lastCurrentName  = currentName;

				//Текущие значения должны передаться дальше по списку
				currentScore = topScore;
				currentName  = topName;

				//Запишем новые значения предыдущей строки
				loadStatic.TopListScores[i] = lastCurrentScore.ToString();
				loadStatic.TopListNames[i]  = lastCurrentName;
			}
		}

		gameData.TopListNames  = loadStatic.TopListNames;
		gameData.TopListScores = loadStatic.TopListScores;

		JSONNode node = JSONNode.Parse("{ }");
		node["topnames"]	= MyJsonTools.ConvertListToJStr(loadStatic.TopListNames, false);
		node["topscores"]	= MyJsonTools.ConvertListToJStr(loadStatic.TopListScores, false); 

        StreamWriter sw = new StreamWriter(staticFile);
        sw.Write(node.ToString());
        sw.Close();
	}

	/// <summary>
	/// Обработка сохранения статичных данных персонажа
	/// </summary>
	private static void StaticSaveData(string path, GameData gameData)
	{
		string staticFile = path + "Static.json";

		//Сохранение статичных данных
		GameData loadStatic = StaticLoadData(path, gameData);
		if(loadStatic.Deaths   > gameData.Deaths)	gameData.Deaths		= loadStatic.Deaths;
		if(loadStatic.MaxScore > gameData.MaxScore)	gameData.MaxScore	= loadStatic.MaxScore;

		JSONNode node = JSONNode.Parse("{ }");
		node["deaths"]		= gameData.Deaths.ToString();
		node["maxscore"]	= gameData.MaxScore.ToString();

        StreamWriter sw = new StreamWriter(staticFile);
        sw.Write(node.ToString());
        sw.Close();
	}

	/// <summary>
	/// Возвращает данные по пероснажу
	/// </summary>
	public static GameData LoadGameData(string persistentPath, string saveFileName, string playerName)
	{
		string path = persistentPath + saveFileName + ".json";

		GameData gameData = new GameData(playerName, path);

		//Получаем значения из сохранения
        StreamReader sr = new StreamReader(path);
        var _tempStr = sr.ReadToEnd();
        JSONNode PPS = JSON.Parse(_tempStr);

		//Получаем статы
        gameData.Name		= (string)PPS["name"];
        gameData.Path		= (string)PPS["path"];
        gameData.Life		= MyJsonTools.ParseIntField(PPS, "life");
        gameData.Score		= MyJsonTools.ParseIntField(PPS, "score");
		gameData.Laser		= MyJsonTools.ParseIntField(PPS, "laser");

		//Получаем теги
		List<string> tags = new List<string>();
        JSONNode savedTags = JSON.Parse(PPS["tags"]);
        for (int i = 0; i < savedTags.Count; i++)
            tags.Add(savedTags[i.ToString()]);
		gameData.Tags = tags;

		//Сохранение топ лист данных
		GameData topListData = StaticLoadTopListData(persistentPath + saveFileName, gameData);
		gameData.TopListNames  = topListData.TopListNames;
		gameData.TopListScores = topListData.TopListScores;

		//Загрузка статичных значений (нельзя уменьшать)
		GameData staticData = StaticLoadData(persistentPath + saveFileName, gameData);
		gameData.Deaths   = staticData.Deaths;
		gameData.MaxScore = staticData.MaxScore;

		sr.Close();

		return gameData;
	}

	private static GameData StaticLoadTopListData(string path, GameData gameData)
	{
		string staticFile = path + "TopList.json";
		GameData ret = new GameData(gameData.Name, gameData.Path);

		//Если файл не существует
		if (!File.Exists(staticFile))
		{
			JSONNode node = JSONNode.Parse("{ }");
			node["topnames"]	= MyJsonTools.ConvertListToJStr(gameData.TopListNames, false);
			node["topscores"]	= MyJsonTools.ConvertListToJStr(gameData.TopListScores, false);
            StreamWriter sw = new StreamWriter(staticFile);
            sw.Write(node.ToString());
            sw.Close();
		}

		StreamReader sr   = new StreamReader(staticFile);
		JSONNode PPSStatic = JSON.Parse(sr.ReadToEnd());

		//Получаем списки
		List<string> topnames = new List<string>();
        JSONNode savedTags = JSON.Parse(PPSStatic["topnames"]);
        for (int i = 0; i < savedTags.Count; i++)
            topnames.Add(savedTags[i.ToString()]);
		ret.TopListNames = topnames;

		List<string> topscores = new List<string>();
        JSONNode savedTags1 = JSON.Parse(PPSStatic["topscores"]);
        for (int i = 0; i < savedTags1.Count; i++)
            topscores.Add(savedTags1[i.ToString()]);
		ret.TopListScores = topscores;

		sr.Close();

		return ret;
	}

	/// <summary>
	/// Загрузка несбрасываемых значений персонажа
	/// </summary>
	private static GameData StaticLoadData(string path, GameData gameData)
	{
		string staticFile = path + "Static.json";
		GameData ret = new GameData(gameData.Name, gameData.Path);

		//Если файл не существует
		if (!File.Exists(staticFile))
		{
			JSONNode node = JSONNode.Parse("{ }");
			node["deaths"]			= "0";
			node["maxscore"]		= "0";
            StreamWriter sw = new StreamWriter(staticFile);
            sw.Write(node.ToString());
            sw.Close();
		}

		StreamReader sr   = new StreamReader(staticFile);
		JSONNode PPSStatic = JSON.Parse(sr.ReadToEnd());

		//Получаем статы
        ret.Deaths			= MyJsonTools.ParseIntField(PPSStatic, "deaths"); 
		ret.MaxScore		= MyJsonTools.ParseIntField(PPSStatic, "maxscore"); 

		sr.Close();

		return ret;
	}
}