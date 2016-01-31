using UnityEngine;
using System.Collections;

public class ShellWeapon : Weapon {

    public GameObject shellPrefab;
    public float throwDelay;
    public float spawnOffset;
    bool attacking = false;

    public override bool IsAttacking()
    {
        return attacking;
    }

    void Fire(Direction attackDir)
    {
        Vector3 pos = transform.position;

        switch (attackDir)
        {
            case Direction.up:
                pos += Vector3.up * spawnOffset;
                break;

            case Direction.down:
                pos += Vector3.down * spawnOffset;
                break;

            case Direction.left:
                pos += Vector3.left * spawnOffset;
                break;

            case Direction.right:
                pos += Vector3.right * spawnOffset;
                break;
        }

        GameObject newShell = (GameObject)Instantiate(shellPrefab, pos, Quaternion.identity);
        TurtleShell shell = newShell.GetComponent<TurtleShell>();
        shell.SetDirection(attackDir);
        shell.SetDamage(damage);
    }

    public override void Attack(Direction attackDir)
    {
        StartCoroutine(AttackRoutine(attackDir));
    }

    IEnumerator AttackRoutine(Direction dir)
    {
        attacking = true;
        yield return new WaitForSeconds(throwDelay);
        Fire(dir);
        attacking = false;
    }
}
