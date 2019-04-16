using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTextScript : MonoBehaviour
{
    public GameObject playerText;
    public GameObject enemyText;
    public GameObject errorText;
    public PlayerObject player;
    public EnemyObject enemy;

    string playerTextString()
    {
        string txt = "";
        txt += player.PlayerRank + "\n";
        txt += player.PWR + "\n";
        txt += player.SPD + "\n";
        txt += player.TGH + "\n\n";
        txt += player.HP + "/" + player.MaxHP + "\n";
        txt += player.SP + "/" + player.MaxSP + "\n";

        /*
                string txt = "HP: " + player.HP + "/" + player.MaxHP + "\n";
                txt += "SP: " + player.SP + "/" + player.MaxSP + "\n";
                txt += "DMG%: " + player.PWR * 10 + "\n";
                txt += "DEF%: " + (int)(player.DEF * 100.0) + "\n";
                txt += "EVA%: " + (int)(player.EVA * 100.0) + "\n";
                txt += "REC%: " + (int)(player.REC * 100.0) + "\n";
                txt += "LCK%: " + (int)(player.LCK * 100.0) + "\n";

                txt += "PLAYER VALUES: \n";
                txt += "PWR: " + player.PWR + "\n";
                txt += "SPD: " + player.SPD + "\n";
                txt += "TGH: " + player.TGH + "\n";
                txt += "GUARDING: " + player.Guard + "\n";
        */

        return txt;
    }

    string enemyTextString()
    {
        string txt = "";
        if (player.PlayerRank == 1)
        {
            txt += "C\n";
        }
        else
        {
            txt += (player.PlayerRank - 1).ToString() + "\n";
        }
        txt += enemy.PWR + "\n";
        txt += enemy.SPD + "\n";
        txt += enemy.TGH + "\n";



/*
 *         string txt = "ENEMY VALUES: \n";
        txt += "HP: " + enemy.HP + "/" + enemy.MaxHP + "\n";
        txt += "SP: " + enemy.SP + "/" + enemy.MaxSP + "\n";
        txt += "PWR: " + enemy.PWR + "\n";
        txt += "SPD: " + enemy.SPD + "\n";
        txt += "TGH: " + enemy.TGH + "\n";
        txt += "DMG%: " + enemy.PWR * 10 + "\n";
        txt += "DEF%: " + (int)(enemy.DEF * 100) + "\n";
        txt += "EVA%: " + (int)(enemy.EVA * 100) + "\n";
        txt += "LCK%: " + (int)(enemy.LCK * 100) + "\n";
        txt += "REC%: " + (int)(enemy.REC * 100) + "\n";
        txt += "GUARDING: " + enemy.Guard + "\n";
*/


        return txt;
    }

    public void updateErrorText(string s)
    {
        errorText.GetComponent<Text>().text = s;
    }

    // Update is called once per frame
    void Update()
    {
        playerText.GetComponent<Text>().text = playerTextString();
        enemyText.GetComponent<Text>().text = enemyTextString();


    }
}
