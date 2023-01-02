using UnityEngine;


public class InputManager : MonoBehaviour
{
    public static float VerticalAxis
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }

    public static float HorizontalAxis
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public static bool TriggerEnter
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Z);
        }
    }
    public static bool Attack1Enter
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Q);
        }
    }
    public static bool Attack2Enter
    {
        get
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }
    public static bool MouseClick
    {
        get
        {
            return Input.GetMouseButtonDown(0);
        }
    }

    public static bool TogglePlayerEnter
    {
        get
        {
            return Input.GetKeyDown(KeyCode.X);
        }
    }
    public static bool FollowPlayer
    {
        get
        {
            return Input.GetKey(KeyCode.Space);
        }
    }

    public static bool LeftArrowEnter
    {
        get
        {
            return Input.GetKeyDown(KeyCode.LeftArrow);
        }
    }
    public static bool RightArrowEnter
    {
        get
        {
            return Input.GetKeyDown(KeyCode.RightArrow);
        }
    }
    public static bool UpArrowEnter
    {
        get
        {
            return Input.GetKeyDown(KeyCode.UpArrow);
        }
    }
    public static bool DownArrowEnter
    {
        get
        {
            return Input.GetKeyDown(KeyCode.DownArrow);
        }
    }
}
