using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText combatTextPrefab;

    private float hp;
    public bool isDead => hp <= 0;
    private string currentAnimName;
    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100, transform);
    }

    public virtual void OnDespam()
    {

    }

    protected virtual void OnDeath()
    {
        ChangeAnim("death");
        Invoke(nameof(OnDespam), 2f);
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }

    }

    public void OnHit(float damage)
    {
        if(!isDead)
        {
            hp -= damage;
        }
        if(isDead)
        {
            hp = 0;
            OnDeath();
        }
        healthBar.SetNewHp(hp);
        Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
    }

   
}
