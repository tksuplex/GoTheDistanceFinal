using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * this controls every game turn (including player and enemy turns which have
 * their own scripts
 */

// Unless denoted by a commented out link, TK wrote literally everything here



public class MatchTurn : MonoBehaviour
{
    public MatchTurnPlayer pTurnScript;
    public MatchTurnEnemy eTurnScript;
    public SceneChanger scene;


    public bool PlayerTurn, EnemyTurn, MatchEnd, StartedCleanup, PlayerKO, EnemyKO;
    public int CurrentTurn;
    public int NextTurn;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTurn = 0;
        NextTurn = 1;
        PlayerTurn = true;
        EnemyTurn = false;
        MatchEnd = false;
        StartedCleanup = false;
        PlayerKO = false;
        EnemyKO = false;
    }

    public void startMatchTurn()
    {
        CurrentTurn++;
    }


    // Update is called once per frame
    void Update()
    {
        if (MatchEnd)
        {
            if (!StartedCleanup && !EnemyTurn && !PlayerTurn)
            {
                Debug.Log("THE MATCH HAS ENDED!");
                StartedCleanup = true;
                NextTurn = CurrentTurn;

                if (PlayerKO)
                {
                    Debug.Log("PLAYER DEFEATED --- MATCH LOST");
                    scene.masterSceneFadeOut(1);
                }
                else
                {
                    Debug.Log("ENEMY DEFEATED --- MATCH WON!");
                    scene.masterSceneFadeOut(7);
                }
            }
        }
        else // !MatchEnd
        {
            if (CurrentTurn < NextTurn) // Start turn (with player turn)
            {
                if (!EnemyTurn)
                {
                    CurrentTurn++;
                    PlayerTurn = true;

                    // sets up player turn for player to make selection
                    pTurnScript.doPlayerTurn();
                }
            }
            else if (!PlayerTurn)
            {
                // Enemy turn

                if (!EnemyTurn)
                {
                    EnemyTurn = true;

                    // Call Enemy Turn
                    eTurnScript.doEnemyTurn();

                    // If the match is not over, allow next turn to start.
                    if (!MatchEnd)
                    {
                        NextTurn++;
                    }
                }
            }
        }
    }
}
