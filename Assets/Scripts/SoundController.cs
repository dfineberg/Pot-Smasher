using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PS
{
	public enum SOUND_ID
	{
		NONE,
		POT_SHAKE
	}

	class PLAY_SOUND
	{
		public SOUND_ID ID;
		public float VOLUME;

		public PLAY_SOUND(SOUND_ID id = SOUND_ID.NONE, float volume = 0.0f)
		{
			ID = id; VOLUME = volume;
		}
	}

	public class SoundController : MonoBehaviour 
	{
		Dictionary<SOUND_ID,AudioSource> soundList;

		const int MAX_PENDING = 10;
		int HEAD;
		int TAIL;
		PLAY_SOUND[] soundQueue;

		public static SoundController Instance{private set;get;}

		void Awake () 
		{
			if ( Instance ) DestroyImmediate( this.gameObject );
			else Instance = this;

			DontDestroyOnLoad(this);
			soundList = new Dictionary<SOUND_ID, AudioSource>();
			soundQueue = new PLAY_SOUND[MAX_PENDING];
			HEAD = TAIL = 0;
		}

		// Use this for initialization
		public void PlaySound (SOUND_ID id, float volume = 0.5f) 
		{
			volume = Mathf.Clamp01(volume);

			// Walk the pending requests.
			for (int i = HEAD; i != TAIL; i = (i + 1) % MAX_PENDING)
			{
				if (soundQueue[i].ID == id)
				{
					// Use the larger of the two volumes.
					soundQueue[i].VOLUME = Mathf.Max(volume, soundQueue[i].VOLUME);

					// Don't need to enqueue.
					return;
				}
			}
				
			if((TAIL + 1) % MAX_PENDING != HEAD)
			{
				// Add to the end of the list.
				soundQueue[TAIL].ID = id;
				soundQueue[TAIL].VOLUME = volume;
				TAIL = (TAIL + 1) % MAX_PENDING;
			}
		}
		
		// Update is called once per frame
		void LateUpdate () 
		{
			// If there are no pending requests, do nothing.
			if (HEAD == TAIL) return;

			AudioSource sound = soundList[soundQueue[HEAD].ID];
			sound.volume = soundQueue[HEAD].VOLUME;
			sound.Play();
			HEAD = (HEAD + 1) % MAX_PENDING;
		}
	}
}
