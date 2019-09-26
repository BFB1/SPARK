using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Transform playerPrefab;
    private List<Player> players;

    public Player startingPlayer;
    private Player currentPlayer;
    
    private Stack<Vector3> lastStablePositions;

    public CameraMovement trackCamera;
    

    private void Awake()
    {
        players = new List<Player>();
        lastStablePositions = new Stack<Vector3>();
        lastStablePositions.Push(startingPlayer.transform.position);
        
        currentPlayer = startingPlayer;
        players.Add(startingPlayer);
    }
    
    // All player switching, adding and removing code happens in the GameManagers update function.
    private void Update()
    {
        if (currentPlayer.GetVelocity() == Vector3.zero)
        {
            Vector3 currentPosition = currentPlayer.transform.position;
            if (!lastStablePositions.Contains(currentPosition))
            {
                lastStablePositions.Push(currentPosition);
            }
        }

        if (currentPlayer.GetVelocity().y < -50)
        {
            if (players.Count == 1)
            {
                currentPlayer.transform.position = StablePosition();
            }
            else
            {
                players.Remove(currentPlayer);
                Destroy(currentPlayer);
                currentPlayer = players[0];
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            NewPlayer();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            SwitchPlayers(true);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            SwitchPlayers(false);
        }
    }

    private void NewPlayer()
    {
        Vector3 pos = StablePosition();
        currentPlayer.enabled = false;
        currentPlayer.GetComponent<AudioListener>().enabled = false;

        currentPlayer = Instantiate(playerPrefab, pos, Quaternion.identity).GetComponent<Player>();
        currentPlayer.ChangeColor(RandomColor());

        trackCamera.target = currentPlayer.transform;
        players.Add(currentPlayer);
    }

    private void SwitchPlayers(bool direction)
    {
        currentPlayer.enabled = false;
        currentPlayer.GetComponent<AudioListener>().enabled = false;

        int playerIndex = players.IndexOf(currentPlayer);
        if (direction == false && playerIndex > 0)
        {
            currentPlayer = players[players.IndexOf(currentPlayer) - 1];
        }
        else if (direction && playerIndex < players.Count - 1)
        {
            currentPlayer = players[players.IndexOf(currentPlayer) + 1];
        }
        
        currentPlayer.enabled = true;
        currentPlayer.GetComponent<AudioListener>().enabled = true;

        trackCamera.target = currentPlayer.transform;
    }

    private Vector3 StablePosition()
    {
        Vector3 pos;
        
        // Loop through our stack of stable positions and find one where there are no other objects intersecting it.
        do
        {
            pos = lastStablePositions.Pop();
            Collider[] intersecting = Physics.OverlapSphere(pos, 0.9f, ~(1 << 8 | 1 << 10 | 1 << 5));
            if (intersecting.Length == 0)
            {
                break;
            }
        } while (lastStablePositions.Count != 0);
        return pos;
    }
    

    private static Color RandomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
