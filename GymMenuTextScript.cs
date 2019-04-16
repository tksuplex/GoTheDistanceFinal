using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// https://docs.unity3d.com/ScriptReference/UI.Selectable.OnSelect.html

// This script shows a text description for the button selections in Gym Menu

public class GymMenuTextScript : MonoBehaviour, ISelectHandler
{
    public Text gymText;
    public Button sparring;
    public Button training;
    public Button match;
    public Button exit;
    public string msg = " ";

    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
//        Debug.Log(this.gameObject.name + " was selected");
        Button btn = GetComponent<Button>();

        msg = " ";

        if (btn == sparring)
        {
            msg = "Practice your abilities and gain some EXP by having a sparring match.";
        }
        else if (btn == training)
        {
            msg = "Spend your accumulated EXP on stat increases or buying new abilities.";
        }
        else if (btn == match)
        {
            msg = "Proceed to your next ranked match; winning increases your rank, losing results in a game over.";
        }
        else if (btn == exit)
        {
            msg = "Exit the game and return to the main menu.";
        }
        gymText.text = msg;

    }


}
