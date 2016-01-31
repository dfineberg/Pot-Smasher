using UnityEngine;
using System.Collections;

namespace PS
{
	public class Door : MonoBehaviour {

		public int levelNumber;

		static bool canLoadLevel = true;

		// Use this for initialization
		void OnTriggerEnter2D(Collider2D other) 
		{
			if (other.tag == "Player" && canLoadLevel)
			{
				// load level sequence
				canLoadLevel = false;
				LevelLoader.Instance.LoadLevel(levelNumber);
			}
		}

		void OnTriggerExit2D(Collider2D other) 
		{
			if (other.tag == "Player")
			{
				// load level sequence
				canLoadLevel = true;
			}
		}
	}
}
