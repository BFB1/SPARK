using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : MonoBehaviour
{
    public void OnTriggerEnter()
    {
        Score.SubmitScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
