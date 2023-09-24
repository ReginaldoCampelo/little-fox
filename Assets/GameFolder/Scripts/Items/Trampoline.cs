using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private Animator animator;
    public float _yForce = 30f;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            animator.SetTrigger("Jump");
            SFXController.instance.SFX("Trampoline", 1f);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
                collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, 
                _yForce
                );
        }
    }
}
