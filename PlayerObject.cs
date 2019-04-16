using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// Unless denoted by a commented out link, TK wrote literally everything here

public class PlayerObject : MonoBehaviour
{
    public SavePrefs prefs;
    public MoveSetScript moveSet;
    public Image HPbar;
    public Image SPbar;


    [HideInInspector]
    public int PlayerRank, MaxHP, HP, MaxSP, SP, PWR, SPD, TGH;

    // DEF = % of enemy attack damage can be mitigated by player
    // DMG = % of attacks base damage can be used by player
    // EVA = % chance of evading enemy attack
    // LCK = % chance of a critical attack (DMG*1.5)
    // REC = % of HP/SP recovered when using Recovery Move
    // CounterChance = % chance of generating a counter
    [HideInInspector]
    public float HPpercent, SPpercent, DMG, DEF, EVA, LCK, REC, CounterChance;

    // Special abilities player can learn by ranking up & spending EXP
    public bool SundayPunch;
    public bool ButterBee;
    public bool DynamiteBlow;

    public bool Guard;
    public bool KO;
    public bool Dazed;
    public bool Sparring;

    // I might have to change/move these idk...
    public bool Evasion;
    public bool Miss;

    // Stores player's previous moves including things like missing the enemy;
    public int prevMove;
    public int turnCounter;

    // Start is called before the first frame update
    void Start()
    {
        // Call function to populate Player Object with correct values
        populatePlayerObject();
//        displayPlayerStats();
    }

    public void populatePlayerObject()
    {
        PlayerRank = prefs.GetPref("PlayerRank");
        PWR = prefs.GetPref("PlayerPower");
        SPD = prefs.GetPref("PlayerSpeed");
        TGH = prefs.GetPref("PlayerTough");
        SundayPunch = prefs.GetPrefBool("NewAbility1");
        ButterBee = prefs.GetPrefBool("NewAbility2");
        DynamiteBlow = false;
        Sparring = prefs.GetPrefBool("Sparring");
        Dazed = false;
        turnCounter = 0;
        prevMove = -1; // Previous move doesn't exist because just started

        populateDerivedValues();
    }

    public void populateDerivedValues()
    {
        MaxSP = (int)(PWR * 20);
        SP = MaxSP;
        MaxHP = (int)((TGH * 100) / 2);
        HP = MaxHP;

        DMG = ((float)PWR * 10) / 100;
        DEF = ((float)TGH * 10) / 200;
        EVA = ((float)SPD * 10) / 270;
        LCK = ((float)PWR + TGH + (2 * SPD)) / 200;
        REC = (float)SPD / 180;
    }

    void displayPlayerStats()
    {
        Debug.Log("Player Values:");
        consoleStats();
    }

    public void consoleStats()
    {
        Debug.Log("HP: " + MaxHP);
        Debug.Log("SP: " + MaxSP);
        Debug.Log("PWR: " + PWR);
        Debug.Log("SPD: " + SPD);
        Debug.Log("TGH: " + TGH);
        Debug.Log("DMG %: " + DMG);
        Debug.Log("DEF %: " + DMG);
        Debug.Log("EVA %: " + EVA);
        Debug.Log("LCK %: " + LCK);
        Debug.Log("REC %: " + REC);
        Debug.Log("Counter Chance %: " + CounterChance);

        Debug.Log("Sunday: " + SundayPunch);
        Debug.Log("ButterBee: " + ButterBee);
        Debug.Log("Dynamite: " + DynamiteBlow);
    }

    public void updatePlayerBar()
    {
        HPpercent = (float)HP / MaxHP;
        SPpercent = (float)SP / MaxSP;

        HPbar.fillAmount = HPpercent;
        SPbar.fillAmount = SPpercent;
    }

// Update is called once per frame
void Update()
    {
        // DELETE!
//        Debug.Log("Player HP: " + HP + "/" + MaxHP);
       
        
    }
}
