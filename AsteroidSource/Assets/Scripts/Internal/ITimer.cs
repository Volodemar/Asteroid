internal interface ITimer
{
	bool IsActive();
	void StartTimer(int in_second);
	void StopTimer();
}