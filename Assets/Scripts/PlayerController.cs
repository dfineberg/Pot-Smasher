using UnityEngine;
using System.Collections;

public enum Direction { up, down, left, right }

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    Direction currentDirection = Direction.down;

    public Weapon weapon;
    public bool disableUserInput = false;
    bool attacking = false, attackAnimFinish = false;

    public float walkSpeed, walkCutOff = 0.5f;
    new Rigidbody2D rigidbody;

    public int gemsToLevelUp;
    int currentLevel = 0, currentXP;
    public GameObject[] weapons;

    Animator animator;
    int downHash, leftHash, rightHash, upHash, attackHash, walkingHash, levelUpHash;

    public delegate void PlayerGemEvent(int xp, int target);
    public static event PlayerGemEvent e_gemGet;

    void Start()
    {
        instance = this;

        downHash = Animator.StringToHash("Down");
        leftHash = Animator.StringToHash("Left");
        rightHash = Animator.StringToHash("Right");
        upHash = Animator.StringToHash("Up");
        attackHash = Animator.StringToHash("Attack");
        walkingHash = Animator.StringToHash("Walking");
        levelUpHash = Animator.StringToHash("LevelUp");

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !attacking && !LevelUpPlaying())
        {
            attackAnimFinish = false;
            StartCoroutine(AttackRoutine());
        }
    }

    void FixedUpdate()
    {
        if (!disableUserInput)
        {
            Vector2 thisVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            if (thisVelocity.sqrMagnitude >= (walkCutOff * walkCutOff) && !attacking && !LevelUpPlaying())
            {
                Move(thisVelocity);
            }
            else
            {
                animator.SetBool(walkingHash, false);
                rigidbody.velocity = Vector2.zero;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Gem")) CollectGem(col.gameObject);
    }

    void CollectGem(GameObject gem)
    {
        Destroy(gem);
        currentXP++;
        if (e_gemGet != null) e_gemGet(currentXP, gemsToLevelUp);
    }

    public void LevelUp()
    {
        animator.SetTrigger(levelUpHash);
        weapon.StopAttacking();
        attackAnimFinish = false;
        currentDirection = Direction.down;
        currentXP = 0;
        currentLevel++;
        attacking = false;

        if (currentLevel < weapons.Length)
        {
            GameObject newWeapon = (GameObject)Instantiate(weapons[currentLevel - 1]);
            newWeapon.transform.SetParent(transform);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localScale = Vector3.one;

            if(weapon != null)
            {
                Destroy(weapon.gameObject);
            }

            weapon = newWeapon.GetComponent<Weapon>();
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

    public bool LevelUpPlaying()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == levelUpHash) return true;
        return false;
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
