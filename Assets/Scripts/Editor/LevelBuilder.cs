using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

namespace PS
{
	public class LevelBuilder : Editor {

		/*
		[MenuItem("Tools/Build Level")]
		public static void ShowBuildLevel()
		{
			BuildLevel();
		}
		
		static void BuildLevel()
		{
			Level level = (Level)GameObject.FindObjectOfType<Level>();
			level.pots = new List<Pot>();

			SpriteRenderer[] spriteObjects = GameObject.FindObjectsOfType<SpriteRenderer>();
			foreach ( SpriteRenderer s in spriteObjects )
			{
				if (s.gameObject.tag == "Pot")
				{
					Pot pot = s.gameObject.GetComponent<Pot>();
					if (!pot)
					{
						s.gameObject.AddComponent<Pot>();
						pot = s.gameObject.GetComponent<Pot>();
					}

					level.pots.Add(pot);
				}
					
			}

			level.Save();
		}
		*/

		[MenuItem("Tools/Build Level Prefab")]
		public static void ShowBuildLevelPrefab()
		{
			BuildLevelPrefab();
		}

		static void BuildLevelPrefab()
		{
			// check if this level has been copied first
			int tryLevel = GameObject.FindObjectOfType<Level>().levelNumber;
			GameObject levelPrefabExists = Resources.Load(tryLevel.ToString(),typeof(GameObject)) as GameObject;
			if (levelPrefabExists)
			{
				Debug.Log("Level " + tryLevel + " already exists in the prefab folder!");
				return;
			}

			GameObject levelCopy = (GameObject)Instantiate(GameObject.FindObjectOfType<Level>().transform.gameObject);
			Level level = levelCopy.GetComponent<Level>();



			// get list of sprites in the level copy
			SpriteRenderer[] spriteObjects = levelCopy.GetComponentsInChildren<SpriteRenderer>();

			/*
			// we need to check if the level has an entrance and an exit
			bool hasEntrance = (level.levelNumber == 0);
			bool hasExit = false;

			foreach ( SpriteRenderer s in spriteObjects )
			{
				if (s.gameObject.tag == "Exit")
				{
					hasExit = true;

					Door exit = s.gameObject.GetComponent<Door>();
					if (!exit)
					{
						s.gameObject.AddComponent<Door>();
						exit = s.gameObject.GetComponent<Door>();
					}

					exit.levelNumber = level.levelNumber + 1;
					level.exit = exit;
				}

				else if (s.gameObject.tag == "Entrance")
				{
					hasEntrance = true;

					Door entrance = s.gameObject.GetComponent<Door>();
					if (!entrance)
					{
						s.gameObject.AddComponent<Door>();
						entrance = s.gameObject.GetComponent<Door>();
					}

					entrance.levelNumber = level.levelNumber - 1;
					level.entrance = entrance;
				}

				if (hasExit && hasEntrance) break;
			}

			if (!hasEntrance || !hasExit)
			{
				if (!hasEntrance) Debug.Log( "No object tagged as [Entrance]");
				if (!hasExit) Debug.Log( "No object tagged as [Exit]");
				DestroyImmediate (levelCopy);
				return;
			}
			*/

			level.pots = new List<Pot>();

			foreach ( SpriteRenderer s in spriteObjects )
			{
				if (s.gameObject.tag == "Pot")
				{
					Pot pot = s.gameObject.GetComponent<Pot>();
					if (!pot)
					{
						s.gameObject.AddComponent<Pot>();
						pot = s.gameObject.GetComponent<Pot>();
					}

					level.pots.Add(pot);
				}

				if (s.gameObject.tag == "Exit")
				{
					//hasExit = true;

					Door exit = s.gameObject.GetComponent<Door>();
					if (!exit)
					{
						s.gameObject.AddComponent<Door>();
						exit = s.gameObject.GetComponent<Door>();
					}

					exit.levelNumber = level.levelNumber + 1;
					level.exit = exit;
				}

				else if (s.gameObject.tag == "Entrance")
				{
					//hasEntrance = true;

					Door entrance = s.gameObject.GetComponent<Door>();
					if (!entrance)
					{
						s.gameObject.AddComponent<Door>();
						entrance = s.gameObject.GetComponent<Door>();
					}

					entrance.levelNumber = level.levelNumber - 1;
					level.entrance = entrance;
				}
			}

			// find the tile layers
			TileEditor.TileLayer[] tileLayers = levelCopy.GetComponentsInChildren<TileEditor.TileLayer>();
			foreach (TileEditor.TileLayer layer in tileLayers) 
			{
				// destroy the tiles in each layer
				TileEditor.Tile[] tiles = layer.gameObject.GetComponentsInChildren<TileEditor.Tile>();
				foreach (TileEditor.Tile tile in tiles) DestroyImmediate(tile);
				// destroy the layer
				DestroyImmediate(layer);
			}

			// destroy tilemap
			DestroyImmediate(levelCopy.GetComponent<TileEditor.TileMap>());

			level.transform.name = level.levelNumber.ToString();
			//level.Save();
		}

	}
}
