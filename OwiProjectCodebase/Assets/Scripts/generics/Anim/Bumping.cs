
using UnityEngine;

public class Bumping : MonoBehaviour
{
    [SerializeField]
    float minScaleY = 1;

    [SerializeField]
    float maxScaleY = 1;

    [SerializeField]
    float minScaleX = 1;

    [SerializeField]
    float maxScaleX = 1;

    [SerializeField]
    float idleSpeed = 20;
    
    [SerializeField]
    float movingSpeed = 80;

    [SerializeField]
    bool billboard = false;

    [HideInInspector]
    public bool moving = false;

    private float currentSpeed = 20;
    private Vector3 newScale;


    private void Update(){
        Bump();
        Billboard();
    }

    private void Billboard() {
        if(!billboard)
            return;

        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);

    }

    private void Bump(){
        currentSpeed = moving ? movingSpeed : idleSpeed;

        newScale = Vector3.one;
        
        newScale.y = Mathf.Abs(Mathf.Sin(Time.time * currentSpeed));
        newScale.y = rescaleFloat(minScaleY, maxScaleY, newScale.y);

        newScale.x = Mathf.Abs(Mathf.Cos(Time.time * currentSpeed));
        newScale.x = rescaleFloat(minScaleX, maxScaleX, newScale.x);

        transform.localScale = newScale;
    }

    private float rescaleFloat(float min, float max, float v) {
        return (v * (max - min)) + min;
    }
}
