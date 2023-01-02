using UnityEngine;
using UnityEngine.Events;

// struct for the dialog item
[System.Serializable]
public struct DialogItem
{
    public string title;
    public string text;
    public UnityEvent callback;
}

public class SimpleDialogActionable : Actionable
{
    [SerializeField]
    private DialogItem[] dialogItems;

    [SerializeField]
    private UnityEvent finalCallback;

    private int iterator = 0;

    public override void Act()
    {
        iterator = 0;
        ShowText();
    }

    public void ShowText()
    {
        var textToShow = dialogItems[iterator].text;
        var titleToShow = dialogItems[iterator].title;


        System.Action callback = () => ShowText();

        if (iterator + 1 >= dialogItems.Length)
        {
            finalCallback.Invoke();
            callback = null;
        }

        DialogSystem.singleton.StartDialog();
        DialogSystem.singleton.SetConversation(
            textToShow,
            titleToShow,
            callback
        );
        dialogItems[iterator].callback.Invoke();
        iterator++;
    }
}