using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterEvent : MonoBehaviour
{
    public UnityEvent onDieEvent;
    public int targetCount = 5;

    private int currentCount = 0;

    public void Increment()
    {
        currentCount++;
        if (currentCount >= targetCount)
        {
            onDieEvent.Invoke();
        }
    }
}
