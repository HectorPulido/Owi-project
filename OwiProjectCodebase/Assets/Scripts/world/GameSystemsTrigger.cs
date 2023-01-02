using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemsTrigger : MonoBehaviour
{
    public void SetOnomatopoeiaPlace(Transform pivot)
    {
        Onomatopoeia.singleton.MoveText(pivot);
    }
    public void SetOnomatopoeiaText(string textToShow)
    {
        Onomatopoeia.singleton.SetText(textToShow);
    }

    public void SetEmotionTransform(Transform pivot)
    {
        EmotionalSystem.singleton.SetEmotionTransform(pivot);
    }

    public void SetEmotionSprite(string emotion)
    {
        EmotionalSystem.singleton.SetEmotionSprite(emotion);
    }

    public void HideEmotion()
    {
        EmotionalSystem.singleton.HideEmotion();
    }

    public void HideEmotionInTime(float time)
    {
        EmotionalSystem.singleton.HideEmotionInTime(time);
    }

    public void UpdatePlayers()
    {
        PlayerController.singleton.UpdatePlayers();
    }

    public void SimpleDialog(string text)
    {
        var textToShow = text;

        DialogSystem.singleton.StartDialog();
        DialogSystem.singleton.SetConversation(
            textToShow,
            "<b>Alerta</b>",
            null
        );

    }

    public static void SetMaxOrthoSize(float value)
    {
        VirtualCameraManager.SetMaxOrthoSize(value);
    }

    public static void SetMinOrthoSize(float value)
    {
        VirtualCameraManager.SetMinOrthoSize(value);
    }

    public static void SetMaxDistance(float value)
    {
        VirtualCameraManager.SetMaxDistance(value);
    }

    public static void SetMinDistance(float value)
    {
        VirtualCameraManager.SetMinDistance(value);
    }

    public void FadeIn(float time)
    {
        BlackScreenManager.singleton.FadeIn(time);
    }
    public void FadeOut(float time)
    {
        BlackScreenManager.singleton.FadeOut(time);
    }
    public void SetBlackScreen()
    {
        BlackScreenManager.singleton.SetBlackScreenAlpha();
    }
}
