using UnityEngine;
using System.Collections;

namespace PS
{
	public class Pot : MonoBehaviour {

		public int HP;
        public float gemChance;

		// Use this for initialization
		void OnEnable () 
		{
			HP = 1;
            gemChance = 0.6f;
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
            if (Random.value <= gemChance) Instantiate(PotSmasher.instance.gemPrefab, transform.position, Quaternion.identity);
            Instantiate(PotSmasher.instance.smashParticles, transform.position, Quaternion.identity);
            BreakImmediately();
		}

		public virtual void BreakImmediately()
		{
			Debug.Log("Break Immediate Pot");
            GetComponent<SpriteRenderer>().sprite = PotSmasher.instance.smashSprite;
            GetComponent<Collider2D>().enabled = false;
		}
	}
}
