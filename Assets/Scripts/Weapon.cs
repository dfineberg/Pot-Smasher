﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour {

    public int damage;

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    int downHash;
    int leftHash;
    int rightHash;
    int upHash;
    int idleHash;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        downHash = Animator.StringToHash("Down");
        leftHash = Animator.StringToHash("Left");
        rightHash = Animator.StringToHash("Right");
        upHash = Animator.StringToHash("Up");
        idleHash = Animator.StringToHash("Idle");

        SortBehindPlayerEvent();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        PS.Pot pot = col.GetComponent<PS.Pot>();

        if(pot != null)
        {
            pot.Damage(damage);
        }
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

		PS.SoundController.Instance.PlaySound(PS.SOUND_ID.SWORD_1);
    }

    public virtual void StopAttacking()
    {
        if (IsAttacking() && animator) animator.SetTrigger(idleHash);
    }

    public void SortInFrontOfPlayerEvent()
    {
        if(spriteRenderer)
        {
            spriteRenderer.sortingOrder = 1;
        }
    }

    public void SortBehindPlayerEvent()
    {
        if (spriteRenderer)
        {
            spriteRenderer.sortingOrder = -1;
        }
    }
}
