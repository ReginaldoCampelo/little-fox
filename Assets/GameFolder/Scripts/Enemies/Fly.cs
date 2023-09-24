using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Animator anim;
    public Transform player;
    public float speed;
    private float angle = 0;
    public float distanceToPlayer;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, player.position) < distanceToPlayer)
        {
            Follow();
            Rotate();
        } 
    }

    void Follow()
    {
        float t = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, player.position.y + 0.8f), t);

        if(player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
    }

    void Rotate()
    {
        Vector3 dir = player.position - transform.position;

        if(transform.localScale.x == -1f)
        {
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180f;
        }
        else
        {
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
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
                Destroy(transform.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            speed = 0f;
            anim.Play("Explosion");
            SFXController.instance.SFX("DeathEnemy", 1f);
        }
    }
}
