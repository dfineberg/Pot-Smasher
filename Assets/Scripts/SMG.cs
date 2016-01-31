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
        yield return null;
        while (IsAttacking())
        {
            GameObject newBullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
            newBullet.GetComponent<Bullet>().SetDamage(damage);
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
