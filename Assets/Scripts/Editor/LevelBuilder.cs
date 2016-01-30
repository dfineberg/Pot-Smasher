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
			GameObject levelCopy = (GameObject)Instantiate(GameObject.FindObjectOfType<Level>().transform.gameObject);
			Level level = levelCopy.GetComponent<Level>();// (Level)GameObject.FindObjectOfType<Level>();
			level.pots = new List<Pot>();
			level.doors = new List<Door>();

			SpriteRenderer[] spriteObjects = levelCopy.GetComponentsInChildren<SpriteRenderer>();
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
				else if (s.gameObject.tag == "Door")
				{
					Door door = s.gameObject.GetComponent<Door>();
					if (!door)
					{
						s.gameObject.AddComponent<Door>();
						door = s.gameObject.GetComponent<Door>();
					}

					level.doors.Add(door);
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
			level.Save();
		}

	}
}
