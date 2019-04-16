using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Unless denoted by a commented out link, TK wrote literally everything here

public class SceneChanger : MonoBehaviour
{

    public void Start()
    {
//        this.StartCoroutine(fadeInDisableScreen());

    }

    // Exit the game from Main Menu
    public void MenuQuitGame()
    {
        Debug.Log("The Game is Exiting.");
        Application.Quit();
    }

    // Load scene by number in build order
    public void GoSceneNumber(int num)
    {
        Debug.Log("Loading Scene Number " + num + ".");
        SceneManager.LoadScene(num);
    }

    // ------------------------------------------------------ //

    // FADE IN/OUT STUFF:

    // Drag AudioManager object that's IN SCENE to this place in inspector
    public AudioManagerScript audioManager;
    public Animator blackScreeneAnimator;
    public GameObject blackScreen;

    public IEnumerator fadeInDisableScreen()
    {
        yield return new WaitForSeconds(1.1f);
        blackScreen.SetActive(false);
    }

    public void sceneFadeOutKeepMusic(int scene)
    {
        // enable blackScreen UI object
        blackScreen.SetActive(true);

        // trigger fade to black animation
        blackScreeneAnimator.SetTrigger("BlackFadeOutAnimation");

        // start delayed scene switch
        this.StartCoroutine(fadeOutSwitchScene(scene));
    }

    // scene is the scene number we are switching to
    public void masterSceneFadeOut(int scene)
    {
        // enable blackScreen UI object
        blackScreen.SetActive(true);

        // trigger fade to black animation
        blackScreeneAnimator.SetTrigger("BlackFadeOutAnimation");

        // fade the music out (time)
        audioManager.FadeOutBGM(1.2f);

        // start delayed scene switch
        this.StartCoroutine(fadeOutSwitchScene(scene));
    }

    public IEnumerator fadeOutSwitchScene(int scene)
    {
        yield return new WaitForSeconds(1.3f);
        GoSceneNumber(scene);
    }


    // ------------------------------------------------------ //

    // WARNING!!!!!!!!!!!!!!!
    // If you rename any of the scenes 
    // these functions will not work anymore:

    public void GoMainMenu()
    {
        Debug.Log("Loading Main Menu.");
        SceneManager.LoadScene("0MainMenu");
    }

    public void GoGameOver()
    {
        Debug.Log("Loading Game Over.");
        SceneManager.LoadScene("1GameOver");
    }

    public void GoBeatGame()
    {
        Debug.Log("Loading Beat Game.");
        SceneManager.LoadScene("2BeatGame");
    }

    public void GoCredits()
    {
        Debug.Log("Loading Credits.");
        SceneManager.LoadScene("3Credits");
    }

    public void GoGymMenu()
    {
        Debug.Log("Loading GymMenu.");
        SceneManager.LoadScene("4GymMenu");
    }

    public void GoMatch()
    {
        Debug.Log("Loading Match.");
        SceneManager.LoadScene("5Match");
    }

    public void GoSparring()
    {
        Debug.Log("Loading Sparring.");
        SceneManager.LoadScene("6Sparring");
    }

    public void GoRankUp()
    {
        Debug.Log("Loading Rank Up.");
        SceneManager.LoadScene("7RankUp");
    }

    public void GoSpendEXP()
    {
        Debug.Log("Loading Spend EXP.");
        SceneManager.LoadScene("8SpendEXP");
    }
}
