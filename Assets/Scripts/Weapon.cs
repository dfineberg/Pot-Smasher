using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour {

    public int damage;

    Animator animator;
    int downHash;
    int leftHash;
    int rightHash;
    int upHash;
    int idleHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        downHash = Animator.StringToHash("Down");
        leftHash = Animator.StringToHash("Left");
        rightHash = Animator.StringToHash("Right");
        upHash = Animator.StringToHash("Up");
        idleHash = Animator.StringToHash("Idle");
    }

    public virtual bool IsAttacking()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).shortNameHash != idleHash)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool WaitWhileAttacking()
    {
        return true;
    }

	public virtual void Attack(Direction attackDir)
    {
        switch (attackDir)
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
