using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Actionable
{
    // This is the script of a door that remove the current scene and load a new one
    [SerializeField]
    private string sceneName;
    
    [SerializeField]
    private float fadeTime = 1f;
    
    [SerializeField]
    private int minCollidersToTrigger = 0;


    private int items = 0;

    public override void Act()
    {
        // if there are not enough colliders to trigger the door, do nothing
        if(items < minCollidersToTrigger) 
            return;

        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        BlackScreenManager.singleton.FadeIn(fadeTime);
        
        yield return new WaitForSeconds(fadeTime);
        
        // then remove the current scene
        print("Unloading scene " + SceneManager.GetActiveScene().name);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        
        // and load the new scene
        print("Loading scene " + sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void OnTriggerEnter()
    {
        items++; 
    }

    private void OnTriggerExit()
    {
        items--;
    }


}
