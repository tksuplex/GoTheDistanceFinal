using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unless denoted by a commented out link, TK wrote literally everything here

public class MatchTurnEnemy : MonoBehaviour
{
    public DecisionTree tree;
    public MatchTurn turn;
    public EnemyObject enemy;
    public PlayerObject player;
    public MoveSetScript moveSet;
    public Animations animate;

    public bool currentGuard;
    public bool prevGuard;

    public void doEnemyTurn()
    {
//        Debug.Log("You are inside Enemy turn!");

        int moveNum = -1;
        enemy.turnCounter++;

        // If previously guarding, stop
        if (enemy.Guard && enemy.prevMove == 6)
        {
            animate.EnemyTurnOffGuard();
        }
        enemy.Guard = false;

        // this function implements AI to decide which returns which move to take
        moveNum = tree.runEnemyAI(player, enemy, turn, moveSet);
        Debug.Log("returned enemy AI ==== " + moveNum);

        currentGuard = (moveNum == 6) ? true : false;

        // Hold the turn until the animations are over.
        StartCoroutine(RunEnemyTurnAnimations(moveNum));
    }

    IEnumerator RunEnemyTurnAnimations(int moveNum)
    {
        // If was in guarding stance, and not guarding again, return to idle stance animation
        if (enemy.prevMove == 6)
        {
            yield return new WaitForSeconds(0.2f);
        }

        // Implement move selected by Decision Tree
        moveSet.doEnemyMove(moveNum);

        // If Dazed this turn set to false for next turn
        if (moveNum == 10)
        {
            enemy.Dazed = false;
        }

        float timeWait;
        if (moveNum == 10)
        {
            timeWait = 1.1f;
        }
        else if (moveNum == 6)
        {
            timeWait = 0.4f;
        }
        else
        {
            timeWait = 0.8f;
        }

        // Run regular move animations

        animate.EnemySpriteAnimation(moveNum);
        yield return new WaitForSeconds(timeWait);

        checkEndTurn();
    }

    public void checkEndTurn()
    {
        if (player.HP <= 0)
        {
            // set MatchEnd = true;
            turn.MatchEnd = true;
            turn.PlayerTurn = false;
            turn.EnemyTurn = false;
            turn.PlayerKO = true;
        }
        else if (enemy.HP <= 0)
        {
            // set MatchEnd = true;
            turn.MatchEnd = true;
            turn.PlayerTurn = false;
            turn.EnemyTurn = false;
            turn.EnemyKO = true;
        }
        else
        {
            // yield turn
            yieldEnemyTurn();
        }
    }

    public void yieldEnemyTurn()
    {
        turn.EnemyTurn = false;
    }

}
