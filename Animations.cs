using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Unless denoted by a commented out link, TK wrote literally everything here

public class Animations : MonoBehaviour
{
    // So many animators... =____=;
    public Animator PlayerAnimator;
    public Animator PlayerVFX;
    public Animator PlayerHPText;
    public Text pHP;
    public Text pSP;

    public Animator EnemyAnimator;
    public Animator EnemyVFX;
    public Animator EnemyHPText;
    public Text eHP;
    public Text eSP;

    public Animator CamAnimator;

    /*
     * Player/Enemy Triggers:
     * guard, idle, jab, straight, hook, upper, hit, evade
     * 
     * Player/Enemy VFXTriggers:
     * recover, missed, dazed
     * 
     * Player/Enemy Text Triggres:
     * hpred, hpgreen
     * 
     * Camera Triggers:
     * playerhit, enemyhit
     */

    private void Start()
    {
        pHP.text = " ";
        pSP.text = " ";
        eHP.text = " ";
        eSP.text = " ";
    }
    // ------------------------------------------------------------------------------------------------- //

    public void PlayerTurnOffGuard()
    {
        PlayerAnimator.SetTrigger("idle");
    }

    public void EnemyTurnOffGuard()
    {
        EnemyAnimator.SetTrigger("idle");
    }

    // ------------------------------------------------------------------------------------------------- //

    public void PlayerSpriteAnimation(int attemptMove, int moveNum)
    {
        string enemyTrigger = "NOTHING";
        string playerTrigger = "NOTHING";
        string enemyVFXTrigger = "NOTHING";
        string playerVFXTrigger = "NOTHING";


        if (attemptMove == 0 || attemptMove == 11)
        {
            // Set random to straight, hook, upper
            if (Random.Range(0.0f, 1.0f) < 0.33f)
            {
                playerTrigger = "straight";
            }
            else if (Random.Range(0.0f, 1.0f) < 0.66f)
            {
                playerTrigger = "hook";
            }
            else
            {
                playerTrigger = "upper";
            }

            enemyTrigger = "hit";
        }
        else if (attemptMove == 1)
        {
            playerTrigger = "jab";
            enemyTrigger = "hit";
        }
        else if (attemptMove == 2 || attemptMove == 8)
        {
            playerTrigger = "straight";
            enemyTrigger = "hit";
        }
        else if (attemptMove == 3)
        {
            playerTrigger = "hook";
            enemyTrigger = "hit";
        }
        else if (attemptMove == 4 || attemptMove == 5)
        {
            playerTrigger = "upper";
            enemyTrigger = "hit";
        }
        else if (attemptMove == 6)
        {
            playerTrigger = "guard";
        }
        else if (attemptMove == 7)
        {
            playerVFXTrigger = "recover";
        }
        else if (attemptMove == 10)
        {
            playerVFXTrigger = "dazed";
        }

        // Despite player selection, if move missed an evade animation should play
        if (moveNum == 9)
        {
            enemyTrigger = "evade";
            enemyVFXTrigger = "missed";
        }

        // Trigger animations based on set triggers
        if (playerTrigger != "NOTHING")
        {
            PlayerAnimator.SetTrigger(playerTrigger);
        }
        if (playerVFXTrigger != "NOTHING")
        {
            PlayerVFX.SetTrigger(playerVFXTrigger);
        }
        if (enemyTrigger != "NOTHING")
        {
            EnemyAnimator.SetTrigger(enemyTrigger);
            if (enemyTrigger == "hit")
            {
                CamAnimator.SetTrigger("enemyhit");
            }
        }
        if (enemyVFXTrigger != "NOTHING")
        {
            EnemyVFX.SetTrigger(enemyVFXTrigger);
        }
    }

    // ------------------------------------------------------------------------------------------------- //

