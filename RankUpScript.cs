using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Unless denoted by a commented out link, TK wrote literally everything here

public class RankUpScript : MonoBehaviour
{
    public SavePrefs prefs;
    public GameObject rankCheer;
    public GameObject rankText;
    public GameObject expText;
    public SceneChanger sceneChanger;
    public Button exitButton;


    [System.NonSerialized]
    public bool Sparring = false;
    public int PlayerRank = 3;
    public int PlayerEXP = 0;
    public int MaxEXP = 0;
    public int TotalEXP = 0;
    public int GiveThisMuchEXP = 0;
    public int updateCount = 0;
    public bool startCount = false;
    public bool exitNow = false;
    public int SparringR2EXP;
    public int SparringR1EXP;
    public int MatchR2EXP;
    public int MatchR1EXP;
    public int MaxEXPR2;
    public int MaxEXPR1;

    public bool MaxedOut = false;
    // -----------------------------------------------------------------------//

    // Start is called before the first frame update
    void Start()
    {
        // Set correct values
        SparringR2EXP = 250;
        SparringR1EXP = 500;
        MatchR2EXP = 3000;
        MatchR1EXP = 6000;
        MaxEXPR2 = 3000;
        MaxEXPR1 = 6000;

        exitButton.enabled = false;
        exitButton.GetComponent<Image>().enabled = false;

        PlayerRank = PlayerPrefs.GetInt("PlayerRank");
        rankText.GetComponent<Text>().text = "" + PlayerRank;

        Sparring = prefs.getAbilityTF("SparringMatch");
        PlayerEXP = PlayerPrefs.GetInt("PlayerEXP");
        TotalEXP = PlayerPrefs.GetInt("TotalEXP");
        MaxedOut = checkMaxed();


        if (Sparring)
        {
            if (MaxedOut)
            {
                GiveThisMuchEXP = 0;
                expText.GetComponent<Text>().text = "+" + GiveThisMuchEXP + "\n" + PlayerEXP;
            }
            else
            {
                // Do sparing-only code
                SparringMatchSetup();

                // Turn Sparring off in player prefs
                prefs.SavePrefBool("SparringMatch", false);
            }
        }
        else
        {
            // Real Match set-up
            TrueMatchSetup();
        }


    }

    // -----------------------------------------------------------------------//

    public bool checkMaxed()
    {
        // this function checks if they have maxed earnable exp for that rank
        if (PlayerRank > 1 && TotalEXP >= MaxEXPR2)
        {
            return true;
        }
        else if (PlayerRank <= 1 && TotalEXP >= MaxEXPR1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // -----------------------------------------------------------------------//

    void SparringMatchSetup()
    {
        // Get rank & display in text box
        PlayerRank = PlayerPrefs.GetInt("PlayerRank");
        rankText.GetComponent<Text>().text = "" + PlayerRank;

        if (PlayerRank >= 2) // player rank >=2
        {
            GiveThisMuchEXP = SparringR2EXP;

            prefs.GainEXP(GiveThisMuchEXP);

/*          Kept for posterity  
            // Add current exp + granted exp
            newEXP = PlayerEXP + GiveThisMuchEXP;
            // Update playerprefs
            //                PlayerPrefs.SetInt("PlayerEXP", newEXP);
            prefs.SavePref("PlayerEXP", newEXP);

            // Update player's total earned EXP
            TotalEXP += GiveThisMuchEXP;
            prefs.SavePref("TotalEXP", TotalEXP);
*/
        }
        if (PlayerRank == 1)
        {
            GiveThisMuchEXP = SparringR1EXP;

            prefs.GainEXP(GiveThisMuchEXP);
        }

        expText.GetComponent<Text>().text = "+" + GiveThisMuchEXP + "\n" + PlayerEXP;

    }

    // -----------------------------------------------------------------------//

    void TrueMatchSetup()
    {
        if ((PlayerRank - 1) > 0) // New Player Rank will be > 0 (ie. hasn't won game yet.)
        {
            // Decrease rank as player wins
            PlayerRank--;
            // Save rank across scenes
            //            PlayerPrefs.SetInt("PlayerRank", PlayerRank);
            prefs.SavePref("PlayerRank", PlayerRank);

            if (PlayerRank == 2) // player goes 3->2
            {
                GiveThisMuchEXP = MatchR2EXP;

                prefs.GainEXP(MatchR2EXP);
            }
            if (PlayerRank == 1)
            {
                GiveThisMuchEXP = MatchR1EXP;

                prefs.GainEXP(MatchR1EXP);
            }

            expText.GetComponent<Text>().text = "+" + GiveThisMuchEXP + "\n" + PlayerEXP;

        }
        else if (PlayerRank > 3)
        {
            Debug.Log("PLAYER RANK IS TOO HIGH; SOMETHING BROKEN");
        }
        else // New Player rank 0 ie <1
        {
            rankText.GetComponent<Text>().text = "";
            expText.GetComponent<Text>().text = "";

            // Player Beat Game!
            prefs.SavePrefBool("BeatGame", true);
//            PlayerPrefs.SetInt("BeatGame", 1);

            // Load Beat Game Scene (no delay)
            sceneChanger.GoSceneNumber(2);
        }
    }

    // -----------------------------------------------------------------------//

    void Update()
    {
        if (startCount)
        {
            updateCount++;
            if (GiveThisMuchEXP > 0)
            {
                GiveThisMuchEXP -= 25;
                PlayerEXP += 25;
                expText.GetComponent<Text>().text = "+" + GiveThisMuchEXP + "\n" + PlayerEXP;

                if (updateCount > 30 && Input.anyKey)
                {
                    PlayerEXP += GiveThisMuchEXP;
                    GiveThisMuchEXP -= GiveThisMuchEXP;
                }
            }
            else
            {
                expText.GetComponent<Text>().text = "\n" + PlayerEXP;
                exitButton.enabled = true;
                exitButton.GetComponent<Image>().enabled = true;
                exitButton.Select();
            }
        }
        else
        {
            if (Input.anyKey)
            {
                startCount = true;
                rankText.GetComponent<Text>().text = "" + PlayerRank;
                if (!Sparring)
                {
                    rankCheer.GetComponent<Text>().text = "Rank Up!";
                }
            }
        }
    }
}
