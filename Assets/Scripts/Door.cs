﻿using UnityEngine;
using System.Collections;

namespace PS
{
	public class Door : MonoBehaviour {

		public int levelNumber;

		// Use this for initialization
		void OnTriggerEnter2D(Collider2D other) 
		{
			if (other.tag == "Player")
			{
				// load level sequence
				LevelLoader.Instance.LoadLevel(levelNumber);
			}
		}
	}
}
