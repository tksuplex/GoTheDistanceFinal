using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Unless denoted by a commented out link, TK wrote literally everything here

// Auto calls Main Menu scene on a delay with fadeout of screen & music
// Only called for GameOver / Win Game

public class AutoSceneChange : MonoBehaviour
{
    public SceneChanger sceneChanger;
    public Animator blackScreenAnimator;

    public float waitSeconds = 4.0f;
    public int sceneNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        Scene s = SceneManager.GetActiveScene();
        if (s.name == "1GameOver")
        {
            StartCoroutine(WaitSceneChange(waitSeconds, sceneNum));
        }
        else if (s.name == "2BeatGame")
        {
            waitSeconds = 9.0f;
            sceneNum = 3; // currently points to mainmenu, may change to credits
            StartCoroutine(WaitSceneKeepBGM(waitSeconds, sceneNum));
        }
    }

    IEnumerator WaitSceneChange(float sec, int scene)
    {
        yield return new WaitForSeconds(sec);
        Debug.Log("RETURNING TO GAME'S MAIN MENU!");
        sceneChanger.masterSceneFadeOut(scene);
    }

    IEnumerator WaitSceneKeepBGM(float sec, int scene)
    {
        yield return new WaitForSeconds(sec);
        Debug.Log("RETURNING TO GAME'S MAIN MENU!");
        sceneChanger.sceneFadeOutKeepMusic(scene);
    }
}
