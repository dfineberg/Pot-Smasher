using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using System.Collections.Generic;

namespace PS
{
	public class LevelLoader : MonoBehaviour 
	{
		public SpriteRenderer fadeSprite;
		public float fadeSpeed = 1.0f;

		List<int>visitedLevels;
		bool loading = false;
		Level currentLevel = null;

		public static LevelLoader Instance{private set;get;}

		void Awake () 
		{
			if ( Instance ) DestroyImmediate( this.gameObject );
			else Instance = this;

			fadeSprite.gameObject.SetActive(true);
		}

		void Start ()
		{
			visitedLevels = new List<int>();
			LoadLevelResource(1);
			StartCoroutine(FadeToLevel());
		}

		public void LoadLevel(int levelNumber)
		{
			if (loading) return;
			StartCoroutine(LoadLevelSequence(levelNumber));
		}

		bool fadeOut( float delta )
		{
			Color c = fadeSprite.color;
			c.a -= delta * fadeSpeed;
			if (c.a < 0.0f) c.a = 0.0f;
			fadeSprite.color = c;
			if (fadeSprite.color.a == 0.0f )
			{
				return true;
			}
			return false;
		}

		bool fadeIn( float delta )
		{
			Color c = fadeSprite.color;
			c.a += delta * fadeSpeed;
			if (c.a > 1.0f) c.a = 1.0f;
			fadeSprite.color = c;
			if (fadeSprite.color.a == 1.0f )
			{
				return true;
			}
			return false;
		}

		IEnumerator FadeToBlack()
		{
			// fade screen to black
			fadeSprite.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

			while (!fadeIn(Time.deltaTime))
			{
				yield return false;
			}

			yield return true;
		}

		IEnumerator FadeToLevel()
		{
			// fade screen to black
			fadeSprite.color = Color.white;

			while (!fadeOut(Time.deltaTime))
			{
				yield return false;
			}

			yield return true;
		}

		void LoadLevelResource(int levelNumber)
		{
			// load the level resource and get the level object
			Resources.Load(levelNumber.ToString());

			// delete the current level if it exists
			if (currentLevel != null)
			{
				DestroyImmediate(currentLevel.gameObject);
				currentLevel = null;
			}
		
			// set the current level as the new level
			currentLevel = GameObject.FindObjectOfType<Level>();
		}

		IEnumerator LoadLevelSequence(int levelNumber)
		{
			// fade screen to black
			yield return StartCoroutine(FadeToBlack());

			// load level resource
			LoadLevelResource(levelNumber);

			// fade level back in
			yield return StartCoroutine(FadeToLevel());

			// level has been loaded
			loading = false;

			yield return true;
		}
	}
}

