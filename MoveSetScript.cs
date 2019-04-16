using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// Unless denoted by a commented out link, TK wrote literally everything here

public class MoveSetScript : MonoBehaviour
{
    public EnemyObject enemy;
    public PlayerObject player;
    public MatchTurn turn;
    public Animations animate;

    int[] moveSetDMG = new int[12];
    int[] moveSetCost = new int[12];


    // -----------------------------------------------------------------------//

    /*
     * LIST OF MOVES POSSIBLE:
     * 0 Counter
     * 1 Jab
     * 2 Straight / Light Attack
     * 3 Hook / Heavy Attack
     * 4 Sunday Punch / Crit chance
     * 5 Dynamite Blow / Dazes enemy
     * 6 Guard / Halves Damage 1 turn
     * 7 Recover / +% of HP & SP
     * 8 ButterBee / +1 EVA for X turns
     * 9 Miss / Missed Oponent
     * 10 Dazed / Dazed by enemy cannot attack
     * 11 FINISHER / Finishing Move; target critical condition
     */

    // -----------------------------------------------------------------------//

    // Start is called before the first frame update
    void Start()
    {
        setUpMoveSet();
    }

    public void setUpMoveSet()
    {
        moveSetDMG[0] = 30; // counter base damage
        moveSetDMG[1] = 10; // jab
        moveSetDMG[2] = 20; // straight / light
        moveSetDMG[3] = 40; // hook / heavy
        moveSetDMG[4] = 45; // sunday
        moveSetDMG[5] = 40; // dynamite
        moveSetDMG[6] = 0; // guard
        moveSetDMG[7] = 0; // recover
        moveSetDMG[8] = 20; // butterbee
        moveSetDMG[9] = 0; // miss
        moveSetDMG[10] = 0; // dazed
        moveSetDMG[11] = 40; // FINISHING MOVE

        moveSetCost[0] = 5; // counter
        moveSetCost[1] = 0; // jab
        moveSetCost[2] = 5; // straight /light
        moveSetCost[3] = 10; // hook / heavy
        moveSetCost[4] = 15; // sunday
        moveSetCost[5] = 20; // dynamite
        moveSetCost[6] = 0; // guard
        moveSetCost[7] = 10; // recover
        moveSetCost[8] = 15; // butterbee
        moveSetCost[9] = 0; // miss
        moveSetCost[10] = 0; // dazed
        moveSetCost[11] = 25; // FINISHING MOVE

    }

    public void doMove(int moveNum)
    {
        // check for valid moveNum 
        if (moveNum < 0 || moveNum > 11)
        {
            Debug.Log("INVALID MOVE NUMBER, EXITING.");
            return;
        }

        // DELETE/FIX THIS LATER....
//        animate.testAnimation("jab","hit");

        int costSP = getMoveSetCost(moveNum);
        int recSP = getSPrecover(moveNum);
        int recHP = getHPrecover(moveNum);

        int baseDMG = getMoveBaseDMG(moveNum);
        int attackerDMG = getAdjustedDMG(baseDMG);
        bool guard = getGuard(); // gets if the one recieving move has guard up
        bool dazeEnemy = getDazeChance(moveNum);

        // If last move was Dazed, clear it now
        if (player.prevMove == 10)
        {
            player.Dazed = false;
        }

        // Halve damage of target is guarding
        if (guard)
        {
            attackerDMG = (int)(attackerDMG / 2);
        }

        // If you dazed enemy, set their status dazed = true
        if (dazeEnemy)
        {
            enemy.Dazed = true;
        }

        // If doing Guard move, set guard status = true
        if (moveNum == 6)
        {
            player.Guard = true;
        }

        // Update enemy HP with damage done, without going into negative
        if (enemy.HP > attackerDMG)
        {
            enemy.HP -= attackerDMG;
        }
        else
        {
            enemy.HP = 0;
        }

        // Should not be able to spend SP you don't have (prevented elsewhere)
        player.SP -= costSP;

        // Get the SP amount recovered this turn & update SP
        if (player.SP+recSP > player.MaxSP)
        {
//            Debug.Log("the SP rec is : " + (player.MaxSP - player.SP));
            recSP = (player.MaxSP - player.SP);
            player.SP = player.MaxSP;
        }
        else
        {
            player.SP += recSP;
 //           Debug.Log("the SP rec is : " + recSP);
        }

        // Get the HP amount recovered this turn & update HP
        if (player.HP + recHP > player.MaxHP)
        {
//            Debug.Log("the HP rec is : " + (player.MaxHP - player.HP));
            recHP = (player.MaxHP - player.HP);
            player.HP = player.MaxHP;
        }
        else
        {
            player.HP += recHP;
//            Debug.Log("the HP rec is : " + recHP);
        }

        animate.playerTextRoll(recHP, recSP);
//        animate.playerHPText(recHP);
//        animate.playerSPText(recSP);
        animate.enemyHPText(-attackerDMG);

        // update HP/SP bars
        player.updatePlayerBar();
        enemy.updateEnemyBar();


        // UPDATE prevMove with the move that happened (number) (could be a miss or counter)
        player.prevMove = moveNum;
        Debug.Log("PLAYER MOVE NUMBER ==== " + moveNum);

    }

