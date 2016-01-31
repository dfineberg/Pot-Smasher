using UnityEngine;
using System.Collections;

public class TurtleShell : MonoBehaviour {

    public float moveSpeed;
    public float lifeTime;
    public int damage;

    Direction currentDir;
    Rigidbody2D rigidbody;
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
        switch (currentDir)
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
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Pot"))
        {
            col.GetComponent<PS.Pot>().Damage(damage);
        }

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
    }
}
