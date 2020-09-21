using UnityEngine;

//Обычный таймер
internal class Timer : MonoBehaviour, ITimer
{
	//Количество секунд по умолчанию
    [SerializeField] private int StartSeconds = 10;
	private float _seconds = 10;

	/// <summary>
	/// Точный остаток времени
	/// </summary>
	[HideInInspector] public float RemainingTime => _seconds;

	/// <summary>
	/// Остаток времени в секундах
	/// </summary>
	[HideInInspector] public int RemainingSecond => Mathf.FloorToInt(_seconds % 60);

	/// <summary>
	/// Остаток времени в минутах
	/// </summary>
	[HideInInspector] public int RemainingMinutes => Mathf.FloorToInt(_seconds / 60);

	/// <summary>
	/// Остаток времени в часах
	/// </summary>
	[HideInInspector] public int RemainingHours => Mathf.FloorToInt(_seconds / 3600);

	//Активен не активен
	private bool isActive = false;

    private void Update()
    {
		if(isActive)
		{
			if (_seconds > 0)
			{
				_seconds -= Time.deltaTime;
			}
			else
				_seconds = 0;
		}
    }

	/// <summary>
	/// Старт таймера
	/// </summary>
	public void StartTimer()
	{
		StartTimer(StartSeconds);
	}

	/// <summary>
	/// Старт таймера
	/// </summary>
	/// <param name="in_second">Время в секундах</param>
	public void StartTimer(int in_second)
	{
		if(in_second != 0)
			_seconds = in_second;

		isActive = true;
	}

	/// <summary>
	/// Остановить таймер
	/// </summary>
	public void StopTimer()
	{
		isActive = false;
	}

	/// <summary>
	/// Возвращает активность таймера
	/// </summary>
	public bool IsActive()
	{
		return isActive;
	}
}