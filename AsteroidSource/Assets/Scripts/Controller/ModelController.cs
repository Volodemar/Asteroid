using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : AsteroidGameObject
{
	[SerializeField] public GameObject model3D;
	[SerializeField] public GameObject model2D;

    void Update()
    {
		//Защита от кривых рук
		if(model3D.activeSelf && model2D.activeSelf)
		{
			model2D.SetActive(false);
			model3D.SetActive(false);
		}

		//Если включен режим 3D отобразить 3D модель
		if(GM.TypeGame && !model3D.activeSelf)
		{
			model2D.SetActive(false);
			model3D.SetActive(true);			
		}

		//Если включен режим 2D отобразить 2D модель
		if(!GM.TypeGame && !model2D.activeSelf)
		{
			model3D.SetActive(false);
			model2D.SetActive(true);			
		}
    }
}