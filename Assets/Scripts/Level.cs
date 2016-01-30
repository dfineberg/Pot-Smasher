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
		public int levelNumber;
		public List<Pot> pots;

		// Use this for initialization
		void Start () {

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
			BinaryFormatter bf = new BinaryFormatter(); 
			FileStream file = File.Open(Application.persistentDataPath + "/level" + levelNumber + ".dat", FileMode.Open );

			PotData[] potdata = bf.Deserialize(file) as PotData[];
			file.Close();

			int len = potdata.Length;
			for ( int i = 0; i < len; i++ )
			{
				if (potdata[i].HP == 0) pots[i].BreakImmediately();
			}
		}

		public void Save()
		{
			BinaryFormatter bf = new BinaryFormatter(); 
			FileStream file = File.Create(Application.persistentDataPath + "/level" + levelNumber + ".dat");//, FileOptions.RandomAccess);

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


