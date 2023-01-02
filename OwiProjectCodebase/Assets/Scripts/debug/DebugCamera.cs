using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugCamera : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private void Update()
    {
        var vertical = InputManager.VerticalAxis;
        var horizontal = InputManager.HorizontalAxis;
        var movement = new Vector3(horizontal, vertical);
        transform.position += movement * speed * Time.deltaTime;
    }
}
