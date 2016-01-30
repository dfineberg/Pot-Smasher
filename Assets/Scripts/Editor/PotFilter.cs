using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

namespace PS
{
	public class PotFilter : MonoBehaviour {

		// add menu item named "Filter Pots" to the Tools menu
		[MenuItem("Tools/Build Level")]
		public static void ShowWindow()
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
	}
}
