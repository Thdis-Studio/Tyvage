using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basa : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprdr;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprdr = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (sprdr.flipX) 
        {
            transform.Translate(transform.right * 0.05f);
        }
        else 
        {
            transform.Translate(-transform.right * 0.05f);
        }
        Debug.DrawRay(rigid.position, Vector2.left * 0.3f, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector2.left, 0.3f, LayerMask.GetMask("Player"));
        if (rayhit.collider != null)
        {
            Destroy(gameObject);
        }
    }
}
