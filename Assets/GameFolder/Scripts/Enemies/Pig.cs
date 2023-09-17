using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public Transform a, b;
    private bool goRight;
    [Header("Movement Velocity")]
    public float speedMove = 6f;


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
        Destroy(this.gameObject);
    }

}
