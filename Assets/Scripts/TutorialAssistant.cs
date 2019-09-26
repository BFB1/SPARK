using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialAssistant : MonoBehaviour
{
    
    // Fades in helpful information if the player appears to be stuck.

    [SerializeField]
    private int helpWait;

    private bool triggered = false;
    private bool finished = false;
    
    private Coroutine runningTimer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && !finished)
        {
            runningTimer = StartCoroutine(DisplayHelp());
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (runningTimer != null && !finished)
        {
            StopCoroutine(runningTimer);
            runningTimer = null;
            triggered = false;
        }
    }

    private IEnumerator DisplayHelp()
    {
        yield return new WaitForSeconds(helpWait);
        finished = true;
        StartCoroutine(FadeInText(GetComponentInChildren<TextMeshProUGUI>()));
    }

    private IEnumerator FadeInText(TextMeshProUGUI target)
    {
        while (target.color.a != 1f)
        {
            target.color = new Color(1, 1, 1, target.color.a + 0.01f);
            yield return null;
        }
    }
}
