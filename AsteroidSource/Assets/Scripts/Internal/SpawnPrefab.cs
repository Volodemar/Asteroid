using System.Collections.Generic;
using UnityEngine;

internal class SpawnPrefab : MonoBehaviour, ISpawnPrefab
{
	/// <summary>
	/// Список префабов
	/// </summary>
	public List<string> PrefabPath = null;

	/// <summary>
	/// Текущий объект
	/// </summary>
	private Transform spawner;

	private void Awake()
	{
		spawner = this.transform;
	}

	/// <summary>
	/// Спавн префаба в позицию c поворотом
	/// </summary>
	public GameObject OnSpawnPrefab(int prefabNumber, Vector3 position, Quaternion rotate)
	{	
		return OnSpawnPrefab(prefabNumber, position, rotate, spawner);	
	}

	/// <summary>
	/// Спавн префаба в позицию
	/// </summary>
	public GameObject OnSpawnPrefab(int prefabNumber, Vector3 position)
	{	
		return OnSpawnPrefab(prefabNumber, position, Quaternion.identity, spawner);	
	}

	/// <summary>
	/// Спавн префаба
	/// </summary>
	/// <param name="prefabNumber">Номер префаба от 0 до N</param>
	/// <param name="position">Позиция</param>
	/// <param name="quaternion">Поворот</param>
	/// <param name="parent">Родитель</param>
	/// <returns>Новый объект из загруженного префаба.</returns>
	public GameObject OnSpawnPrefab(int prefabNumber, Vector3 position, Quaternion quaternion, Transform parent)
	{
		//Проверка номера
		if(PrefabPath.Count <= prefabNumber)
		{
			Debug.Log("Попытка обращения к префабу за границами списка. Индекс: " + prefabNumber.ToString());
			return null;
		}

		//Проверка доступности префаба
		Object prefab = Resources.Load(PrefabPath[prefabNumber]);
		if(prefab == null)
		{
			Debug.Log("Не найден префаб: " + PrefabPath[prefabNumber]);
			return null;
		}
		
		return Instantiate(prefab, position, quaternion, parent) as GameObject;	
	}
}
