using UnityEngine;

internal interface ISpawnPrefab
{
	GameObject OnSpawnPrefab(int prefabNumber, Vector3 position, Quaternion quaternion, Transform parent);
}