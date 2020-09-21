using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SizeField : MonoBehaviour, ISizeField
{
	//Объект маркер угла поля
	[SerializeField] public Transform LeftUp    = null;
	[SerializeField] public Transform LeftDown  = null;
	[SerializeField] public Transform RightUp   = null;
	[SerializeField] public Transform RightDown = null;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        Vector2 leftUp      = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
		Vector2 leftDown    = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
		Vector2 rightUp     = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
		Vector2 rightDown   = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));

		//Размещение маркеров по краям в 3D пространстве (может пригодиться фишечка)
        LeftUp.position      = leftUp;
		LeftDown.position    = leftDown;
		RightUp.position     = rightUp;
		RightDown.position   = rightDown;
    }

	/// <summary>
	/// Ограничение поля с лева
	/// </summary>
	public float LeftBorder => LeftUp.position.x;

	/// <summary>
	/// Ограничение поля с верху
	/// </summary>
	public float UpBorder => LeftUp.position.y;

	/// <summary>
	/// Ограничение поля снизу
	/// </summary>
	public float DownBorder => RightDown.position.y;

	/// <summary>
	/// Ограничение поля справа
	/// </summary>
	public float RightBorder => RightDown.position.x;
}
