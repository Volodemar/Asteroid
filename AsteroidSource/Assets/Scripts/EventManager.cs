using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
	public static Action<string> OnAction;

	public static void OnActionSend(string resourceID)
	{
		OnAction?.Invoke(resourceID);
	}
}
