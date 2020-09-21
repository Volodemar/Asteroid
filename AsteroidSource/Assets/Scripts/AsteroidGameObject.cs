using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AsteroidGameObject : MonoBehaviour
{
	private GameManager _gm; 
	private Transform _tr;
	private GameObject _go;
	private AudioManager _audio;

	public GameManager GM => _gm;
	public Transform TR => _tr;
	public GameObject GO => _go;
	public AudioManager AUDIO => _audio;

    public virtual void Awake()
    {
        _gm    = GameObject.Find("GameManager").GetComponent<GameManager>();
		_audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		_tr    = this.transform;
		_go    = this.gameObject;
    }

	#region Подписка на обработку событий
		public virtual void OnEnable()
		{
			EventManager.OnAction += GetAction;
		}

		public virtual void OnDisable()
		{
			EventManager.OnAction -= GetAction;
		}

		public virtual void OnDestroy()
		{
			EventManager.OnAction -= GetAction;
		}

		/// <summary>
		/// Слушаем события
		/// </summary>
		/// <param name="resourceID">Идентификатор события</param>
		public virtual void GetAction(string resourceID)
		{	
		}
	#endregion
}
