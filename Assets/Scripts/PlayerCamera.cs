using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public static PlayerCamera Instance{private set;get;}

	void Awake () 
	{
		if ( Instance ) DestroyImmediate( this.gameObject );
		else Instance = this;
	}

	PlayerController player;
	Vector2 offSet;
	public float shakeForce = 5.0f;
	public float returnSpeed = 10.0f;
	public float maxOffset = 2.0f;

	// Use this for initialization
	public void Shake () 
	{
		Vector2 nudge = new Vector2((Random.value * 2 - 1) * shakeForce, (Random.value * 2 - 1) * shakeForce);
		offSet += nudge;

		if (offSet.magnitude > maxOffset) offSet = offSet.normalized * maxOffset;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.P)) Shake();

		if (player == null) player = PlayerController.instance;
		transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0,0,-10) + (Vector3)offSet, Time.deltaTime);

		offSet = Vector2.Lerp( offSet, Vector2.zero, Time.deltaTime * returnSpeed );
	}
}
