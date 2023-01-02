using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceZone : MonoBehaviour
{
    public Transform pivot;
    public string textToShow;


    private void OnTriggerEnter2D()
    {
        WorldText.singleton.SetText(pivot.position, textToShow);
    }

    private void OnTriggerExit2D()
    {
        WorldText.singleton.HideText();
    }

    private void OnTriggerEnter()
    {
        WorldText.singleton.SetText(pivot.position, textToShow);
    }

    private void OnTriggerExit()
    {
        WorldText.singleton.HideText();
    }
}
