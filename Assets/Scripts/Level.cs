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

	public PotData(){}
}

namespace PS
{
	public class Level : MonoBehaviour 
	{
		//static bool Level0Visited = false;
		static int PrevLevel = 0;

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

			Load();
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
			FileStream file = File.Open(Application.persistentDataPath + "/level" + levelNumber + ".dat", FileMode.Open );
			//FileStream file = File.Open("Assets/LevelData/Level_" + levelNumber + ".dat", FileMode.Open );

			PotData[] potdata = bf.Deserialize(file) as PotData[];
			file.Close();

			int len = potdata.Length;
			for ( int i = 0; i < len; i++ )
			{
				pots[i].HP = potdata[i].HP;
				if (pots[i].HP == 0) pots[i].BreakImmediately(); //.gameObject.SetActive(false);//
			}
		}

		public void Save()
		{
			//Debug.Log("Saving Level " + levelNumber);
			BinaryFormatter bf = new BinaryFormatter(); 
			FileStream file = File.Create(Application.persistentDataPath + "/level" + levelNumber + ".dat");
			//FileStream file = File.Create("Assets/LevelData/Level_" + levelNumber + ".dat");

			PotData[] potdata = new PotData[pots.Count];
			for ( int i = 0; i < pots.Count; i++ )
			{
				potdata[i] = new PotData();
				potdata[i].HP = pots[i].HP;
			}

			bf.Serialize( file, potdata );
			file.Close();
		}
	};
}


