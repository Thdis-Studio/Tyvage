using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public GameObject scanobj;
    public string mod = "hit";
    public bool isfight;
    public GameObject filgc;
    public void fights()
    {
        Debug.Log("Ok1");
        isfight = true;
        Debug.Log("Ok3");
        filgc.SetActive(true);
        Debug.Log("Ok4");
        
    }
    public void fightsof()
    {
        Debug.Log("Ok1");
        isfight = false;
        Debug.Log("Ok3");
        filgc.SetActive(false);
        Debug.Log("Ok4");
    }
    public int str(List<string> s)
    {
        System.Random rnd = new System.Random();
        int m = rnd.Next(s.Count);
        return m;
    }
    public Dictionary<int, float> getobjd(GameObject obj)
    {
        objdata objd = obj.GetComponent<objdata>();
        Dictionary<int, float> dic = new Dictionary<int, float>();
        if(objd.isnpc)
            dic.Add(1, 1f);
        else
            dic.Add(1, 0f);
        if (objd.ismonster)
            dic.Add(2, 1f);
        else
            dic.Add(2, 0f);
        if (objd.iswbp)
            dic.Add(3, 1f);
        else
            dic.Add(3, 0f);
        dic.Add(4, objd.id);
        dic.Add(5, objd.damage);
        return dic;
    }
}
