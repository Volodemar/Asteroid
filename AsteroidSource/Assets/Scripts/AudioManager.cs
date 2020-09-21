using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	/// <summary>
	/// Основной источник звука
	/// </summary>
	public AudioSource soundAudioSource = null;

	/// <summary>
	/// Список файлов музыкальных композиций
	/// </summary>
	public List<string> musicClipFileName;

	//Внешний вызов
	public void PlaySoundOnce(int numberAudio)
	{
		StartCoroutine(PlaySound(numberAudio));
	}

	/// <summary>
	/// Для короутины
	/// </summary>
	private IEnumerator PlaySound(int numberAudio)
	{
		yield return StartCoroutine(LoadSoundClip(musicClipFileName[numberAudio]));
	}

	/// <summary>
	/// Загружает мазыку из ресурсов
	/// </summary>
	IEnumerator LoadSoundClip(string audioFile)
	{
		ResourceRequest request = Resources.LoadAsync(audioFile);
        while (!request.isDone)
        {
            yield return null;
        }

        AudioClip soundClip = (AudioClip)request.asset;

        if (null == soundClip)
        {
            Debug.Log("Музыка не загрузилась: " + audioFile);
        }

		GameObject sound = new GameObject("Sound");
		sound.transform.parent = this.transform;
		AudioSource audioSource = soundAudioSource; //sound.AddComponent<AudioSource>();
		audioSource.PlayOneShot(soundClip);
		Destroy(sound, 1);
	}
}