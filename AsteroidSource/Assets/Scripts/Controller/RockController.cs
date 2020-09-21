using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : AsteroidGameObject
{
	public float RockSpeed = 0.3f;

	/// <summary>
	/// Тип большой - 0, средний - 1, мелкий - 2
	/// </summary>
	public int TypeRock = 0;

	private Vector3 direction;

    void Start()
    {
		direction = new Vector3(Random.Range(-100, 101) * 0.01f, Random.Range(-100, 101) * 0.01f, 0f);
    }

    void Update()
    {
        transform.Translate(direction.normalized * RockSpeed * Time.deltaTime);
    }

	/// <summary>
	/// Разрушение пулей
	/// </summary>
	public void OnBulletDestroy()
	{
		//Уничтожение и разброс мелких
		if(TypeRock == 0)
		{
			AUDIO.PlaySoundOnce(0);
			GM.SetScore(1);
			SpawnerController spawner = this.GetComponentInParent<SpawnerController>();
			spawner.OnSpawnMediumRock(TR.position);
			Destroy(this.gameObject);
		}
		else if(TypeRock == 1)
		{
			AUDIO.PlaySoundOnce(0);
			GM.SetScore(1);
			SpawnerController spawner = this.GetComponentInParent<SpawnerController>();
			spawner.OnSpawnSmallRock(TR.position);
			Destroy(this.gameObject);
		}
	}

	/// <summary>
	/// Разрушение лазером
	/// </summary>
	public void OnLaserDestroy()
	{
		AUDIO.PlaySoundOnce(0);
		GM.SetScore(1);
		SpawnerController spawner = this.GetComponentInParent<SpawnerController>();
		spawner.OnSpawnSmallRock(TR.position);
		Destroy(this.gameObject);
	}
}
