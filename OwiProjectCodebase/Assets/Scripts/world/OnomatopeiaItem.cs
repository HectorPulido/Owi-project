using UnityEngine;
using TMPro;

public class OnomatopeiaItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textHolder;

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        Dependencies();
    }

    private void Dependencies()
    {
        if (textHolder == null)
            textHolder = GetComponentInChildren<TMP_Text>();
        
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void SetOnomatopoeia(Vector3 position, string textToShow) {
        animator.SetTrigger("play");
        transform.position = position;
        textHolder.text = textToShow;
    }
}
