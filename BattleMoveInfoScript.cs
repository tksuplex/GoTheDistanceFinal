using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// https://docs.unity3d.com/ScriptReference/UI.Selectable.OnSelect.html

// This script shows a text description for player move selections in battle

public class BattleMoveInfoScript : MonoBehaviour, ISelectHandler
{
    public MoveSetScript moveSet;
    public Text moveText;

    public Button jab;
    public Button straight;
    public Button hook;
    public Button sunday;

    public Button counter;
    public Button guard;
    public Button recover;
    public Button butterbee;

    public string msg = " ";

    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        int cost = 0;
        Button btn = GetComponent<Button>();

        msg = " ";

        if (btn == jab)
        {
            cost = moveSet.getMoveSetCost(1);
            msg = "Jab (" + cost.ToString() + "SP)\n\n";
            msg += "A weak attack that has no SP cost, but recovers some SP.";
        }
        else if (btn == straight)
        {
            cost = moveSet.getMoveSetCost(2);
            msg = "Straight (" + cost.ToString() + "SP)\n\n";
            msg += "A light attack that recovers a small amount of HP.";
        }
        else if (btn == hook)
        {
            cost = moveSet.getMoveSetCost(3);
            msg = "Hook (" + cost.ToString() + "SP)\n\n";
            msg += "A heavy attack with a small chance to daze the opponent.";
        }
        else if (btn == sunday)
        {
            cost = moveSet.getMoveSetCost(4);
            msg = "Sunday Punch (" + cost.ToString() + "SP)\n\n";
            msg += "A heavy attack with a larger chance to daze the opponent.";
        }
        else if (btn == counter)
        {
            cost = moveSet.getMoveSetCost(0);
            msg = "Counter! (" + cost.ToString() + "SP)\n\n";
            msg += "A medium attack that recovers some SP and some HP.";
        }
        else if (btn == guard)
        {
            cost = moveSet.getMoveSetCost(6);
            msg = "Guard (" + cost.ToString() + "SP)\n\n";
            msg += "A defensive move that recovers a large amount of SP and halves damaged recieved until next turn.";
        }
        else if (btn == recover)
        {
            cost = moveSet.getMoveSetCost(7);
            msg = "Recover (" + cost.ToString() + "SP)\n\n";
            msg += "A defensive move that recovers some HP.";
        }
        else if (btn == butterbee)
        {
            cost = moveSet.getMoveSetCost(8);
            msg = "ButterBee (" + cost.ToString() + "SP)\n\n";
            msg += "A light attack that has a medium chance to attack a second time with a Counter.";
        }

        // Update textbox with move information
        moveText.text = msg;
    }
}
