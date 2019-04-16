using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Unless denoted by a commented out link, TK wrote literally everything here


// This script sets a playerpref improvised bool to indicate 
// if a sparring match is happening to true, 
// then loads the Match Scene

public class SparringScript : MonoBehaviour
{
    public SavePrefs prefs;
    public SceneChanger sceneChanger;

//    public float waitSeconds = 1.0f;
    public int sceneNum = 5;

    // Start is called before the first frame update
    void Start()
    {
        Scene s = SceneManager.GetActiveScene();
        if (s.buildIndex == 6 || s.name == "6Sparring")
        {
            // Debug.Log("You are in Scene 6!");
            bool doSparring;
            prefs.SavePrefBool("SparringMatch", true);
            doSparring = prefs.GetPrefBool("SparringMatch");
            Debug.Log("Sparring Status set to: " + doSparring);
            sceneChanger.GoSceneNumber(sceneNum);
        }

    }
}
