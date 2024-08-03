using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject HitVFX;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
       
    }

    public void OnInit()
    {
        rb.velocity = transform.right * 5f;
        Invoke(nameof(OnDesPam), 4f);
    }

    public void OnDesPam()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Character>().OnHit(30f);
            Instantiate(HitVFX, transform.position, transform.rotation);
            OnDesPam();
        }
    }
}
