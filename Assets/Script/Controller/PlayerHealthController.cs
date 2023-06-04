using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int health;
    [SerializeField]
    int healthOri;
    Animator anim;
    Rigidbody2D rb;
    public float deadtime;
    public GameObject healthball1, healthball2, healthball3, healthball4, healthball5;
    public GameObject deathbar;
    public AudioSource hurtSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        healthOri = health;
    }
    private void Update()
    {
        if (health <= 0)
        {
            Dead();
        }
        if(health > healthOri)
        {
            health = healthOri;
        }
        healthballReset();
    }
    public void Dead()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(death());
    }
    IEnumerator death()
    {
        anim.SetTrigger("death");
        yield return new WaitForSeconds(deadtime);
        deathbar.SetActive(true);

    }
    public void Hurt(int damage)
    {
        health -= damage;
        hurtSound.Play();
    }

    void healthballReset()
    {
        if (health < healthOri * 0.8f)
            healthball5.SetActive(false);
        if (health < healthOri * 0.6f)
            healthball4.SetActive(false);
        if (health < healthOri * 0.4f)
            healthball3.SetActive(false);
        if (health < healthOri * 0.2f)
            healthball2.SetActive(false);
        if (health <= 0)
            healthball1.SetActive(false);
    }
}
