using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timertext;
   
   public float score=60;
    //  public CharController livesleft;

    [SerializeField] float startTime;
    public CharController cc;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        


    }
    void Update()
    {
        //timer
       if( cc.win==false){
        float t = Time.time - startTime;
        string minutes=((int)t/60).ToString();
        string sec = (t % 60).ToString("f0");
        timertext.text = "Score : " + minutes+ ":"+sec;
       }
    }
}
