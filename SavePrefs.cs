using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Unless denoted by a commented out link, TK wrote literally everything here

public class SavePrefs : MonoBehaviour
{
    // Player rank (can be 3,2,1,0; 0 = Champion & win game)
    int PlayerRank;

    // Current stats for player
    int PlayerMaxHP;
    int PlayerMaxStamina;
    int PlayerPower;
    int PlayerSpeed;
    int PlayerTough;

    // Player's current EXP amount 
    // (more like currency to be spent)
    int PlayerEXP;

    // Used to cap EXP gain btw levels ?
    int TotalEXP;

    // Toggles on abilities
    // bool not allowed; 0 is falso / 1 is true
    // Winning 1st match and ranking up to 2 toggles Ability1 to true/1;
    int NewAbility1;
    int NewAbility2;
    bool BoolAbility1;
    bool BoolAbility2;

    // Keeps track of the upgrades a player has bought per rank
    // Values can be 0-2
    int Rank2Upgrades;
    int Rank1Upgrades;

    // If the current battle is a real match or sparring
    // bool not allowed; 0 is falso / 1 is true
    int SparringMatch;
    bool BoolSparring;

    int BeatGame;

    // -----------------------------------------------------------------------//

    // This function is called when player selects 'Play' from
    // the MAIN MENU. It resets all Prefs to the starting positions we want
    // because it is a new game.
    public void NewGamePrefSet()
    {
        // SET PLAYER STARTING STAT VALUES HERE!!!!!
        // THESE ARE RANDOMLY CHOSEN VALUES FOR TESTING
        // CHANGE THEM!!!!!!!
        // *change this note after you fix them also :o
        PlayerPower = 7;
        PlayerSpeed = 6;
        PlayerTough = 7;

        PlayerMaxHP = (PlayerTough * 100) / 2;
        PlayerMaxStamina = (PlayerPower * 10);

        // Starting rank for player is always 3
        PlayerPrefs.SetInt("PlayerRank", 3);

        // IDK what the values of these should be yet
        // We will have to figure that out later
        PlayerPrefs.SetInt("PlayerMaxHP", PlayerMaxHP);
        PlayerPrefs.SetInt("PlayerMaxStamina", PlayerMaxStamina);
        PlayerPrefs.SetInt("PlayerPower", PlayerPower);
        PlayerPrefs.SetInt("PlayerSpeed", PlayerSpeed);
        PlayerPrefs.SetInt("PlayerTough", PlayerTough);

        // Player starts game with 0 EXP
        PlayerPrefs.SetInt("PlayerEXP", 0);
        PlayerPrefs.SetInt("TotalEXP", 0);

        // Bool don't exist for prefs :< so we have to use 0/1
        // 0 is falso / 1 is true
        PlayerPrefs.SetInt("NewAbility1", 0);
        PlayerPrefs.SetInt("NewAbility2", 0);

        // Bool for sparring, default to false (0)
        PlayerPrefs.SetInt("SparringMatch", 0);

        // Fake bool for having beaten the game set to false (0)
        PlayerPrefs.SetInt("BeatGame", 0);

        PlayerPrefs.SetInt("Rank2Upgrades", 0);
        PlayerPrefs.SetInt("Rank1Upgrades", 0);

        // DO NOT FORGET TO SAVE AFTER UPDATE!
        PlayerPrefs.Save();

        Debug.Log("A new game has been started with the default player values.");
    }

    // -----------------------------------------------------------------------//

    public void spendEXP(int cost)
    {
        PlayerEXP = GetPref("PlayerEXP");
        SavePref("PlayerEXP", (PlayerEXP - cost));
    }

    public void GainEXP(int expGain)
    {
        PlayerEXP = PlayerPrefs.GetInt("PlayerEXP");
        TotalEXP = PlayerPrefs.GetInt("TotalEXP");
        PlayerEXP += expGain;
        TotalEXP += expGain;
        SavePref("PlayerEXP", PlayerEXP);
        SavePref("TotalEXP", TotalEXP);

    }

    // -----------------------------------------------------------------------//

