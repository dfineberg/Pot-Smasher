using UnityEngine;
using System.Collections;

public class ShellWeapon : Weapon {

    public GameObject shellPrefab;
    public float throwDelay;
    bool attacking = false;

    public override bool IsAttacking()
    {
        return attacking;
    }

    void Fire(Direction attackDir)
    {
        GameObject newShell = (GameObject)Instantiate(shellPrefab, transform.position, Quaternion.identity);
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
