using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagerScript : MonoBehaviour
{
    // Unless denoted by a commented out link, TK wrote literally everything here

    public AudioClip MainMenuBGM;
    public AudioClip GameOverBGM;
    public AudioClip VictoryBGM;
    public AudioClip GymBGM;
    public AudioClip SparringBGM;
    public AudioClip MatchBGM;
    public AudioClip Rank2MatchBGM;
    public AudioClip Rank1MatchBGM;
    public AudioClip RankCMatchBGM;

    public float volumeMAX;

    [System.NonSerialized]
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        volumeMAX = 0.5f;

        // Get the background music object to use it's AudioSource
        GameObject go = GameObject.Find("BGMObject");
        musicSource = go.GetComponent<AudioSource>();

        musicSource.volume = volumeMAX;

        checkSceneBGM();
    }

    void checkSceneBGM()
    {
        bool fadeIn = true;
        bool loop = true;
        AudioClip clippy = null;
        float fadeInTime = 0;

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "0MainMenu")
        {
            loop = true;
            fadeIn = true;
            fadeInTime = 5.0f;
            clippy = MainMenuBGM;
        }
        if (scene.name == "3Credits")
        {
            if (PlayerPrefs.GetInt("BeatGame") == 1)
            {
                Debug.Log("PLAYER BEAT GAME!");

                loop = true;
                fadeIn = false;
                clippy = VictoryBGM;
            }
            else
            {
                loop = true;
                fadeIn = true;
                fadeInTime = 5.0f;
                clippy = MainMenuBGM;
            }
        }
        if (scene.name == "1GameOver")
        {
            loop = false;
            fadeIn = false;
            clippy = GameOverBGM;
        }
        if (scene.name == "4GymMenu" || scene.name == "8SpendEXP")
        {
            loop = true;
            fadeIn = true;
            fadeInTime = 1.5f;
            clippy = GymBGM;
        }
        if (scene.name == "7RankUp")
        {
            loop = true;
            fadeIn = false;
            clippy = VictoryBGM;
        }
        if (scene.name == "2BeatGame")
        {
            loop = true;
            fadeIn = false;
            clippy = VictoryBGM;
        }
        if (scene.name == "5Match")
        {
            if (PlayerPrefs.GetInt("Sparring") == 1)
            {
                // Play the Sparring Music
                loop = true;
                fadeIn = true;
                fadeInTime = 1.5f;
                clippy = SparringBGM;
            }
            else
            {
                // Play Match Music
                loop = true;
                fadeIn = true;
                fadeInTime = 1.5f;
                clippy = MatchBGM;
            }
        }
        checkCurrentBGM(clippy, loop, fadeIn, fadeInTime);
    }


    void checkCurrentBGM(AudioClip bgm, bool loop, bool fadeIn, float fadeInTime)
    {
        if (musicSource.isPlaying)
        {
            if (musicSource.clip != bgm) // change to correct BGM
            {
                StartBGM(bgm, loop, fadeIn, fadeInTime);
            }
            else
            {
                Debug.Log("The correct BGM clip is playing!");
            }
        }
        else // not playing anything
        {
            StartBGM(bgm, loop, fadeIn, fadeInTime);
        }
    }

    public void StartBGM(AudioClip bgm, bool loop, bool fadeIn, float fadeInTime)
    {
        StopBGM();
        musicSource.loop = loop;
        musicSource.clip = bgm;
        if (fadeIn)
        {
            FadeInBGM(fadeInTime);
        }
        else
        {
            musicSource.volume = volumeMAX;
            musicSource.Play();
        }
    }


    public void FadeOutBGM(float time)
    {
        if (musicSource == null)
        {
            Debug.Log("MUSIC SOURCE IS NULL !?!?!?!");
        }
        StartCoroutine(FadeOut(musicSource, time));
    }

    public void FadeInBGM(float time)
    {
        StartCoroutine(FadeIn(musicSource, time));
    }

    // https://medium.com/@wyattferguson/how-to-fade-out-in-audio-in-unity-8fce422ab1a8
    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
    }


    // https://medium.com/@wyattferguson/how-to-fade-out-in-audio-in-unity-8fce422ab1a8
    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < volumeMAX)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

    public void StopBGM()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}
