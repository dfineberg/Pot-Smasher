using UnityEngine;
using System.Collections;

public class TurtleShell : MonoBehaviour {

    public float moveSpeed;
    public float lifeTime;
    public int damage;

    Direction currentDir;
    public Rigidbody2D rigidbody;
    float startTime;

    void Start()
    {
        startTime = Time.time;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetDamage(int d)
    {
        damage = d;
    }

    public void SetDirection(Direction newDir)
    {
        currentDir = newDir;

        switch (newDir)
        {
            case Direction.up:
                rigidbody.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
                break;

            case Direction.down:
                rigidbody.AddForce(Vector2.down * moveSpeed, ForceMode2D.Impulse);
                break;

            case Direction.left:
                rigidbody.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
                break;

            case Direction.right:
                rigidbody.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
                break;
        }
    }

    void Update()
    {
        if(Time.time - startTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        /*switch (currentDir)
        {
            case Direction.up:
                rigidbody.MovePosition(rigidbody.position + Vector2.down * moveSpeed * Time.fixedDeltaTime);
                break;

            case Direction.down:
                rigidbody.MovePosition(rigidbody.position + Vector2.up * moveSpeed * Time.fixedDeltaTime);
                break;

            case Direction.left:
                rigidbody.MovePosition(rigidbody.position + Vector2.right * moveSpeed * Time.fixedDeltaTime);
                break;

            case Direction.right:
                rigidbody.MovePosition(rigidbody.position + Vector2.left * moveSpeed * Time.fixedDeltaTime);
                break;
        }*/
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pot"))
        {
            col.gameObject.GetComponent<PS.Pot>().Damage(damage);
        }

        rigidbody.velocity = Vector2.Reflect(rigidbody.velocity, col.contacts[0].normal);

        /*
        switch (currentDir)
        {
            case Direction.up:
                SetDirection(Direction.down);
                break;

            case Direction.down:
                SetDirection(Direction.up);
                break;

            case Direction.left:
                SetDirection(Direction.right);
                break;

            case Direction.right:
                SetDirection(Direction.left);
                break;
        }
        */
    }
}
