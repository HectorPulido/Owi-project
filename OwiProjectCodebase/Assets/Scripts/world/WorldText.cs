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

            cameraTransform = VirtualCameraManager.singleton.vcam.transform;
            return;
        }

        Destroy(gameObject);
    }

    public void SetText(Vector3 position, string textToShow) {
        textHolder.gameObject.SetActive(true);
        textHolder.text = textToShow;
        transform.position = position;
    }

    public void HideText() {
        textHolder.gameObject.SetActive(false);
    }

    private void Update(){
        transform.LookAt(cameraTransform);
    }


}
