using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public Transform a, b;
    private bool goRight;
    [Header("Movement Velocity")]
    public float speedMove = 6f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        followPoints();
    }

    public void followPoints()
    {
        if(goRight)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            if(Vector2.Distance(transform.position, b.position) < 0.1f)
            {
                goRight = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, b.position, speedMove * Time.deltaTime);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            if (Vector2.Distance(transform.position, a.position) < 0.1f)
            {
                goRight = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, a.position, speedMove * Time.deltaTime);
        }
    }

    public void Death()
    {
        if (transform.parent != null)
        {
            Transform[] siblingObjects = transform.parent.GetComponentsInChildren<Transform>();

            if (siblingObjects.Length > 2)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            speedMove = 0f;
            anim.Play("Explosion");
            SFXController.instance.SFX("DeathEnemy", 1f);
        }
    }
}
