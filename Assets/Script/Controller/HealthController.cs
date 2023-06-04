using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int health;
    Animator anim;
    Rigidbody2D rb;
    public float deadtime;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (health <= 0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        GetComponent<Movement_Enemy01>().isDead = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(death());
    }
    IEnumerator death()
    {
        anim.SetTrigger("death");
        yield return new WaitForSeconds(deadtime);
        Destroy(gameObject);

    }
    public void Hurt(int damage)
    {
        health -= damage;
    }
}
