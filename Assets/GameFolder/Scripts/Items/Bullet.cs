using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    public float xVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(xVelocity, rb2d.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6)
        {
            xVelocity = 0f;
            anim.Play("Explosion");
            SFXController.instance.SFX("DeathEnemy", 0.7f);
        }
    }

    public void BulletDelete()
    {
        Destroy(this.gameObject);
    }
}