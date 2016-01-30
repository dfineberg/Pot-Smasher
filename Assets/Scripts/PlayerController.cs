using UnityEngine;
using System.Collections;

public enum Direction { up, down, left, right }

public class PlayerController : MonoBehaviour {
    
    Direction currentDirection = Direction.down;

    public Weapon weapon;
    bool attacking = false;
    bool attackAnimFinish = false;

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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !attacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    void FixedUpdate()
    {
        Vector2 thisVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if(thisVelocity.sqrMagnitude >= (walkCutOff * walkCutOff) && !attacking)
        {
            Move(thisVelocity);
        }
        else
        {
            animator.SetBool(walkingHash, false);
            rigidbody.velocity = Vector2.zero;
        }
    }

    public void Move(Vector2 moveVector)
    {
        animator.SetBool(walkingHash, true);
        rigidbody.velocity = moveVector.normalized * walkSpeed;
        Direction moveDirection = GetDirection(moveVector);

        if (moveDirection != currentDirection)
        {
            ChangeDirection(moveDirection);
        }
    }

    IEnumerator AttackRoutine()
    {
        weapon.Attack(currentDirection);
        animator.SetTrigger(attackHash);
        attacking = true;

        while (attacking)
        {
            if (attackAnimFinish)
            {
                if(weapon != null)
                {
                    if (weapon.WaitWhileAttacking())
                    {
                        if (!weapon.IsAttacking())
                        {
                            attacking = false;
                        }
                    }
                    else
                    {
                        attacking = false;
                    }
                }
                else
                {
                    attacking = false;
                }
            }

            yield return null;
        }

        attackAnimFinish = false;
    }

    public void EndAttackAnimEvent()
    {
        attackAnimFinish = true;
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
