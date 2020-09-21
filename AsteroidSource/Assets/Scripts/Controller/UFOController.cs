using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : AsteroidGameObject
{
	public float UFOSpeed = 0.6f;
	public int TimerFindPlayer = 2;
	private Vector3 direction = Vector2.zero;
	private Timer timer;

	public override void Awake()
	{
		base.Awake();
		timer = this.GetComponent<Timer>();
	}

	private void Start()
	{
		GetDirection();
		timer.StartTimer(TimerFindPlayer);
	}

	private void Update()
	{
		if(timer.RemainingTime == 0)
		{
			GetDirection();
			timer.StartTimer(TimerFindPlayer);
		}

		if(direction != Vector3.zero)
			transform.Translate(direction * UFOSpeed * Time.deltaTime);
	}

	private void GetDirection()
	{
		if(GM.PlayerPos.x >= TR.position.x)
			direction = Vector3.right;
		else
			direction = Vector3.left;
	}

	/// <summary>
	/// Попадание снаряда
	/// </summary>
	public void OnBulletDestroy()
	{
		GM.SetScore(1);

		//Уничтожение и разброс мелких
		Destroy(GO);
	}
}