    // ------------------------------------------------------------------------------------------------- //

    public bool getGuard()
    {
        if (turn.PlayerTurn)
        {
            return (enemy.Guard);
        }
        else
        {
            return (player.Guard);
        }
    }

    // ------------------------------------------------------------------------------------------------- //

    public bool getDazeChance(int moveNum)
    {
        if (moveNum == 0 || moveNum == 3 || moveNum == 4 || moveNum == 5)
        {
            bool DazeChance;
            if (turn.PlayerTurn)
            {
                DazeChance = (UnityEngine.Random.Range(0.0f, 1.0f) < (player.LCK + 0.03f)) ? true : false;
                if (moveNum == 4)
                {
                    DazeChance = (UnityEngine.Random.Range(0.0f, 1.0f) < (player.LCK + 0.07f)) ? true : false;
                }
            }
            else
            {
                DazeChance = (UnityEngine.Random.Range(0.0f, 1.0f) < enemy.LCK) ? true : false;
            }
            return DazeChance;
        }
        else return false;
    }

    // ------------------------------------------------------------------------------------------------- //

    public int getMoveBaseDMG(int moveNum)
    {
        return moveSetDMG[moveNum];
    }

    // ------------------------------------------------------------------------------------------------- //

    public int getMoveSetCost(int moveNum)
    {
        return moveSetCost[moveNum];
    }

    // ------------------------------------------------------------------------------------------------- //

    public int getHPrecover(int moveNum)
    {
        int hpRecover = 0;

        if (turn.PlayerTurn)
        {
            // It is players turn
            hpRecover = (int)(player.REC * player.MaxHP * 1.35);
        }
        else if (turn.EnemyTurn)
        {
            // It is enemy turn
            hpRecover = (int)(enemy.REC * enemy.MaxHP * 0.85);
        }

        if (moveNum == 0 || moveNum == 2)
        {
            hpRecover = (int)(hpRecover * 0.6);
        }
        else if (moveNum != 7)
        {
            return 0;
        }

        return hpRecover;
    }

    // ------------------------------------------------------------------------------------------------- //

    public int getSPrecover(int moveNum)
    {
        int spRecover = 0;

        if (turn.PlayerTurn)
        {
            // It is players turn
            spRecover = (int)(player.REC * player.MaxSP * 2.25);
        }
        else if (turn.EnemyTurn)
        {
            // It is enemy turn
            spRecover = (int)(enemy.REC * enemy.MaxSP * 2);
        }

        if (moveNum == 0)
        {

            spRecover = (int)(spRecover * 0.6);
            return spRecover;
        }
        else if (moveNum == 6)
        {
            return (int)(spRecover * 2.5);
        }
        else if (moveNum == 1) // || moveNum == 7)
        {
            return spRecover;
        }

        return 0;
    }