    // There are only 2 buyable abilities 
    // so It doesn't do anything if num is not 1 or 2
    public void buyAbilityYo(int num, int cost)
    {
        string abilName;
        if (num == 1)
        {
            abilName = "NewAbility1";
        }
        else if (num == 2)
        {
            abilName = "NewAbility2";
        }
        else
        {
            return;
        }

        SavePref(abilName, 1);
        spendEXP(cost);
        // Old stuff don't use:
        //            PlayerPrefs.SetInt(abilName, 1);
        //            PlayerPrefs.SetInt("PlayerEXP", (PlayerEXP - cost));
        // DO NOT FORGET TO SAVE AFTER UPDATE!
        // PlayerPrefs.Save();
    }

    // Player can only buy 2 stat increases per rank (2 and 1)
    // This keeps track of that and updates playerprefs
    public void updateRankStat()
    {
        PlayerRank = GetPref("PlayerRank");
        Rank2Upgrades = GetPref("Rank2Upgrades");
        Rank1Upgrades = GetPref("Rank1Upgrades");
        Debug.Log("PLAYERRANK = " + PlayerRank);
        if (PlayerRank > 1)
        {
            if (Rank2Upgrades < 2)
            {
                Debug.Log("Rank2 num = " + Rank2Upgrades);
                SavePref("Rank2Upgrades", (Rank2Upgrades + 1));
                Rank2Upgrades = GetPref("Rank2Upgrades");
                Debug.Log("Rank2 NEW num = " + Rank2Upgrades);
            }
        }
        else
        {
            if (Rank1Upgrades < 2)
            {
                Debug.Log("Rank1 num = " + Rank1Upgrades);
                SavePref("Rank1Upgrades", (Rank1Upgrades + 1));
                Rank1Upgrades = GetPref("Rank1Upgrades");
                Debug.Log("Rank1 NEW num = " + Rank1Upgrades);
            }
        }
    }

    // Actually buys an upgrade to the stat selected by the player :)
    public void buyStatUpgrade(string stat, int cost)
    {
        if (stat == "PWR")
        {
            PlayerPower = GetPref("PlayerPower");
            SavePref("PlayerPower", (PlayerPower + 1));
            PlayerMaxStamina = ((PlayerPower+1) * 10);
            SavePref("PlayerMaxStamina", PlayerMaxStamina);
            spendEXP(cost);
            updateRankStat();
        }
        else if (stat == "SPD")
        {
            PlayerSpeed = GetPref("PlayerSpeed");
            SavePref("PlayerSpeed", (PlayerSpeed + 1));
            spendEXP(cost);
            updateRankStat();
        }
        else if (stat == "TGH")
        {
            PlayerTough = GetPref("PlayerTough");
            SavePref("PlayerTough", (PlayerTough + 1));
            PlayerMaxHP = ((PlayerTough+1) * 100) / 2;
            SavePref("PlayerMaxHP", PlayerMaxHP);
            spendEXP(cost);
            updateRankStat();
        }
    }

    // Pre determined stat increases by ranking up (like levelling)
    // Can only rank up by defeating enemy in real (not sparring) match
    public void doStatsRankUp(int rank)
    {
        PlayerPower = GetPref("PlayerPower");
        PlayerSpeed = GetPref("PlayerSpeed");
        PlayerTough = GetPref("PlayerTough");

        if (rank == 2)
        {
            // P+1
            SavePref("PlayerPower", (PlayerPower + 1));
            PlayerPower = GetPref("PlayerPower");
        }
        if (rank == 1)
        {
            // S+1
            SavePref("PlayerSpeed", (PlayerSpeed + 1));
            PlayerSpeed = GetPref("PlayerSpeed");
        }

        PlayerMaxStamina = (PlayerPower * 10);
        SavePref("PlayerMaxStamina", PlayerMaxStamina);

        PlayerMaxHP = (PlayerTough * 100) / 2;
        SavePref("PlayerMaxHP", PlayerMaxHP);
    }

    // -----------------------------------------------------------------------//

    // Simple function using arguments to update a single playerpref value
    // must include the CORRECT playerprefs name or no work-o
    public void SavePref(string prefname, int val)
    {
        PlayerPrefs.SetInt(prefname, val);
        PlayerPrefs.Save();

        // the following is for testing
        int temp = PlayerPrefs.GetInt(prefname);
        Debug.Log("Player Pref [" + prefname + "] saved with new value of [" + temp + "].");
    }

