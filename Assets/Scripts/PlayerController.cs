using UnityEngine;
using System.Collections;

public enum Direction { up, down, left, right }

public class PlayerController : MonoBehaviour {
    
    Direction currentDirection = Direction.down;

    public float walkSpeed;
    float walkCutOff = 0.5f;
    Rigidbody2D rigidbody;

    Animator animator;
    int downHash;
    int leftHash;
    int rightHash;
    int upHash;
    int attackHash;
    int walkingHash;

    void Start()
    {
        downHash = Animator.StringToHash("Down");
        leftHash = Animator.StringToHash("Left");
        rightHash = Animator.StringToHash("Right");
        upHash = Animator.StringToHash("Up");
        attackHash = Animator.StringToHash("Attack");
        walkingHash = Animator.StringToHash("Walking");

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 thisVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if(thisVelocity.sqrMagnitude >= (walkCutOff * walkCutOff))
        {
            animator.SetBool(walkingHash, true);
            rigidbody.velocity = thisVelocity * walkSpeed * Time.fixedDeltaTime;
            Direction moveDirection = GetDirection(thisVelocity);

            if (moveDirection != currentDirection)
            {
                ChangeDirection(moveDirection);
            }
        }
        else
        {
            animator.SetBool(walkingHash, false);
            rigidbody.velocity = Vector2.zero;
        }
    }

    Direction GetDirection(Vector2 v)
    {
        float dot = Vector2.Dot(Vector2.up, v);

        if(dot >= 0.5f)
        {
            return Direction.up;
        }else if(dot < 0.5f && dot >= -0.5f)
        {
            if(v.x > 0)
            {
                return Direction.right;
            }
            else
            {
                return Direction.left;
            }
        }
        else
        {
            return Direction.down;
        }
    }

    void ChangeDirection(Direction newDirection)
    {
        if(newDirection != currentDirection)
        {
            currentDirection = newDirection;

            switch (newDirection)
            {
                case Direction.up:
                    animator.SetTrigger(upHash);
                    break;

                case Direction.right:
                    animator.SetTrigger(rightHash);
                    break;

                case Direction.down:
                    animator.SetTrigger(downHash);
                    break;

                case Direction.left:
                    animator.SetTrigger(leftHash);
                    break;
            }
        }
    }
}