    public void EnemySpriteAnimation(int moveNum)
    {
        string enemyTrigger = "NOTHING";
        string playerTrigger = "NOTHING";
        string enemyVFXTrigger = "NOTHING";
        string playerVFXTrigger = "NOTHING";

        if (moveNum == 0 || moveNum == 9 || moveNum == 11)
        {
            // Player avoids play evade animation, else hit animation
            if (moveNum == 9)
            {
                playerTrigger = "evade";
                playerVFXTrigger = "missed";
            }
            else
            {
                playerTrigger = "hit";
            }

            // Set random to straight, hook, upper
            if (Random.Range(0.0f, 1.0f) < 0.33f)
            {
                enemyTrigger = "straight";
            }
            else if (Random.Range(0.0f, 1.0f) < 0.66f)
            {
                enemyTrigger = "hook";
            }
            else
            {
                enemyTrigger = "upper";
            }
        }
        else if (moveNum > -1 && moveNum <= 5)
        {
            playerTrigger = "hit";
            if (moveNum == 1)
            {
                enemyTrigger = "jab";
            }
            else if (moveNum == 2)
            {
                enemyTrigger = "straight";
            }
            else if (moveNum == 3)
            {
                enemyTrigger = "hook";
            }
            else if (moveNum == 4)
            {
                enemyTrigger = "upper";
            }
            else if (moveNum == 5)
            {
                enemyTrigger = "upper";
            }
        }
        else if (moveNum == 6)
        {
            enemyTrigger = "guard";
        }
        else if (moveNum == 7)
        {
            enemyVFXTrigger = "recover";
        }
        else if (moveNum == 8)
        {
            enemyTrigger = "straight";
            playerTrigger = "hit";
        }
        else if (moveNum == 10)
        {
            enemyVFXTrigger = "dazed";
        }

        // Trigger animations based on set triggers
        if (playerTrigger != "NOTHING")
        {
            PlayerAnimator.SetTrigger(playerTrigger);
            if (playerTrigger == "hit")
            {
                CamAnimator.SetTrigger("playerhit");
            }
        }
        if (playerVFXTrigger != "NOTHING")
        {
            PlayerVFX.SetTrigger(playerVFXTrigger);
        }
        if (enemyTrigger != "NOTHING")
        {
            EnemyAnimator.SetTrigger(enemyTrigger);
        }
        if (enemyVFXTrigger != "NOTHING")
        {
            EnemyVFX.SetTrigger(enemyVFXTrigger);
        }
    }

    // ------------------------------------------------------------------------------------------------- //

    public void playerTextRoll(int hp, int sp)
    {
        // Zero out text
        pHP.text = " ";
        pSP.text = " ";
        eHP.text = " ";
        eSP.text = " ";

        playerSPText(sp);
        if (hp == 0 && sp != 0)
        {
            // trigger animation with either keyword
            PlayerHPText.SetTrigger("hpred");
        }
        else
        {
            playerHPText(hp);
        }
    }

    public void playerHPText(int hp)
    {
        string msg = " ";
        string hpTrigger = "NONE";

        if (hp < 0)
        {
            // if hp is negative, send "-XHP" to textbox and trigger red animation
            msg = "" + hp.ToString() + "HP";
            hpTrigger = "hpred";
        }
        else if (hp > 0)
        {
            // else send "+XHP" to textbox and trigger green animation
            msg = "+" + hp.ToString() + "HP";
            hpTrigger = "hpgreen";
        }
        pHP.text = msg;

        if (hpTrigger != "NONE")
        {
            // trigger the animation using hpTrigger trigger
            PlayerHPText.SetTrigger(hpTrigger);
        }

    }

    public void playerSPText(int sp)
    {
        string msg = " ";

        // send +XSP to textbox
        if (sp > 0)
        {
            msg = "+" + sp.ToString() + "SP";
        }
        pSP.text = msg;
    }

    // ------------------------------------------------------------------------------------------------- //

    public void enemyTextRoll(int hp, int sp)
    {
        // Zero out text
        pHP.text = " ";
        pSP.text = " ";
        eHP.text = " ";
        eSP.text = " ";

        enemySPText(sp);
        if (hp == 0 && sp != 0)
        {
            // trigger animation with either keyword
            EnemyHPText.SetTrigger("hpred");
        }
        else
        {
            enemyHPText(hp);
        }
    }


    public void enemyHPText(int hp)
    {
        string msg = " ";
        string hpTrigger = "NONE";

        if (hp < 0)
        {
            // if hp is negative, send "-XHP" to textbox and trigger red animation
            msg = "" + hp.ToString() + "HP";
            hpTrigger = "hpred";
        }
        else if (hp > 0)
        {
            // else send "+XHP" to textbox and trigger green animation
            msg = "+" + hp.ToString() + "HP";
            hpTrigger = "hpgreen";
        }
        eHP.text = msg;

        if (hpTrigger != "NONE")
        {
            // trigger the animation using hpTrigger trigger
            EnemyHPText.SetTrigger(hpTrigger);
        }
    }

    public void enemySPText(int sp)
    {
        string msg = " ";

        // send +XSP to textbox
        if (sp > 0)
        {
            msg = "+" + sp.ToString() + "SP";
        }
        eSP.text = msg;
    }

    // ------------------------------------------------------------------------------------------------- //

}
