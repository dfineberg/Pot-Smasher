using UnityEngine;
using System.Collections;

public class MaceWeapon : Weapon {

    public float throwForce;
    public GameObject macePrefab;
    Rigidbody2D maceRigidbody;
    DistanceJoint2D playerJoint;
    GameObject maceObject;

    void Start()
    {
        maceObject = (GameObject)Instantiate(macePrefab, transform.position, Quaternion.identity);
        Mace mace = maceObject.GetComponentInChildren<Mace>();
        maceRigidbody = mace.GetComponent<Rigidbody2D>();
        playerJoint = mace.playerJoint;
        StartCoroutine(PlayerWait(mace));
    }

    void OnDestroy()
    {
        Destroy(maceObject);
    }

    IEnumerator PlayerWait(Mace mace)
    {
        yield return null;
        playerJoint.connectedBody = PlayerController.instance.GetComponent<Rigidbody2D>();
        mace.chainTransforms[3] = PlayerController.instance.transform;
    }

    public override void Attack(Direction attackDir)
    {
        switch (attackDir)
        {
            case Direction.up:
                maceRigidbody.AddForce(Vector2.up * throwForce, ForceMode2D.Impulse);
                break;

            case Direction.down:
                maceRigidbody.AddForce(Vector2.down * throwForce, ForceMode2D.Impulse);
                break;

            case Direction.left:
                maceRigidbody.AddForce(Vector2.left * throwForce, ForceMode2D.Impulse);
                break;

            case Direction.right:
                maceRigidbody.AddForce(Vector2.right * throwForce, ForceMode2D.Impulse);
                break;
        }
    }

    public override bool IsAttacking()
    {
        return false;
    }

    public override bool WaitWhileAttacking()
    {
        return false;
    }
}
