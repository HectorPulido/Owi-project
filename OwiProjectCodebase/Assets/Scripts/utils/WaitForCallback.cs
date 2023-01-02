using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaitForCallback
{
    public static IEnumerator WaitForSecondsCallback(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
}