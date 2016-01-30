using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


namespace PS
{
	public class LevelLoader : MonoBehaviour 
	{
		int levelNumber = 1;

		public static LevelLoader Instance{private set;get;}

		void Awake () {

			Instance = this;

			if ( Instance )
			{
				DestroyImmediate( this.gameObject );
			}

			else
			{
				Instance = this;
			}
		}

		public void LoadLevel(int levelNumber)
		{
			++levelNumber;

			SceneManager.LoadScene( "level" + levelNumber );
		}
	}
}

