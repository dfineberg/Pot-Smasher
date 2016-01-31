using UnityEngine;
using System.Collections;

public class Mace : MonoBehaviour {

    public int damage;
    public float damageSpeed;
    Rigidbody2D rigidbody;
    public DistanceJoint2D playerJoint;
    LineRenderer lineRenderer;

    public Transform[] chainTransforms;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

	void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Pot") && rigidbody.velocity.sqrMagnitude >= damageSpeed * damageSpeed)
        {
            col.gameObject.GetComponent<PS.Pot>().Damage(damage);
        }
    }

    void Update()
    {
        for(int i = 0; i < chainTransforms.Length; i++)
        {
            lineRenderer.SetPosition(i, chainTransforms[i].position + (i == 3 ? new Vector3(0f, -0.5f, 1f) : Vector3.back));
        }
    }
}
