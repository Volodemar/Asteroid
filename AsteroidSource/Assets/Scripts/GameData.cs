using System.Collections.Generic;
/// <summary>
/// Данные песонажа
/// </summary>
public class GameData
{
	/// <summary>
	/// Имя персонажа
	/// </summary>
	public string Name {get; set;}	

	/// <summary>
	/// Путь к сохранению персонажа
	/// </summary>
	public string Path {get; set;}	

	/// <summary>
	/// Количество жизней персонажа
	/// </summary>
	public int Life {get; set;}

	/// <summary>
	/// Очки персонажа
	/// </summary>
	public int Score {get; set;}

	/// <summary>
	/// Количество выстрелов лазером
	/// </summary>
	public int Laser {get; set;}

	/// <summary>
	/// Теги персонажа (удобная штука для bool данных по наличию тега)
	/// </summary>
	public List<string> Tags {get; set;}

	/// <summary>
	/// Статичный, количество смертей игроков
	/// </summary>
	public int Deaths {get; set;}

	/// <summary>
	/// Статичный, количество очков игрока
	/// </summary>
	public int MaxScore {get; set;}

	/// <summary>
	/// Для простой таблицы лидеров имена
	/// </summary>
	public List<string> TopListNames {get; set;}

	/// <summary>
	/// Для простой таблицы лидеров очки
	/// </summary>
	public List<string> TopListScores {get; set;}

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="_name">Имя персонажа</param>
	/// <param name="_path">Путь к персонажу</param>
	public GameData(string name, string path)
	{
		//Статы
		Name				= name;
		Path				= path;
		Life				= 3;
		Score				= 0;
		Laser				= 5;

		//Списки
		Tags				= new List<string>();

		//Статичные данные персонажа (не уменьшаются)
		Deaths				= 0;
		MaxScore			= 0;

		//Top 10
		TopListNames		= new List<string>();
		TopListNames.Add("Noname");
		TopListNames.Add("Noname");
		TopListNames.Add("Noname");
		TopListNames.Add("Noname");
		TopListNames.Add("Noname");
		TopListNames.Add("Noname");
		TopListNames.Add("Noname");
		TopListNames.Add("Noname");
		TopListNames.Add("Noname");
		TopListNames.Add("Noname");
		TopListScores		= new List<string>();
		TopListScores.Add("100");
		TopListScores.Add("90");
		TopListScores.Add("80");
		TopListScores.Add("60");
		TopListScores.Add("50");
		TopListScores.Add("40");
		TopListScores.Add("30");
		TopListScores.Add("20");
		TopListScores.Add("10");
		TopListScores.Add("0");
	}
}
