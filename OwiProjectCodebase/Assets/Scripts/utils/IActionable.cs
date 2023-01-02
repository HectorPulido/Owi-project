using UnityEngine;

public class Actionable : MonoBehaviour
{
    public int priority = 1;
    public virtual void Act() {}
}