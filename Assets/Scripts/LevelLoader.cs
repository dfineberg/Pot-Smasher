using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using System.Collections.Generic;

namespace PS
{
	public class LevelLoader : MonoBehaviour 
	{
		public int currentLevelNumber;
		public SpriteRenderer fadeSprite;
		public float fadeSpeed = 1.0f;

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
			//Reset();
			LoadLevelResource(currentLevelNumber);
			StartCoroutine(FadeToLevel());
		}

		/*
		void Reset()
		{
			Debug.Log("Resetting Levels");
			for (int i = 1; i < 999; ++i )
			{
				//("fontname",typeof(Font)) as Font;
				GameObject level = Resources.Load(i.ToString(),typeof(GameObject)) as GameObject;

				if (level != null)
				{
					level.GetComponent<Level>().Save();
				}


				else 
				{
					Debug.Log("No Level " + i + "to Reset");
					break;
				}
			}
		}
		*/

		/*
		void ResetVisited ()
		{
			foreach (int levelNumber in visitedLevels)
			{
				//("fontname",typeof(Font)) as Font;
				GameObject level = Resources.Load(levelNumber.ToString(),typeof(GameObject)) as GameObject;
				Debug.Log("Resetting Level " + levelNumber);
				if (level)
				{
					level.GetComponent<Level>().Save();
				}
				else break;
			}
		}
		*/

		public void LoadLevel(int levelNumber)
		{
			if (loading) return;
			loading = true;
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
			currentLevelNumber = levelNumber;

			// delete the current level if it exists
			if (currentLevel != null)
			{
				currentLevel.Save();
				Destroy(currentLevel.gameObject);
				currentLevel = null;
			}

			// load the level resource and get the level object
			GameObject level = (GameObject)Instantiate(Resources.Load(levelNumber.ToString()));
			if (level == null)
			{
				Debug.Log("Could not load level " + levelNumber + " from Resources folder");
				return;
			}
		
			// set the current level as the new level
			currentLevel = level.GetComponent<Level>();
		}

		IEnumerator LoadLevelSequence(int levelNumber)
		{
			Debug.Log("Loading level " + levelNumber);

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

