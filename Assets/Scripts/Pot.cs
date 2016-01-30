using UnityEngine;
using System.Collections;

namespace PS
{
	public class Pot : MonoBehaviour {

		public int HP;

		// Use this for initialization
		void Start () 
		{
			HP = 5;
		}
		
		// Update is called once per frame
		void OnMouseDown () 
		{
			Damage( 1 );
		}

		public bool Damage( int damage )
		{
			HP -= damage;

			if (HP <= 0)
			{
				HP = 0;
				Break();
				return true;
			}

			Shake();
			return false;
		}

		public virtual void Shake() 
		{
			
		}

		public virtual void Break() 
		{
			this.gameObject.SetActive(false);
		}

		public virtual void BreakImmediately()
		{
			Debug.Log("Break Immediate Pot");
			this.gameObject.SetActive(false);
		}
	}
}