    // ------------------------------------------------------------------------------------------------- //

    int getAdjustedDMG(int baseDMG)
    {
        // adjusts baseDMG for attackers pwr stat
        // then adjusts damage for defenders tgh stat
        int betterDMG = 0;
        int afterDEF = 0;

        if (turn.PlayerTurn)
        {
            // It is players turn
            betterDMG = (int)(player.DMG * baseDMG);
            afterDEF = (int)(betterDMG * (1 - enemy.DEF));
/*
            Debug.Log("starting damage: " + baseDMG);
            Debug.Log("adjusted for power: " + betterDMG);
            Debug.Log("adjusted for defense: " + afterDEF);
*/
        }
        else if (turn.EnemyTurn)
        {
            // It is enemy turn
            betterDMG = (int)(enemy.DMG * baseDMG);
            afterDEF = (int)(betterDMG * (1 - player.DEF));
        }

        return afterDEF;
    }

    // ------------------------------------------------------------------------------------------------- //

    public void doEnemyMove(int moveNum)
    {
        // check for valid moveNum 
        if (moveNum < 0 || moveNum > 11)
        {
            Debug.Log("INVALID MOVE NUMBER, EXITING.");
            return;
        }

        int costSP = getMoveSetCost(moveNum);
        int recSP = getSPrecover(moveNum);
        int recHP = getHPrecover(moveNum);

        int baseDMG = getMoveBaseDMG(moveNum);
        int attackerDMG = getAdjustedDMG(baseDMG);
        bool guard = getGuard(); // gets if the one recieving move has guard up
        bool dazePlayer = getDazeChance(moveNum);


        // Halve damage of target is guarding
        if (guard)
        {
            attackerDMG = (int)(attackerDMG / 2);
        }

        // If ememy daze player, set their status dazed = true
        if (dazePlayer)
        {
            player.Dazed = true;
        }

        // If doing Guard move, set guard status = true
        if (moveNum == 6)
        {
            enemy.Guard = true;
        }

        if (moveNum == 8)
        {
            attackerDMG += getAdjustedDMG(getMoveBaseDMG(8));
        }

        // Update player HP with damage done, without going into negative
        if (player.HP > attackerDMG)
        {
            player.HP -= attackerDMG;
        }
        else
        {
            player.HP = 0;
        }

        // Should not be able to spend SP you don't have (prevented elsewhere)
        enemy.SP -= costSP;

        // Get the SP amount recovered this turn & update SP
        if (enemy.SP + recSP > enemy.MaxSP)
        {
            //           Debug.Log("the SP rec is : " + (enemy.MaxSP - enemy.SP));

            recSP = (enemy.MaxSP - enemy.SP);
            enemy.SP = enemy.MaxSP;
        }
        else
        {
            enemy.SP += recSP;
//            Debug.Log("the SP rec is : " + recSP);
        }

        // Get the HP amount recovered this turn & update HP
        if (enemy.HP + recHP > enemy.MaxHP)
        {
            //           Debug.Log("the HP rec is : " + (enemy.MaxHP - enemy.HP));

            recSP = (enemy.MaxHP - enemy.HP);
            enemy.HP = enemy.MaxHP;
        }
        else
        {
            enemy.HP += recHP;
 //           Debug.Log("the HP rec is : " + recHP);
        }

        animate.enemyTextRoll(recHP, recSP);
//        animate.enemyHPText(recHP);
//        animate.enemySPText(recSP);
        animate.playerHPText(-attackerDMG);

        // update HP/SP bars
        // POSSIBLY MOVING THIS LATER!
        player.updatePlayerBar();
        enemy.updateEnemyBar();


        // UPDATE prevMove with the move that happened (number) (could be a miss or counter)
        enemy.prevMove = moveNum;

    }


}
