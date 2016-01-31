using UnityEngine;
using System.Collections;

public class Boomerang : Weapon {

    public float throwDistance;
    public float flySpeed;
    int attackHash;

    protected override void Start()
    {
        base.Start();
        attackHash = Animator.StringToHash("Attacking");
    }

    public override bool WaitWhileAttacking()
    {
        return false;
    }

    public override void Attack(Direction attackDir)
    {
        if (!IsAttacking())
        {
            StartCoroutine(AttackRoutine(attackDir));
			PS.SoundController.Instance.PlaySound(PS.SOUND_ID.BOOMERANG_1);
        }
    }

    IEnumerator AttackRoutine(Direction direction)
    {
        animator.SetBool(attackHash, true);
        Transform parent = transform.parent;
        transform.SetParent(null);
        Vector3 targetPos = transform.position;

        switch (direction)
        {
            case Direction.up:
                targetPos += Vector3.up * throwDistance;
                break;

            case Direction.right:
                targetPos += Vector3.right * throwDistance;
                break;

            case Direction.down:
                targetPos += Vector3.down * throwDistance;
                break;

            case Direction.left:
                targetPos += Vector3.left * throwDistance;
                break;
        }

        while((targetPos - transform.position).sqrMagnitude > 0.07f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, flySpeed * Time.deltaTime);
            yield return null;
        }

        while((transform.position - parent.position).sqrMagnitude > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, parent.position, flySpeed * 2f * Time.deltaTime);
            yield return null;
        }

        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        animator.SetBool(attackHash, false);
    }

    public override void StopAttacking()
    {
        //do nothing
    }
}
