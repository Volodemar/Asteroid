using System.Collections;
using UnityEngine;

/// <summary>
/// Класс отвечает за спавн объектов
/// </summary>
public class SpawnerController : AsteroidGameObject
{
	[SerializeField] private Timer timerSpawn = null;
	[SerializeField] private Timer timerAddLaser = null;
	private SpawnPrefab spawner;

	public override void Awake()
	{
		base.Awake();
		spawner = this.GetComponent<SpawnPrefab>();
	}

	public override void GetAction(string resourceID)
	{
		switch (resourceID)
		{
			case "PlayerDie":
				OnSpawnPlayer();
				break;
			case "LevelReset":
				OnSpawnReset();
				break;
		}
	}

	private void Update()
    {
        //Проверка если таймер истек, надо спавнить объект
		if(timerSpawn.RemainingTime == 0 && timerSpawn.IsActive())
		{
			//20% шанс что появится НЛО
			if(Random.Range(0,100) < 40)
				OnSpawnUFO();

			OnSpawnLargeRock();
			timerSpawn.StartTimer();
		}

		if(timerAddLaser.RemainingTime == 0 && timerAddLaser.IsActive())
		{
			GM.SetLaser(1);
			timerAddLaser.StartTimer();
		}
    }

	/// <summary>
	/// Очистка поля при перезагрузке уровня
	/// </summary>
	private void OnSpawnReset()
	{
		//Очистка поля от игровых объектов
		for(int i=0;i<TR.childCount;i++)
			Destroy(TR.GetChild(i).gameObject);

		//Спавн игрока
		OnSpawnPlayer();

		//Стартуем таймер спавнера
		timerSpawn.StartTimer();
		timerAddLaser.StartTimer();

		//Первичная вставка
		for(int i=0;i<3;i++)
			OnSpawnLargeRock();
	}

	/// <summary>
	/// Спавн игрока
	/// </summary>
	public void OnSpawnPlayer()
	{
		if(GM.GameData.Life > 0)
			spawner.OnSpawnPrefab(3, Vector3.zero);

		GM.SetLaser(5,true);
	}

	/// <summary>
	/// Спавн летающей тарелки
	/// </summary>
	public void OnSpawnUFO()
	{
		AUDIO.PlaySoundOnce(3);
		spawner.OnSpawnPrefab(4, new Vector3(GetFreePosX(), GM.PlayerPos.y, 0f));
	}

	/// <summary>
	/// Спавн большого метеорита
	/// </summary>
	public void OnSpawnLargeRock()
	{
		spawner.OnSpawnPrefab(0, new Vector3(GetFreePosX(), GetRandomPos().y, 0f));
	}

	/// <summary>
	/// Спавн среднего метеорита
	/// </summary>
	public void OnSpawnMediumRock(Vector3 pos)
	{
		for(int i=0;i<3;i++)
			spawner.OnSpawnPrefab(1, pos);
	}

	/// <summary>
	/// Спавн мелкого метеорита
	/// </summary>
	public void OnSpawnSmallRock(Vector3 pos)
	{
		for(int i=0;i<3;i++)
		{
			GameObject inst = spawner.OnSpawnPrefab(2, pos);
			Destroy(inst, GetRandomTime(0.1f, 1f));
		}
	}

	/// <summary>
	/// Возвращает безопасную позицию для спавна относительно игрока
	/// </summary>
	private float GetFreePosX()
	{
		if(GM.PlayerPos.x > 0)
			return Random.Range(GM.LeftBorder*100,0)*0.01f;
		else
			return Random.Range(0,GM.RightBorder*100+1)*0.01f;
	}

	/// <summary>
	/// Возвращает случайную позицию на поле
	/// </summary>
	private Vector3 GetRandomPos()
	{
		float x = Random.Range(GM.LeftBorder*100,GM.RightBorder*100)*0.01f;
		float y = Random.Range(GM.DownBorder*100,GM.UpBorder*100)*0.01f;
		return new Vector3(x, y, 0f);
	}

	/// <summary>
	/// Возвращает случайное время 
	/// </summary>
	private float GetRandomTime(float minSec, float maxSec)
	{
		return Random.Range(minSec*100,maxSec+1*100)*0.01f;
	}
}
