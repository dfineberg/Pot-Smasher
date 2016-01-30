using UnityEngine;
using System.Collections;

namespace PS
{
	public class Pot : MonoBehaviour {

		public int HP;
        public float gemChance;

		// Use this for initialization
		void Reset () 
		{
			HP = 5;
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
			StartCoroutine(ShakeRoutine());
		}

		IEnumerator ShakeRoutine()
		{
			SpriteRenderer renderer = GetComponent<SpriteRenderer>();

			Color changingColor = Color.red;
			renderer.color = changingColor;

			yield return false;

			while (true)
			{
				changingColor.r = Mathf.Clamp01( changingColor.r + Time.deltaTime * 5.0f );
				changingColor.g = Mathf.Clamp01( changingColor.g + Time.deltaTime * 5.0f);
				changingColor.b = Mathf.Clamp01( changingColor.b + Time.deltaTime * 5.0f);
				renderer.color = changingColor;

				if (changingColor.r == 1 && changingColor.g == 1 && changingColor.b == 1 ) break;
				yield return false;
			}

			yield return true;
		}

		public virtual void Break() 
		{
            if (Random.value <= gemChance) Instantiate(PotSmasher.instance.gemPrefab, transform.position, Quaternion.identity);
            Instantiate(PotSmasher.instance.smashParticles, transform.position, Quaternion.identity);
            BreakImmediately();
		}

		public virtual void BreakImmediately()
		{
			//Debug.Log("Break Immediate Pot");
            GetComponent<SpriteRenderer>().sprite = PotSmasher.instance.smashSprite;
            GetComponent<Collider2D>().enabled = false;
		}
	}
}
