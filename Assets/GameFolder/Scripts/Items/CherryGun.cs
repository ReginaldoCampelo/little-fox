using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryGun : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D box2D;

    void Start()
    {
        anim = GetComponent<Animator>();
        box2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            box2D.enabled = false;
            anim.Play("Explosion");
            SFXController.instance.SFX("Gem", 1f);
            GameController.instance.totalBullets += 1;
        }
    }

    public void CherryGunDelete()
    {
        Destroy(this.gameObject);
    }
}
