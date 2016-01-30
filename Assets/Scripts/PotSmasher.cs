using UnityEngine;
using System.Collections;

public class PotSmasher : MonoBehaviour {

    public static PotSmasher instance;

    public Sprite smashSprite;
    public GameObject smashParticles;

    void Start()
    {
        instance = this;
    }
}
