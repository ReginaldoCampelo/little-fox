using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            anim.gameObject.GetComponent<Pig>().speedMove = 0f;
            SFXController.instance.SFX("DeathEnemy", 1f);
            anim.Play("Explosion");

            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
                other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 15f);
            anim.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            anim.gameObject.GetComponent<Pig>().speedMove = 0f;
            anim.Play("Explosion");
            SFXController.instance.SFX("DeathEnemy", 1f);
        }
    }
}
