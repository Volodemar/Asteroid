using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : AsteroidGameObject
{
	/// <summary>
	/// Тип патрона лазер или пуля
	/// </summary>
	public bool isBullet = false;

	public float SpeedBullet = 300;
	public float SpeedLaser = 600;

	/// <summary>
	/// Кешированные объекты
	/// </summary>
	private Rigidbody rb3D;

    public override void Awake()
    {
		base.Awake();
		rb3D = GO.GetComponent<Rigidbody>();
    }

    private void Update()
    {
		//Снаряд двигается в одну сторону с постоянной скоростью
		if(isBullet)
			rb3D.velocity = -TR.up * SpeedBullet * Time.deltaTime;
		else
			rb3D.velocity = -TR.up * SpeedLaser * Time.deltaTime;
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Rock"))
		{
			RockController rc = other.GetComponent<RockController>();

			if(isBullet)
			{
				rc.OnBulletDestroy();
				Destroy(GO);
			}
			else
			{
				rc.OnLaserDestroy();
			}
		}

		if(other.CompareTag("UFO"))
		{
			UFOController uc = other.GetComponent<UFOController>();

			if(isBullet)
			{
				uc.OnBulletDestroy();
				Destroy(GO);
			}
			else
			{
				uc.OnBulletDestroy();
			}
		}
	}
}
