using UnityEngine;
using TMPro;


public class WorldText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textHolder;
    public static WorldText singleton;

    private Transform cameraTransform;

    private void Start()
    {
        if (!singleton)
        {
            singleton = this;
            HideText();

            cameraTransform = Camera.main.transform;
            return;
        }

        Destroy(gameObject);
    }

    public void SetText(Vector3 position, string textToShow) {
        textHolder.transform.parent.gameObject.SetActive(true);
        textHolder.text = textToShow;
        transform.position = position;
    }

    public void HideText() {
        textHolder.transform.parent.gameObject.SetActive(false);
    }

    private void Update() {
        var rot = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.rotation = rot;
    }


}
