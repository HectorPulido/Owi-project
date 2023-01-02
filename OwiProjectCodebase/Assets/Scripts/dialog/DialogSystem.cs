using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogBackground;
    public GameObject nameBackground;
    public TMP_Text nameText;
    public TMP_Text dialogText;
    public float timeToShowCharacter;
    public Action callback;



    // public Transform dialogOptions;
    // public Transform cursor;
    // public DialogOptionItem dialogOptionPrefab;

    // public Image characterIlustration;
    // public Image backgroundIlustration;




    private bool writing;
    private DialogOption[] options;
    // private int currentCursorPosition = 0;

    public static DialogSystem singleton;

    public static bool DialogIsActive
    {
        get
        {
            if (!singleton)
                return false;

            return singleton.dialogBackground.activeInHierarchy;
        }
    }

    private void Start()
    {
        if (!singleton)
        {
            singleton = this;
            StopDialog();
            // SetCursorOutside();
            return;
        }

        Destroy(gameObject);
    }

    // private void SetCursorOutside()
    // {
    //     cursor.position = new Vector3(99999, 99999);
    // }

    // private void SetCursor(int cursorId)
    // {
    //     if (dialogOptions.childCount == 0)
    //     {
    //         SetCursorOutside();
    //         return;
    //     }

    //     cursorId = cursorId % dialogOptions.childCount;
    //     if (cursorId < 0)
    //     {
    //         cursorId = dialogOptions.childCount + cursorId;
    //     }

    //     currentCursorPosition = cursorId;

    //     cursor.position = dialogOptions.GetChild(cursorId).position;
    // }

    public void StartDialog()
    {
        dialogBackground.SetActive(true);
        nameBackground.SetActive(true);
    }

    public void ContinueDialog()
    {
        if (writing == true)
        {
            writing = false;
            return;
        }

        // if (options != null)
        // {
        //     options[currentCursorPosition].callback.Invoke();
        //     DeleteOptions();
        //     return;
        // }

        if (callback == null)
        {
            StopDialog();
            return;
        }

        callback.Invoke();
    }

    // public void SetCharacterIlustration(Sprite characterIlustration)
    // {
    //     this.characterIlustration.sprite = characterIlustration;
    // }

    // public void SetBackgoundIlustration(Sprite backgroundIlustration)
    // {
    //     this.backgroundIlustration.sprite = backgroundIlustration;
    // }

    // public void DeleteOptions()
    // {
    //     foreach (Transform child in dialogOptions)
    //     {
    //         GameObject.Destroy(child.gameObject);
    //     }
    //     options = null;
    //     SetCursorOutside();
    // }

    // public void SetOptions(DialogOption[] options)
    // {
    //     DeleteOptions();
    //     for (int i = 0; i < options.Length; i++)
    //     {
    //         var instance = Instantiate(dialogOptionPrefab);
    //         instance.text.text = options[i].text;
    //         instance.transform.SetParent(dialogOptions);
    //         instance.transform.position = Vector2.zero;
    //     }
    //     this.options = options;

    //     SendWaitForSecondsCallback(0.01f, () =>
    //     {
    //         SetCursor(0);
    //     });
    // }

    public void SetConversation(
        string conversation,
        string title,
        Action callback
        )
    {

        this.callback = callback;
        nameText.text = title;

        StartCoroutine(CharacterToCharacter(conversation));
    }

    private IEnumerator CharacterToCharacter(string text)
    {
        writing = true;
        dialogText.text = text;
        dialogText.maxVisibleCharacters = 0;
        for (int i = 0; i <= text.Length; i++)
        {
            if (!writing)
            {
                dialogText.maxVisibleCharacters = text.Length;
                break;
            }

            dialogText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(timeToShowCharacter);
        }
        writing = false;
    }

    public void StopDialog()
    {
        nameBackground.SetActive(false);
        dialogBackground.SetActive(false);
    }

    // private void Update()
    // {
    //     if (options != null)
    //     {
    //         if (options.Length != 0)
    //         {
    //             if (InputManager.RightArrowEnter)
    //             {
    //                 SetCursor(currentCursorPosition + 1);
    //             }
    //             if (InputManager.LeftArrowEnter)
    //             {
    //                 SetCursor(currentCursorPosition - 1);
    //             }
    //         }
    //     }
    // }

    private void SendWaitForSecondsCallback(float time, Action callback)
    {
        StartCoroutine(WaitForCallback.WaitForSecondsCallback(time, callback));
    }

}
