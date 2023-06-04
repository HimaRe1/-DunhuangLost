using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Player : MonoBehaviour
{
    [Header("组件")]
    Rigidbody2D rb;
    Animator anim;


    [Header("MOVE")]
    public float speed;
    float speednow;
    public float jumpforce = 5f;
    public float fallMultiplier = 2.5f;
    public float lowjumpMultiplier = 2f;
    private bool isGround;
    public Transform groundcheckleft;
    public Transform groundcheckright;
    public LayerMask ground;
    private int jumpCount;
    private bool jumpPressed;
    public PhysicsMaterial2D P_fH;
    public PhysicsMaterial2D P_fL;

    [Header("ATTACK")]
    public int damage;
    int damagenow;
    float CDAttack;
    public float attackRadius;
    public bool ishurt;
    int attackCount;
    float CDHit;
    public float attackDelay;
    public AudioSource attackSound;
    public AudioSource damageSound;
    public AudioSource dashSound;

    [Header("DASH")]
    public float dashSpeed;
    public float dashTime;
    float startDashTimer;
    public bool isDash;
    public bool isUltra;
    float dashTimer;
    public float dashCD;
    float dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        damagenow = damage;
        speednow = speed;
    }
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float facedir = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x,y);
        isGround = Physics2D.OverlapCircle(groundcheckleft.position, 0.1f, ground)|| Physics2D.OverlapCircle(groundcheckright.position, 0.1f, ground);//触地检测
        CDAttack -= Time.deltaTime;
        CDHit -= Time.deltaTime;
        dashTimer -= Time.deltaTime;
        run(dir,facedir);
        jump();
        Attack(damage);
        Dash();
    }

    void Dash()
    {
        if (!isDash)
        {
            isUltra = false;
            if (Input.GetKeyDown(KeyCode.K)&&dashTimer <0)
            {
                anim.SetTrigger("dash");
                isDash = true;
                startDashTimer = dashTime;
                dashTimer = dashCD;
                dashSound.Play();
            }
        }
        else
        {
            isUltra = true;
            startDashTimer -= Time.deltaTime;
            if (startDashTimer <= 0)
                isDash = false;
            else
                dir = transform.localScale.x;
                rb.velocity = new Vector2(dir * dashSpeed, 0);
        }
        
    }
    void run(Vector2 dir,float facedir)
    {
        if (ishurt)
        {
            anim.SetTrigger("hurt");
            speednow = speed * 0.6f;
            //设置受伤动画
            CDAttack = 0.3f;
            ishurt = false;
        }
        rb.velocity = new Vector2(dir.x * speednow, rb.velocity.y);
        if(facedir!= 0)
        {

            transform.localScale = new Vector3(facedir, 1, 1);
        }
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));
    }
    void jump()
    {
        anim.SetFloat("fall", rb.velocity.y);
        anim.SetBool("ground", isGround);
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }//判断是否跳跃
        if (isGround)
        {
            rb.sharedMaterial = P_fH;
            jumpCount = 2;
            if (jumpPressed)
            {
                jumpCount--;
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetTrigger("jump");
                jumpPressed = false;
            }
        }
        else
        {
            rb.sharedMaterial = P_fL;
            if (jumpPressed && jumpCount > 0)
            {
                jumpCount--;
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("ground", false);
                anim.SetTrigger("jump");
                jumpPressed = false;
            }
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowjumpMultiplier - 1) * Time.deltaTime;
        }
    }
    void Attack(int damage)
    {    
        
        if(CDAttack <0)
        {
            speednow = speed;
        }
        if(Input.GetKeyDown(KeyCode.J) && CDAttack <= 0)
        {
            attackSound.Play();
            speednow = speed * 0.3f;
            damagenow = damage;
            
            StartCoroutine(attacking());
            
            CDAttack = 0.5f;
            attackCount++;
            if(attackCount > 4)
            {
                attackCount = 0;
            }
            if(CDHit <= 0)
            {
                attackCount = 0;
                CDHit = 4f;
            }
        }
        
    }
    IEnumerator attacking()
    {
        SwitchAttack();
        yield return new WaitForSeconds(attackDelay);
        var colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Enemy")
            {
                colliders[i].GetComponent<Movement_Enemy01>().ishurt = true;
                colliders[i].GetComponent<HealthController>().Hurt(damagenow);
                damageSound.Play();
            }

        }
    }
    void SwitchAttack()
    {
        switch (attackCount)
        {
            case 0:
                anim.SetTrigger("a1");
                break;
            case 1:
                anim.SetTrigger("a2");
                break;
            case 2:
                damagenow = (int)(damage * 1.2f);
                anim.SetTrigger("a3");
                break;
            case 3:
                damagenow = (int)(damage * 1.3f);
                anim.SetTrigger("a4");
                break;
            case 4:
                damagenow = (int)(damage * 1.5f);
                anim.SetTrigger("a5");
                break;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundcheckleft.position, 0.1f);
        Gizmos.DrawSphere(groundcheckright.position, 0.1f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        //在相机上以groundcheck为圆心 checkRadius范围画圆显示在相机上
    }
}