    public void SavePrefBool(string prefname, bool TF)
    {
        if (TF) // true  = 1
        {
            SavePref(prefname, 1);
        }
        else // falso =  0
        {
            SavePref(prefname, 0);
        }
    }

    // -----------------------------------------------------------------------//

    // Return value of a specified player pref
    public int GetPref(string prefname)
    {
        int temp = PlayerPrefs.GetInt(prefname);
        return temp;
    }

    // Return value of a specified boolean player pref
    public bool GetPrefBool(string prefname)
    {
        bool temp = getAbilityTF(prefname);
        return temp;
    }

    public bool getAbilityTF(string AbilityName)
    {
        int num = PlayerPrefs.GetInt(AbilityName);
        if (num != 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // -----------------------------------------------------------------------//

    // Pulls all Saved PlayerPrefs values and outputs them to the console
    // This function is for testing
    public void ConsoleAllPlayerPrefValues()
    {
        PlayerRank = PlayerPrefs.GetInt("PlayerRank");
        PlayerMaxHP = PlayerPrefs.GetInt("PlayerMaxHP");
        PlayerMaxStamina = PlayerPrefs.GetInt("PlayerMaxStamina");
        PlayerPower = PlayerPrefs.GetInt("PlayerPower");
        PlayerSpeed = PlayerPrefs.GetInt("PlayerSpeed");
        PlayerTough = PlayerPrefs.GetInt("PlayerTough");
        PlayerEXP = PlayerPrefs.GetInt("PlayerEXP");
        TotalEXP = PlayerPrefs.GetInt("TotalEXP");

        // bool not allowed; 0 is falso / 1 is true
        //        NewAbility1 = PlayerPrefs.GetInt("NewAbility1");
        BoolAbility1 = getAbilityTF("NewAbility1");
        BoolAbility2 = getAbilityTF("NewAbility2");
        BoolSparring = getAbilityTF("SparringMatch");
        BeatGame = PlayerPrefs.GetInt("BeatGame");

        Rank2Upgrades = PlayerPrefs.GetInt("Rank2Upgrades");
        Rank1Upgrades = PlayerPrefs.GetInt("Rank1Upgrades");



        /*
                if (NewAbility1 != 1)
                    BoolAbility1 = false;
                else
                    BoolAbility1 = true;

                NewAbility2 = PlayerPrefs.GetInt("NewAbility2");
                if (NewAbility2 != 1)
                    BoolAbility2 = false;
                else
                    BoolAbility2 = true;
        */

        Debug.Log("Player Rank = " + PlayerRank);
        Debug.Log("Player Max HP = " + PlayerMaxHP);
        Debug.Log("Player Max Stamina = " + PlayerMaxStamina);
        Debug.Log("Player Power = " + PlayerPower);
        Debug.Log("Player Speed = " + PlayerSpeed);
        Debug.Log("Player Toughness = " + PlayerTough);
        Debug.Log("Player EXP = " + PlayerEXP);
        Debug.Log("Total Gained EXP = " + TotalEXP);
        Debug.Log("Gained Special Ability 1 = " + BoolAbility1);
        Debug.Log("Gained Special Ability 2 = " + BoolAbility2);
        Debug.Log("Currently Sparring = " + BoolSparring);
        Debug.Log("Rank 2 Upgrades Purchased = " + Rank2Upgrades);
        Debug.Log("Rank 1 Upgrades Purchased = " + Rank1Upgrades);
        Debug.Log("Has Beaten Game = " + BeatGame);
    }

    // -----------------------------------------------------------------------//

    /*
     * THESE FUNCTIONS WERE FOR TESTING PURPOSES :d
     */

    // this was for testing; ignore
    int score;

    public void GetScore()
    {
        PlayerPrefs.SetInt("Score", 20);
        PlayerPrefs.Save();

        score = PlayerPrefs.GetInt("Score");
        Debug.Log("This is the saved score: " + score);

        PlayerPrefs.SetInt("Score", 254);
        PlayerPrefs.Save();

        score = PlayerPrefs.GetInt("Score");
        Debug.Log("This is the saved score: " + score);

    }

    public void BasicScore()
    {
        score = PlayerPrefs.GetInt("Score");
        Debug.Log("This is the saved score: " + score);
    }

}