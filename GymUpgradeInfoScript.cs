using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// https://docs.unity3d.com/ScriptReference/UI.Selectable.OnSelect.html

// This script shows a text description for all options in Upgrade Menu

public class GymUpgradeInfoScript : MonoBehaviour, ISelectHandler
{
    public SavePrefs prefs;
    public MoveSetScript moveSet;

    public Text infoText;
    public Button upgrade;
    public Button ability;
    public Button exitmenu;

    public Button power;
    public Button speed;
    public Button tough;
    public Button exitupgrade;

    public Button sunday;
    public Button butterbee;
    public Button exitability;


    public string msg = " ";


    public void OnSelect(BaseEventData eventData)
    {
        Button btn = GetComponent<Button>();
        int rank = PlayerPrefs.GetInt("PlayerRank");
        int cost;
        int moveCost = 0;
        if (rank > 1)
        {
            cost = 1000;
        }
        else
        {
            cost = 2000;
        }


        msg = " ";

        if (btn == exitmenu)
        {
            msg = "\n\n";
            msg += "Exit the Training Menu and return to the Game Menu.";
        }
        else if (btn == upgrade)
        {
            msg = "\n\n";
            msg += "There are stat UPGRADES available; spend EXP to buy upgrades and increase your stats to become more powerful in battle.";
        }
        else if (btn == ability)
        {
            msg = "\n\n";
            msg += "There are NEW ABILITIES available; spend EXP to learn new abilities to use in battle.";
        }
        else if (btn == power)
        {
            msg = "\n\n";
            msg += "Power (PWR)\nCost: " + cost.ToString() + "EXP\n";
            msg += "The PWR stat increases how much DAMAGE your attacks do, as well as increasing your maximum Stamina Points (SP).";
        }
        else if (btn == speed)
        {
            msg = "\n\n";
            msg += "Speed (SPD)\nCost: " + cost.ToString() + "EXP\n";
            msg += "The SPD stat increases the % chance you will EVADE an enemy attack. ";
            msg += "Certain abilities have features such as a chance to Daze the enemy or perform a double attack, SPD stat increases your chance %.";
        }
        else if (btn == tough)
        {
            msg = "\n\n";
            msg += "Toughness (TGH)\nCost: " + cost.ToString() + "EXP\n";
            msg += "The TGH stat increases how much you DEFENSE can mitigate opponents attack, as well as increas your maximum Hit Points (HP).";
        }
        else if (btn == exitupgrade)
        {
            msg = "\n\n";
            msg += "Done with UPGRADES.";
        }
        else if (btn == sunday)
        {
            moveCost = moveSet.getMoveSetCost(4);
            msg = "\n\n";
            msg += "Sunday Punch\nCost: " + cost.ToString() + "EXP\n";
            msg += "A powerful attack costing " + moveCost.ToString() + "SP, that has a small chance to inflict DAZED status on opponent, preventing opponent from attacking for 1 turn.";
        }
        else if (btn == butterbee)
        {
            moveCost = moveSet.getMoveSetCost(8);
            msg = "\n\n";
            msg += "ButterBee\nCost: " + cost.ToString() + "EXP\n";
            msg += "\"Fly like a butterfly...\" you know the rest! A weaker attack costing " + moveCost.ToString() + "SP, that has a medium chance of also performing a second attack on the opponent.";
        }
        else if (btn == exitability)
        {
            msg = "\n\n";
            msg += "Done with ABILITIES.";
        }

        // Update textbox with move information
        infoText.text = msg;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
