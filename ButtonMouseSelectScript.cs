using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Unless denoted by a commented out link, TK wrote literally everything here

// Helped me do this:
// https://gamedev.stackexchange.com/questions/116801/how-can-i-detect-that-the-mouse-is-over-a-button-so-that-i-can-display-some-ui-t
// Note to self, another way to do this:
// https://docs.unity3d.com/ScriptReference/EventSystems.EventTrigger.html

public class ButtonMouseSelectScript : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // Select a button when hovering over it with mouse
        Button btn = GetComponent<Button>();
        btn.Select();
    }
}






/* This does not work on UI eleements:
void OnMouseOver()
{
    //If your mouse hovers over the GameObject with the script attached, output this message
    Debug.Log("Mouse is over GameObject.");
}

void OnMouseExit()
{
    //The mouse is no longer hovering over the GameObject so output this message each frame
    Debug.Log("Mouse is no longer on GameObject.");
}
*/

