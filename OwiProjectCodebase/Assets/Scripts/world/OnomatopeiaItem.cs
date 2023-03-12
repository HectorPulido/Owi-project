using UnityEngine;
using TMPro;

public class OnomatopeiaItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textHolder;

    [SerializeField]
    private BaseAnimation animator;

    private void Awake()
    {
        Dependencies();
    }

    private void Update()
    {
        var rot = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.rotation = rot;
    }

    private void Dependencies()
    {
        if (textHolder == null)
            textHolder = GetComponentInChildren<TMP_Text>();

        if (animator == null)
            animator = GetComponent<BaseAnimation>();
    }

    public void SetOnomatopoeia(Vector3 position, string textToShow)
    {
        transform.position = position;
        textHolder.text = textToShow;
        animator.StartAnimation();
    }
}
