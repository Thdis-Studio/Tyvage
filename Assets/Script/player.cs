using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public LayerMask groundLayer;
    public float maxSpeed;
    public float maxSpeed_down;
    Rigidbody2D rigid;
    public GameManager gamem;
    public static bool capon = false;
    public GameObject sl;
    public float hp = 100f;
    public UnityEngine.UI.Image img;
    public UnityEngine.UI.Text uitxt;
    public GameObject gmb;
    SpriteRenderer sptrdgmb;
    Vector2 dirv;
    GameObject scob;
    SpriteRenderer sprdr;
    Animator anim;
    public static bool ani;
    List<string> sentences = new List<string>();
    void Animationf()
    {
        ani = false;
        Debug.Log(ani);
        anim.SetBool("ishw", false);
    }
    void Animationt()
    {
        if (ani)
        {
            ani = false;
            anim.SetBool("ishw", false);
        }
        ani = true;
        Debug.Log(ani);
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Health", 3);
        sptrdgmb = gmb.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        sprdr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Awake()
    {
        Invoke("Health", 3);
        sptrdgmb = gmb.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        sprdr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h == 1)
        {
            dirv = Vector2.right;
        }
        else if (h == -1)
        {
            dirv = Vector2.left;
        }

        if (!gamem.isfight)
        {
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
            anim.SetBool("iswalk", true);
        }
        if(rigid.velocity.x  > maxSpeed && (!gamem.isfight) && (!gamem.isfight)) //오른쪽
        {
            dirv = Vector2.right;
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        if (rigid.velocity.x < maxSpeed*(-1) && (!gamem.isfight) && (!gamem.isfight)) //왼쪽
        {
            dirv = Vector2.left;
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Debug.Log("LeftAlt has been Pressed");
            if (capon)
            {
                capon = false;
                gamem.fightsof();
                Debug.Log("Fight Mode Off");
                ani = false;
                anim.SetBool("ishw", false);
            }
            else
            {
                capon = true;
                gamem.fights();
                Debug.Log("Fight Mode On");
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
            if (gamem.mod == "hit")
            {
                gamem.mod = "mgc";
                Debug.Log("Magic Cricle Mode");
            }
            else if (gamem.mod == "mgc")
            {
                gamem.mod = "hit";
                Debug.Log("Hit Mode");
            }
        }
        float hs = Input.GetAxisRaw("Vertical");
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.5f;
        Debug.DrawRay(position, direction * distance, new Color(0, 1, 0));
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (Input.GetKeyDown(KeyCode.W) && (!gamem.isfight) || Input.GetKeyDown(KeyCode.UpArrow) && (!gamem.isfight) || Input.GetKeyDown(KeyCode.Space) && (!gamem.isfight))
        {
            if (hit.collider != null)
            {
                rigid.AddForce(Vector2.up * maxSpeed, ForceMode2D.Impulse);
                if (rigid.velocity.y > maxSpeed) //점프
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, maxSpeed);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.G) && (!gamem.isfight))
        {
            Instantiate(sl, transform.position, transform.rotation);
        }
        if (Input.GetButtonDown("Horizontal") && (!gamem.isfight))
            sprdr.flipX = Input.GetAxisRaw("Horizontal") == -1;
        anim.SetBool("mirror",Input.GetAxisRaw("Horizontal") == -1);
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("iswalk", false);
        }
        else
        {
            anim.SetBool("iswalk", true);
        }
    }
    void FixedUpdate()
    {
        Debug.DrawRay(rigid.position, dirv * 0.9f, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, dirv, 0.3f, LayerMask.GetMask("Object"));
        if (rayhit.collider != null)
        {
            scob = rayhit.collider.gameObject;
            
        }
        else
            scob = null;
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("iswalk", false);
        }
        else
        {
            anim.SetBool("iswalk", true);
        }
        if (Input.GetButtonDown("Horizontal") && (!gamem.isfight))
            sprdr.flipX = Input.GetAxisRaw("Horizontal") == -1;
        anim.SetBool("mirror",Input.GetAxisRaw("Horizontal") == -1);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("th"))
        {
            Debug.Log("fasds");
            objdata objd = col.gameObject.GetComponent<objdata>();
            hp -= objd.damage;
            img.fillAmount = (hp / 100);
            uitxt.text = hp + "%";
            if(hp <= 0)
            {
                Debug.Log("닝겐 이것은 이미 죽었다");
            }
        }
    }
    void Health()
    {
        if (hp < 100.0f)
        {
            hp += 0.5f;
            if (hp > 100.0f)
                hp -= (100.0f - hp);
            img.fillAmount = (hp / 100);
            uitxt.text = hp + "%";
        }
        Invoke("Health", 3);
    }
}
