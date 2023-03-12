using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenManager : MonoBehaviour
{
    // This is the script of a black screen that can appear and disappear
    // at any moment, so needs a singleton

    public bool initialFaded = true;
    public static BlackScreenManager singleton;
    private Image blackScreen;

    void Awake()
    {
        if (singleton)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
        blackScreen = GetComponent<Image>();

        if (initialFaded)
        {
            StartCoroutine(InitialFade());
        }
    }

    IEnumerator InitialFade() {
        SetBlackScreenAlpha();
        yield return new WaitForSeconds(1.5f);
        yield return FadeTo(0, 1f);
    }


    // This coroutine set the alpha of the black screen to a value from the current
    // alpha value to the target alpha value in a given time
    public IEnumerator FadeTo(float targetAlpha, float time)
    {
        float currentAlpha = blackScreen.color.a;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            blackScreen.color = new Color(0, 0, 0, Mathf.Lerp(currentAlpha, targetAlpha, t));
            yield return null;
        }
    }

    // This function fades the screen from white to black
    public void FadeIn(float time)
    {
        StartCoroutine(FadeTo(1, time));
    }

    // This function fades the screen from black to white
    public void FadeOut(float time)
    {
        StartCoroutine(FadeTo(0, time));
    }

    public void SetBlackScreenAlpha()
    {
        blackScreen.color = new Color(0, 0, 0, 1);
    }
}
