using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PS
{
	public enum SOUND_ID
	{
		GEM_1,
		POT_BREAK_1
	}

	public class SoundController : MonoBehaviour 
	{
		public AudioSource audioSource;
		public List<AudioClip> soundList;

		public static SoundController Instance{private set;get;}

		void Awake () 
		{
			if ( Instance ) DestroyImmediate( this.gameObject );
			else Instance = this;
		}

		public void PlaySound(SOUND_ID soundID)
		{
			audioSource.clip = soundList[(int)soundID];
			audioSource.Play();
		}
	}
}
