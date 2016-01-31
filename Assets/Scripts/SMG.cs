using UnityEngine;
using System.Collections;

public class SMG : Weapon {

    public float shootInterval;
    public GameObject bulletPrefab;

    public override void Attack(Direction attackDir)
    {
        base.Attack(attackDir);
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (IsAttacking())
        {
            GameObject newBullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
            //SET DIRECTION IN BULLET CLASS
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
