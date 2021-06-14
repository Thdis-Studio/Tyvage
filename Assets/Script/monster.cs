using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    public GameObject ob;
    public int nono = 3;
    public float health = 10.0f;
    public GameManager gm;
    public Transform pos;
    SpriteRenderer sprd;
    // Start is called before the first frame update
    void Awake()
    {
        nono = 1;
        rigid = GetComponent<Rigidbody2D>();
        sprd = GetComponent<SpriteRenderer>();
        Invoke("Time", 1);
        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove * 2, rigid.velocity.y);
        if(nextMove == -1)
            sprd.flipX = false;
        else if(nextMove == 1)
            sprd.flipX = true;
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 0.3f, LayerMask.GetMask("Ground"));
        if (rayhit.collider == null)
        {
            
        }
    }
    void Update()
    {
        //Searching player in left side
        Vector2 frontVec = new Vector2(rigid.position.x, rigid.position.y+0.7f);
        Debug.DrawRay(frontVec, Vector3.left, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.left, 4f, LayerMask.GetMask("Player"));
        if (rayhit.collider != null)
        {
            if (nono == 0)
            {
                nextMove = -1;
                ob.GetComponent<SpriteRenderer>().flipX = false;
                Instantiate(ob, pos.position, transform.rotation);
                nono = 1;
            }
        }

        //Searching player in right side
        Vector2 frontVecs = new Vector2(rigid.position.x, rigid.position.y+0.7f);
        Debug.DrawRay(frontVecs, Vector3.right, new Color(0, 1, 0));
        RaycastHit2D rayhits = Physics2D.Raycast(frontVecs, Vector3.right, 4f, LayerMask.GetMask("Player"));
        if (rayhits.collider != null)
        {
            if (nono == 0)
            {
                nextMove = 1;
                ob.GetComponent<SpriteRenderer>().flipX = true;
                Instantiate(ob, pos.position, transform.rotation);
                nono = 1;
            }
        }
        if (health <= 0)
            Destroy(this.gameObject, 1);
    }
    //재귀 함수
    void Think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("Think", 5);
    }
    void Time()
    {
        if (nono > 0)
            nono--;
        Invoke("Time", 1);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("F");
        if (col.gameObject.GetComponent<objdata>().iswbp)
        {
            if (player.ani)
            {
                player.ani = false;
                Dictionary<int, float> dic = new Dictionary<int, float>();
                if (col != null)
                    dic = gm.getobjd(col.gameObject);
                float d = dic[5];
                Debug.Log("Health is " + health + ", and " + d + "has been damaged");
                if (health <= 0)
                    Destroy(this.gameObject);
                health -= d;
            }
            else
                Debug.Log("LL");
        }
    }
}
