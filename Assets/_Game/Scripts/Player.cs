using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]private Rigidbody2D rb;
    
    [SerializeField] private float jumpForce = 500;
    [SerializeField] private LayerMask groupLayer;
    [SerializeField]private float speed = 300;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;


    private bool isGrounded = true;
    private bool isJumping = false;
    
    private bool isAttack;
    private float horizontal;
    
    private Vector3 savePoint;

    private int coin = 0;

    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
    }
    void Update()
    {
        if (isDead)
        {
            return;
        }

        isGrounded = CheckGrounded();
        // -1 -> 0 -> 1
       /* horizontal = Input.GetAxisRaw("Horizontal");*/
        /*horizontal = Input.GetAxisRaw("Vertical");*/

        if (isAttack)
        {
            return;
        }

        if (isGrounded)
        {

            if (isJumping)
            {
                return;
            }

            //jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            //change anim run
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }
            //attack
            if (Input.GetKeyDown(KeyCode.R) && isGrounded)
            {
                Attack();
            }

            //throw
            if (Input.GetKeyDown(KeyCode.T) && isGrounded)
            {
                Throw();
            }

        }

        // check falling
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }

        //moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {

            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }

        else if (isGrounded)
        {
            ChangeAnim("ilde");
            rb.velocity = Vector2.zero;
        }


    }
    public override void OnInit()
    {
        base.OnInit();
        
        isAttack = false;
        transform.position = savePoint;
        ChangeAnim("ilde");
        DeActiveAttack();
        SavePoint();
        UIManager.instance.setCoin(coin);
    }

    public override void OnDespam()
    {
        base.OnDespam();
        OnInit();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
       
    }

    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groupLayer);
        /*if (hit.collider != null) return true;
        else return false;*/
        return hit.collider != null;
    }

    public void Attack()
    {
        rb.velocity = Vector2.zero;
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack),0.5f);
    }

    private void ResetAttack()
    {
        isAttack = false;
        ChangeAnim("ilde");
    }

    public void Throw()
    {
        rb.velocity = Vector2.zero;
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);

        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }

    public void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

    public void setMoving(float horizontal)
    {
        this.horizontal = horizontal; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            Debug.LogError("Collision is null.");
            return;
        }

        if (collision.CompareTag("coin"))
        {
            coin++;
            PlayerPrefs.SetInt("coin", coin);

            if (UIManager.instance != null)
            {
                UIManager.instance.setCoin(coin);
            }
            else
            {
                Debug.LogError("UIManager.instance is null.");
            }

            if (collision.gameObject != null)
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.tag == "DeadZone")
        {
            
            ChangeAnim("death");
            Invoke(nameof(OnInit), 1f);
        }
    }



}
