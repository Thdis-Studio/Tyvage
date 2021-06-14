using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class inputfield : MonoBehaviour
{
    List<string> sentences = new List<string>();
    public TMP_InputField ipf;
    List<string> wordss;
    int fwords;
    public GameManager gm;
    public TMP_Text tmt;
    public string question;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new List<string>(new string[] { "why it comes out same sentences", "I am typing", "I wake up", "I forgot my passport. Where is my passport?", "So. You are in a position of difficulty.", "I\'m sorry, I don't know how to help you.", "HEY! Somebody stop that guy!", "This is an easy method to solve it partially, but you need to solve it completely, or you will be in trouble.",
        "Alright. I'll help ONE last time.", "Listen to me. This is the worst situation we have ever been in. Don't make a joke out of it.", "Well done, you made our school the laughingstock of the whole country." , " 'Ere, round the back! He's flittin'!", "Watch it kid! You wanna start an accident?", "We Must do our best to survive.", " We\'ve been ambushed! ON HEAVY GUARD NOW!!!!!"
        ,"Alright, listen up, Here's the plan.", "Ha! They're pulling a trojan horse, what amateurs!"});
        ipf = GetComponent<TMP_InputField>();
        if(gm.mod == "hit")
            getsentence();
        if (gm.mod == "mgc")
            getmaths();
    }
    void Awake()
    {
        sentences = new List<string>(new string[] { "why it comes out same sentences", "I am typing", "I wake up", "I forgot my passport. Where is my passport?", "So. You are in a position of difficulty.", "I\'m sorry, I don't know how to help you.", "HEY! Somebody stop that guy!", "This is an easy method to solve it partially, but you need to solve it completely, or you will be in trouble.",
        "Alright. I'll help ONE last time.", "Listen to me. This is the worst situation we have ever been in. Don't make a joke out of it.", "Well done, you made our school the laughingstock of the whole country." , " 'Ere, round the back! He's flittin'!", "Watch it kid! You wanna start an accident?", "We Must do our best to survive.", " We\'ve been ambushed! ON HEAVY GUARD NOW!!!!!",
        "Alright, listen up, Here's the plan.", "Ha! They're pulling a trojan horse, what amateurs!"});
        ipf = GetComponent<TMP_InputField>();
        if (gm.mod == "hit")
            getsentence();
        if (gm.mod == "mgc")
            getmaths();
    }
    string ns;
    void getsentence()
    {
        Debug.Log("getsentence() has been called");
        int s = gm.str(sentences);
        Debug.Log(s);
        wordss = new List<string>(sentences[s].Split(' '));
        fwords = wordss.Count;
        tmt.text = sentences[s];
        ns = sentences[s];
    }
    public string answer;
    void getmaths()
    {
        Debug.Log("getmaths() has been called");
        System.Random rnd = new System.Random();
        int pmmd = rnd.Next(1, 5);
        if (pmmd == 1)
        {
            int p1 = rnd.Next(1, 100);
            int p2 = rnd.Next(1, 100);
            int an = p1 + p2;
            answer = an.ToString();
            tmt.text = p1.ToString() + "+" + p2.ToString();
            question = p1.ToString() + "+" + p2.ToString();
            Debug.Log(question);
        }
        else
        {
            int p1 = rnd.Next(1, 100);
            int p2 = rnd.Next(1, p1);
            int an = p1 - p2;
            answer = an.ToString();
            tmt.text = p1.ToString() + "-" + p2.ToString();
            question = p1.ToString() + "-" + p2.ToString();
            Debug.Log(question);
        }
        
    }
    public int words =0;
    // Update is called once per frame
    public string nss;
    public string mod;
    public camsh cs;
    void Update()
    {
        if(mod == "hit")
        {
            tmt.text = ns;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(wordss[words]);
                nss = ns;
                if (string.Equals(ipf.text, wordss[words] + " "))
                {
                    var regex = new Regex(Regex.Escape(wordss[words]));
                    tmt.text = regex.Replace(ns, "", 1);
                    ns = tmt.text;
                    hit();
                    ipf.text = "";
                    Debug.Log(ipf.text);
                    words++;
                    StartCoroutine(cs.Shake(.1f, .2f));
                }
                else
                {
                    ipf.text = "";
                    Debug.Log("Error");
                    anim.SetBool("ishw", false);
                    StartCoroutine(ExampleCoroutine());
                }
                if (fwords == words)
                {
                    getsentence();
                    words = 0;
                    hit();
                    Debug.Log("F");
                    StartCoroutine(cs.Shake(.1f, .2f));
                }
            }
        }
        if(mod == "mgc")
        {
            tmt.text = question;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (string.Equals(ipf.text, answer + "\n"))
                {
                    ipf.text = "";
                    Debug.Log("correct");
                    getmaths();
                }
                else
                {
                    ipf.text = "";
                    Debug.Log("Error");
                    StartCoroutine(ExampleCoroutine());
                }
            }
            
        }
        string text = tmt.text;
        if (text.Length <= 0)
        {
            if (mod == "hit")
                getsentence();
            else if (mod == "mgc")
                getmaths();
        }
    }
    void FixedUpdate()
    {
        mod = gm.mod;
    }
    IEnumerator ExampleCoroutine()
    {
        tmt.color = new Color(255, 0, 0, 1);
        tmt.text = "worng";
        ns = "worng";
        ipf.readOnly = true;
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        ipf.readOnly = false;
        if (gm.mod == "hit")
        {
            ns = nss;
            tmt.color = new Color(255, 255, 255, 1);
            tmt.text = nss;
        }else if(gm.mod == "mgc")
        {
            tmt.color = new Color(255, 255, 255, 1);
            tmt.text = question;
        }
    }
    public void hit()
    {
        anim.SetBool("ishw", false);
        anim.SetBool("ishw", true);
    }
}
