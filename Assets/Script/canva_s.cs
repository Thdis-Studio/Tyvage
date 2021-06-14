using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class canva_s : MonoBehaviour
{
    double fps = 0.0;
    double updateRate = 4.0;  // 4 updates per sec.
    public TMP_Text tmt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fps = Time.frameCount / Time.time;
        string str = System.Convert.ToInt32(fps).ToString();
        tmt.text = "Fps: "+str;
    }
}
