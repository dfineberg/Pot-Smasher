using UnityEngine;
using System.Collections;

public class ParticleLifetime : MonoBehaviour {

    ParticleSystem particles;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        StartCoroutine(Lifetime());
    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(particles.duration);
        Destroy(gameObject);
    }
}
