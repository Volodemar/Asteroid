using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AsteroidGameObject
{
	/// <summary>
	/// Кешированная трансформация
	/// </summary>
	public Transform model;

	/// <summary>
	/// Физическое тело персонажа в 3D
	/// </summary>
	public Rigidbody rb3D;

	/// <summary>
	/// Скорость ускорения
	/// </summary>
	public float SpeedMove = 1;

	/// <summary>
	/// Скорость поворота
	/// </summary>
	public float SpeedRotate = 3;

	/// <summary>
	/// Скорость максимальная
	/// </summary>
	public float SpeedMax = 2;

	/// <summary>
	/// Позиция откуда будет лететь пуля
	/// </summary>
	public Transform BullPos;

	/// <summary>
	/// Хранил скорость корабля
	/// </summary>
	private float speed = 10;

	/// <summary>
	/// Хранит направление корабля
	/// </summary>
	private Vector3 direction;

	/// <summary>
	/// Спавнер снарядов
	/// </summary>
	[SerializeField] private SpawnPrefab spawner = null;

	/// <summary>
	/// Основной объект для корректного удаления
	/// </summary>
	[SerializeField] private GameObject destroyObject = null;

	public override void Awake()
	{
		base.Awake();
		direction = -model.up;
	}

	private void Update()
	{
		//Управление
		float playerMoveH = Input.GetAxis("Horizontal");
		float playerMoveV = Input.GetAxis("Vertical");
		speed = playerMoveV * SpeedMove * Time.deltaTime;
		if(playerMoveV > 0) direction = -model.up;
		Vector3 velocity = direction * speed;

		//Движение в направлении
		if (playerMoveV > 0)
		{
			rb3D.AddForce(velocity, ForceMode.Impulse);		
		}

		//Поворот персонажа
		model.Rotate(new Vector3(0,0,-playerMoveH * SpeedRotate));

		//Ограничитель скорости
		if (rb3D.velocity.magnitude > SpeedMax)
		rb3D.velocity = rb3D.velocity.normalized * SpeedMax;

		//Синхронизация положения тела и модели т.к. модель может вертеться как голова
		model.position = rb3D.position;

		//Расшариваем позицию игрока
		GM.PlayerPos = model.position;

		//Стрельба пулями
		if(Input.GetKeyDown(KeyCode.Space))
		{
			AUDIO.PlaySoundOnce(2);
			spawner.OnSpawnPrefab(0, model.position, model.rotation);
		}

		//Стрельба лазером
		if(Input.GetKeyDown(KeyCode.F) && GM.GameData.Laser > 0)
		{
			AUDIO.PlaySoundOnce(1);
			GM.SetLaser(-1);
			spawner.OnSpawnPrefab(1, model.position, model.rotation);
		}
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Rock"))
		{
			GM.SetLife(-1);
			GM.SetDeath(+1);
			AUDIO.PlaySoundOnce(0);

			//Оповещение что игрок умер
			EventManager.OnAction("PlayerDie");

			Destroy(destroyObject);	
		}

		if(other.CompareTag("UFO"))
		{
			GM.SetLife(-1);
			GM.SetDeath(+1);
			AUDIO.PlaySoundOnce(0);

			//Оповещение что игрок умер
			EventManager.OnAction("PlayerDie");
			Destroy(destroyObject);
		}
	}
}
