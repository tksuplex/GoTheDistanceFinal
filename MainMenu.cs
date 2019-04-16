using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unless denoted by a commented out link, TK wrote literally everything here

public class MainMenu : MonoBehaviour
{
    /*
     * this script will deal with functions and code
     * relating specifically to things that happen in the main menu
     */

    // Need this code to set BeatGame value to false on Open Main Menu 
    // (ie. beat game)

    public SavePrefs prefs;
    private void Start()
    {
        prefs.SavePrefBool("BeatGame", false);
        
    }














    /*
    *  Most of this is old and not being used anymore:
   /*  I was using it to learn stuff
    * here is some examples of calling other scripts from this script
    */
    // METHOD A:

    // you have to drag the SceneChanger GameObject
    // int the 'scene changer' slot in the inspector of the object
    // this script is attached to
    public SceneChanger sceneChanger;

    public void MethodA()
    {
        // calls ChangeSceneExample function from SceneChanger script
//        sceneChanger.GoGameOver();
        sceneChanger.GoSceneNumber(4);
    }

    // METHOD B:
    // make sure both scripts you want to use are attached
    // as components to the calling object (a button in this case)
    // ie. SceneChanger script added to button as component to use this
    public void MethodB()
    {
        FindObjectOfType<SceneChanger>().GoGameOver();
    }

    // METHOD B:
    // drag SceneChanger OBJECT into scene
    public void MethodC()
    {
        // drag SceneChanger OBJECT into scene
        FindObjectOfType<SceneChanger>().GoMainMenu();
        // this will only work if object in scene 
        // because there is a coroutine to it
    }

}
