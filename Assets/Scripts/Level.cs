using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using System.Collections.Generic;

[Serializable]
public class PotData : System.Object
{
	public int HP;
	public float gemChance;

	public PotData(){}
	public PotData(int hp, float gc) { HP = hp; gemChance = gc; }
}

namespace PS
{
	public class Level : MonoBehaviour 
	{
		static List<int> vistedLevels = new List<int>();
		static int PrevLevel = 0;

		static void ResetVisitedLevels()
		{
			vistedLevels = new List<int>();
		}

		public int levelNumber;
		public List<Pot> pots;
		public Door entrance = null;
		public Door exit = null;

		// Use this for initialization
		void Awake()
		{
			//if (exit != null) PlayerController.instance.transform.position = exit.transform.position;
		}

		void Start()
		{
			if (PrevLevel < levelNumber) PlayerController.instance.transform.position = entrance.transform.position;
			else if (PrevLevel > levelNumber) PlayerController.instance.transform.position = exit.transform.position;
			//if (entrance != null) PlayerController.instance.transform.position = entrance.transform.position;
			//else if (Level0Visited) PlayerController.instance.transform.position = exit.transform.position;
			//else { Level0Visited = true; }

			PrevLevel = levelNumber;

			bool found = false;
			foreach (int ln in vistedLevels)
			{
				if ( ln == levelNumber )
				{
					found = true;
					Load();
					break;
				}
			}

			if (!found)
			{
				vistedLevels.Add(levelNumber);
				InitialisePots();
			}
		}

		void InitialisePots()
		{
			UnityEngine.Random.seed = System.DateTime.Now.Millisecond;

			foreach (Pot pot in pots)
			{
				pot.HP = 1;//levelNumber + 1;
				pot.gemChance = UnityEngine.Random.value/ (levelNumber+2);// * 2);
			}
		}

		void OnDestroy()
		{
			Save();
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void Load()
		{
			//Debug.Log("Loading Level " + levelNumber);
			BinaryFormatter bf = new BinaryFormatter();
			string path = Application.persistentDataPath + "/level" + levelNumber + ".dat";
			FileStream file = File.Open(path, FileMode.Open);
			//FileStream file = File.Open("Assets/LevelData/Level_" + levelNumber + ".dat", FileMode.Open );

			PotData[] potdata = bf.Deserialize(file) as PotData[];
			file.Close();

			int len = potdata.Length;
			for ( int i = 0; i < len; i++ )
			{
				pots[i].HP = potdata[i].HP;
				pots[i].gemChance = potdata[i].gemChance;
				if (pots[i].HP == 0) pots[i].BreakImmediately(); //.gameObject.SetActive(false);//
			}
		}

		public void Save()
		{
			//Debug.Log("Saving Level " + levelNumber);
			BinaryFormatter bf = new BinaryFormatter();
			string path = Application.persistentDataPath + "/level" + levelNumber + ".dat";
			if (File.Exists(path)) File.Delete(path);
			FileStream file = File.Create(path);
			//FileStream file = File.Create("Assets/LevelData/Level_" + levelNumber + ".dat");

			PotData[] potdata = new PotData[pots.Count];
			for ( int i = 0; i < pots.Count; i++ )
			{
				potdata[i] = new PotData(pots[i].HP,pots[i].gemChance);
				//potdata[i].HP = pots[i].HP;
				//potdata[i].gemChance = pots[i].gemChance;
			}

			bf.Serialize( file, potdata );
			file.Close();
		}
	};
}


