using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PS
{
	public enum SOUND_ID
	{
		GEM_1,
		POT_BREAK_1,
		POWER_UP_1,
		POWER_UP_2,
		POWER_UP_3,
		ENTER_ROOM_1,
		ENTER_ROOM_2,
		ENTER_ROOM_3,
		SCYTHE_1,
		SCYTHE_2,
		SWORD_1,
		BOOMERANG_1
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
