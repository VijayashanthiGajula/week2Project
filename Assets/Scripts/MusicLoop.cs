using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoop : MonoBehaviour
{
 public static MusicLoop instance;
private void Awake()
{

    if (instance == null)
        instance = this;
    else if (instance != this)
        Destroy(gameObject);
    DontDestroyOnLoad(gameObject);
}  
   
}
