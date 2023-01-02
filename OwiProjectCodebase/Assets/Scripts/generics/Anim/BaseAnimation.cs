using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct EventInAnimation
{
    public string name;
    public float time;
    public UnityEvent onAnimation;
}

public class BaseAnimation : MonoBehaviour
{
    [Header("Position Animation")]
    public AnimationCurve postionXCurve;
    public AnimationCurve postionYCurve;
    public AnimationCurve postionZCurve;

    [Header("Rotation Animation")]
    public AnimationCurve rotationXCurve;
    public AnimationCurve rotationYCurve;
    public AnimationCurve rotationZCurve;

    [Header("Scale Animation")]
    public AnimationCurve scaleXCurve;
    public AnimationCurve scaleYCurve;
    public AnimationCurve scaleZCurve;

    [Header("Animation data")]
    public float duration = 1f;
    public float delay = 0f;
    public bool repeat = false;

    [Header("Events data")]
    public UnityEvent OnAnimationFinished;
    public EventInAnimation[] events;

    private Vector3 initialPosition;
    private Vector3 initialRotation;
    private Vector3 initialScale;

    private float timer = 0f;
    private bool animationStarted = false;
    private bool[] eventsBool;

    public bool animationPlaying
    {
        get
        {
            return animationStarted;
        }
    }

    private void Update()
    {
        if (!animationStarted)
            return;

        timer += Time.deltaTime;

        var postionX = postionXCurve.Evaluate(timer / duration);
        var postionY = postionYCurve.Evaluate(timer / duration);
        var postionZ = postionZCurve.Evaluate(timer / duration);
        transform.localPosition = initialPosition + new Vector3(postionX, postionY, postionZ);

        var rotationX = rotationXCurve.Evaluate(timer / duration);
        var rotationY = rotationYCurve.Evaluate(timer / duration);
        var rotationZ = rotationZCurve.Evaluate(timer / duration);
        transform.eulerAngles = initialRotation + new Vector3(rotationX, rotationY, rotationZ);

        var scaleX = scaleXCurve.Evaluate(timer / duration);
        var scaleY = scaleYCurve.Evaluate(timer / duration);
        var scaleZ = scaleZCurve.Evaluate(timer / duration);
        transform.localScale = initialScale + new Vector3(scaleX, scaleY, scaleZ);

        for (int i = 0; i < events.Length; i++)
        {
            if (timer / duration > events[i].time && !eventsBool[i])
            {
                events[i].onAnimation.Invoke();
                eventsBool[i] = true;
            }
        }


        if (timer > duration)
        {
            timer = timer % duration;
            OnAnimationFinished.Invoke();
            if (!repeat)
            {
                animationStarted = false;
                timer = 0;
            }
        }

    }

    [ContextMenu("Start Animation")]
    public void StartAnimation()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.eulerAngles;
        initialScale = transform.localScale;

        eventsBool = new bool[events.Length];
        for (int i = 0; i < eventsBool.Length; i++)
        {
            eventsBool[i] = false;
        }

        StartCoroutine(WaitForCallback.WaitForSecondsCallback(delay,
            () =>
            {
                animationStarted = true;
                timer = 0;
            }
        ));
    }
}