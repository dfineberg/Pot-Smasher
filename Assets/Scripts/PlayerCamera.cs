using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	PlayerController player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (player == null) player = PlayerController.instance;
		transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0,0,-10), Time.deltaTime);
	}
}
