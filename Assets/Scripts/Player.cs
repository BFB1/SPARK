using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This line ensures that the gameObject this script is attached to has a RigidBody component too.
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    
    // I decided to make the Player class have very little control in this game because i wanted to have
    // multiple players. The GameManager takes care of creating players and switching between them.
    // When a player is inactive this script gets disabled so that it does not move.
    
    private Rigidbody rb;
    private bool isOntop;
    public float forceMultiplier;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    
    private void FixedUpdate()
    {
        float adjustedForce;
        if (isOntop) { adjustedForce = forceMultiplier; } else { adjustedForce = forceMultiplier / 2.5f;}
        
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            rb.AddForce(new Vector3(0, 0, 1) * adjustedForce);
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
        {
            rb.AddForce(new Vector3(0, 0, -1) * adjustedForce);
        }
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
        {
            rb.AddForce(new Vector3(-1, 0) * adjustedForce);
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
            rb.AddForce(new Vector3(1, 0) * adjustedForce);
        }
        
        // The proper way to jump...
        if (Input.GetKey(KeyCode.Space) && isOntop)
        {
            rb.AddForce(new Vector3(0, 3), ForceMode.Impulse);
        }
    }


    private void OnCollisionStay(Collision other)
    {
        // There is an obvious error here because although it accounts for the height of the other cube it does
        // not account for its own height in this calculation.
        // I could fix it, but i prefer to call it a feature here because it makes the final level much more fun
        // Instead of making a tower all the way up to the objective which would take ages... it turns into a game of
        // making a tower long enough that you can launch yourself there. If you want to go for the optional objectives
        // it makes it into an interesting challenge to not use to many cubes but still be able to launch yourself at
        // all three. Its a feature! not a bug!
        if (transform.position.y >
            other.transform.position.y + other.gameObject.GetComponent<MeshRenderer>().bounds.extents.y)
        {
            isOntop = true;
        }
    }

    private void OnCollisionExit()
    {
        isOntop = false;
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    public void ChangeColor(Color newColor)
    {
        GetComponent<Renderer>().material.color = newColor;
    }
}
