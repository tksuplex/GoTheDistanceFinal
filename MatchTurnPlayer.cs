using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// Unless denoted by a commented out link, TK wrote literally everything here

public class MatchTurnPlayer : MonoBehaviour
{
    public MatchTurn turn;
    public PlayerObject player;
    public EnemyObject enemy;
    public MoveSetScript moveset;
    public BattleTextScript btext;
    public Animations animate;

    // Certain player buttons will be disabled if not player turn
    // or player does not have that ability
    public GameObject standardAbilButton;
    public GameObject counterButton;
    public GameObject jabButton;
    public GameObject sundayButton;
    public GameObject butterButton;

    public bool prevGuard;

    public void Start()
    {
        standardAbilButton.SetActive(false);
        counterButton.SetActive(false);
        sundayButton.SetActive(false);
        butterButton.SetActive(false);

    }

    // ------------------------------------------------------------------------------------------------- //

    public void doPlayerTurn()
    {
//        Debug.Log("You are inside Player's turn!");

        // If player was defending last turn; turn it off
        prevGuard = player.Guard;
        player.Guard = false;
        // If was in guarding stance, and not guarding again, return to idle stance animation
        if (prevGuard)
        {
            animate.PlayerTurnOffGuard();
        }

        // If dazed, move is dazed and player doesn't get a turn
        if (player.Dazed)
        {
            playerSelect(10);
            player.Dazed = false;
        }
        else // Set up for player selection
        {
            // Start by enabling all regular buttons
            standardAbilButton.SetActive(true);

            // if counter, enable & select counter button; else select jab
            if (enemy.prevMove == 9)
            {
                counterButton.SetActive(true);
                counterButton.GetComponent<Button>().Select();
            }
            else
            {
                counterButton.SetActive(false);
                jabButton.GetComponent<Button>().Select();
            }

            // if have 1st ability, enable sunday p
            if (player.SundayPunch)
            {
                sundayButton.SetActive(true);
            }
            // if have 2nd ability, enable butterbee
            if (player.ButterBee)
            {
                butterButton.SetActive(true);
            }
        }
    }

    // ------------------------------------------------------------------------------------------------- //

    public void playerSelect(int moveNum)
    {
        bool PlayerMiss;
        int attemptMove = moveNum;

        // Reset error text on make selection
        btext.updateErrorText(" ");

        // Get miss chance, can't miss on Counter
        if (enemy.prevMove == 9 && moveNum == 0)
        {
            PlayerMiss = false;
        }
        else if (moveNum == 1 || moveNum == 6 || moveNum == 7 || moveNum == 10 || moveNum == 11)
        {
            PlayerMiss = false;
        }
        else
        {
            // Get if player misses or not
            PlayerMiss = (Random.Range(0.0f, 1.0f) < enemy.EVA) ? true : false;
        }

        if (PlayerMiss)
        {
            moveNum = 9; // Change move to miss
        }

        // Stop selection if player does not have enough SP
        if (moveset.getMoveSetCost(moveNum) > player.SP)
        {
            Debug.Log("you cannot afford this move w/current SP!");
            btext.updateErrorText("NOT ENOUGH SP!");
        }
        else // continue with player selection
        {
            // Deselect button
            EventSystem.current.SetSelectedGameObject(null);

            // Disable player Buttons
            standardAbilButton.SetActive(false);
            counterButton.SetActive(false);
            sundayButton.SetActive(false);
            butterButton.SetActive(false);

            // DOES THE SELECTED ACTION WOO
            moveset.doMove(moveNum);

            // Hold the turn until the animations are over.
            StartCoroutine(RunPlayerTurnAnimations(attemptMove, moveNum));
        }
    }

    // ------------------------------------------------------------------------------------------------- //

    IEnumerator RunPlayerTurnAnimations(int attemptMove, int moveNum)
    {
        float timeWait;
        if (moveNum == 10)
        {
            timeWait = 1.5f;
        }
        else
        {
            timeWait = 1.0f;
        }

            // Run regular move animations
            animate.PlayerSpriteAnimation(attemptMove, moveNum);
            yield return new WaitForSeconds(timeWait);

            if (moveNum == 8) // If ButterBee, chance for a second attack
            {
                bool bee = (Random.Range(0.0f, 1.0f) < (player.LCK*3.5f)) ? true : false;
                if (bee)
                {
                    playerSelect(0);
                }
                else
                {
                    checkEndTurn();
                }
            }
            else
            {
                checkEndTurn();
            }

        //  EnemySpriteAnimation(int moveNum)
    }


    public void checkEndTurn()
    {
        if (player.HP <= 0)
        {
            // Run game over animatio

            // set MatchEnd = true;
            turn.MatchEnd = true;
            turn.PlayerTurn = false;
            turn.EnemyTurn = false;
            turn.PlayerKO = true;
        }
        else if (enemy.HP <= 0)
        {
            // Run finisher animation for player

            // set MatchEnd = true;
            turn.MatchEnd = true;
            turn.PlayerTurn = false;
            turn.EnemyTurn = false;
            turn.EnemyKO = true;
        }
        else
        {
            // Yield turn to enemy
            yieldPlayerTurn();
        }
    }

    // ------------------------------------------------------------------------------------------------- //

    public void yieldPlayerTurn()
    {
        turn.PlayerTurn = false;
    }

    // ------------------------------------------------------------------------------------------------- //

    //  THESE ARE TEST FUNCTIONS;
    public void setPlayerHP(int hp)
    {
        player.HP = hp;
    }

    public void setEnemyHP(int hp)
    {
        enemy.HP = hp;
    }

}
