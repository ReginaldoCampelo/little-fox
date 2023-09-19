using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D box2D;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        box2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            box2D.enabled = false;
            anim.Play("Explosion");
            GameController.instance.UpdateUIGems();
        }
    }

    public void GemDestroy()
    {
        Destroy(this.gameObject);
    }
}
