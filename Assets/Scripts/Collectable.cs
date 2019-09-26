using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    
    // This function doesn't care what the collider that triggered it is. 
    // So not including it as a parameter makes it a little faster since unity doesn't have to compute a collision.
    private void OnTriggerEnter()
    {
        Score.CurrentLevelScore++;
        Destroy(gameObject);
    }
}