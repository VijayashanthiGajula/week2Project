using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
     public GameObject Player;
    public GameObject playerInstance;
    public int lives=4;
    float respawnTimer;
     [SerializeField] Transform respawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        //Spawner();
    }
 
}
