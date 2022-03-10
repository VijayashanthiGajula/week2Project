using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public int PlayerLives;
    public GameObject GO;
    [SerializeField] Transform player;
    [SerializeField] Transform respawnPoint;


  private void OnTriggerEnter(Collider other)
    {
         if(other.tag=="Lava"){
             Debug.Log(other.tag);
             Destroy(GO);
             
        }
    }

}