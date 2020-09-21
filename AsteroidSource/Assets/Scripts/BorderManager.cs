using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderManager : AsteroidGameObject
{
	public Transform objTransform = null;
	public bool isTeleport = false;
	public bool isDestroy  = false;

	private void Update()
    {
        if(isTeleport)
			Teleport();

		if(isDestroy)
			DestroyObject();
    }

	/// <summary>
	/// Телепорт в противоположную часть поля
	/// </summary>
	private void Teleport()
	{
		//Телепорт при выходе за границы
		if (objTransform.position.x > GM.RightBorder)
			objTransform.position = new Vector3(objTransform.position.x * -1 + 0.1f, objTransform.position.y, objTransform.position.z);
		else if (objTransform.position.x < GM.LeftBorder)
			objTransform.position = new Vector3(objTransform.position.x * -1 - 0.1f, objTransform.position.y, objTransform.position.z);

		if (objTransform.position.y > GM.DownBorder)
			objTransform.position = new Vector3(objTransform.position.x, objTransform.position.y * -1 + 0.1f, objTransform.position.z);
		else if (objTransform.position.y < GM.UpBorder)
			objTransform.position = new Vector3(objTransform.position.x, objTransform.position.y * -1 - 0.1f, objTransform.position.z);
	}

	/// <summary>
	/// Уничтожение объекта при выходе за границы
	/// </summary>
	private void DestroyObject()
	{
		//Телепорт при выходе за границы
		if (objTransform.position.x > GM.RightBorder)
			Destroy(GO);
		else if (objTransform.position.x < GM.LeftBorder)
			Destroy(GO);

		if (objTransform.position.y > GM.DownBorder)
			Destroy(GO);
		else if (objTransform.position.y < GM.UpBorder)
			Destroy(GO);
	}
}
