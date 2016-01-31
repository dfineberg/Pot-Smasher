using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed;

    int damage;
    Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetDamage(int d)
    {
        damage = d;
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + (Vector2)(transform.TransformDirection(Vector3.up) * speed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Pot"))
        {
            col.GetComponent<PS.Pot>().Damage(damage);
        }

        if (!col.CompareTag("Player") && !col.GetComponent<Bullet>())
        {
            Destroy(gameObject);
        }
    }
}
