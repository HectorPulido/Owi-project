using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EmotionalSystem : MonoBehaviour
{

    [SerializeField]
    private string[] emotionsNames;

    [SerializeField]
    private Sprite[] emotions;

    [SerializeField]
    private Image emotionPlace;

    public static EmotionalSystem singleton;

    private Transform follow;

    private void Start()
    {
        if (!singleton)
        {
            singleton = this;
            return;
        }

        Destroy(gameObject);
    }

    private void Update(){
        if(!follow){
            return;
        }
        transform.position = follow.position;
    }

    public void HideEmotion(){
        follow = null;
        transform.position = Vector3.one * 999;
    }

    public void HideEmotionInTime(float time){
        StartCoroutine(WaitForCallback.WaitForSecondsCallback(time, ()=>EmotionalSystem.singleton.HideEmotion()));
    }

    public void SetEmotionTransform(Transform t) {
        follow = t;
    }

    public void SetEmotionSprite(string emotion){
        int index = Array.IndexOf(emotionsNames, emotion.ToLower());
        emotionPlace.sprite = emotions[index];
    }
}
