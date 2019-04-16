using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTree : MonoBehaviour
{
    public int runEnemyAI(PlayerObject player, EnemyObject enemy, MatchTurn turn, MoveSetScript moveSet)
    {
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

        int answerAI = -1; // Holds move number result; returns -1 if error
        int divNum = 4; // Number of turns between special attacks
        int lowSP = 40; // Threshold for SP before attempt SP recovery
        int enemyLowHP = 30; // Threshold for enemy HP
        int playerLowHP = 20; // Threshold for player HP

        bool EnemyMiss = (Random.Range(0.0f, 1.0f) < player.EVA) ? true : false;
        bool PlayerDefense = (player.prevMove == 6 || player.prevMove == 7) ? true : false;
        bool RandoHookChance = (Random.Range(0.0f, 1.0f) < 0.55f) ? true : false;
        /*
        bool divis = ((enemy.turnCounter % divNum)==0 ? true : false);
        Debug.Log("divNum = 3; turn num = " + enemy.turnCounter + "; divisible? = " + divis);
        */

        // LEAF NODES:
        Node Counter = new ActionNode(0);
        Node Jab = new ActionNode(1);
        Node Straight = new ActionNode(2);
        Node Hook = new ActionNode(3);
        Node Sunday = new ActionNode(4);
        Node Dynamite = new ActionNode(5);
        Node Guard = new ActionNode(6);
        Node Recover = new ActionNode(7);
        Node ButterBee = new ActionNode(8);
        Node Miss = new ActionNode(9);
        Node Dazed = new ActionNode(10);
        Node Finisher = new ActionNode(11);

        // SPECIAL BRANCH:

        // Test if player rank == 2, if yes Special move is ButterBee, else Dynamite
        Node PlayerRank2 = new TestNode(ButterBee, Dynamite, player.PlayerRank, 2, 2);
        // Test if player rank == 3, if yes Special move is Sunday Punch, else PlayerRank2 node
        Node PlayerRank3 = new TestNode(Sunday, PlayerRank2, player.PlayerRank, 3, 3);
        // Test if match is training match, if yes return heavy, else PlayerRank3 node
        Node SpecialTraining = new BoolNode(Hook, PlayerRank3, player.Sparring);

        // LOW SP BRANCH: 

        // Decide between Hook or Straight by Random number (see RandoHookChance above)
        Node RandomHook = new BoolNode(Hook, Straight, RandoHookChance);
        // If enemy Misses, return Miss, else RandomHook node
        Node MissRegular = new BoolNode(Miss, RandomHook, EnemyMiss);
        // If enemy HP is <= player HP, Recover else Jab
        Node FreeGetHP = new TestNode(Recover, Jab, enemy.HP, 0, player.HP);
        // If player last move was defensive FreeGetHP node, else RandomHook node
        Node PlayerDefensive = new BoolNode(FreeGetHP, MissRegular, PlayerDefense);
        // If miss true Jab, else SpecialTraining node
        Node MissSpecial = new BoolNode(Jab, SpecialTraining, EnemyMiss);
        // If enemy.turnCount is divisible by divNum, MissSpecial node, else PlayerDefensive node
        Node TurnDivisible = new TestNode(MissSpecial, PlayerDefensive, (enemy.turnCounter % divNum), 0, 0);
        // If enemy HP is <= player HP, Guard, else Jab
        Node LowStamGetSP = new TestNode(Guard, Jab, enemy.HP, 0, player.HP);
        // If enemy.sp < lowSP, LowStamGetSP, else TurnDivisible
        Node EnemyLowSP = new TestNode(LowStamGetSP, TurnDivisible, enemy.SP, 0, lowSP);

        // CRITICAL HEALTH BRANCH:

        // If enemy.SP < recover cost, Jab, else Recover
        Node RecoverCost = new TestNode(Jab, Recover, enemy.SP, 0, moveSet.getMoveSetCost(7));
        // If enemy HP is <= enemyLowHP, RecoverCost node, else EnemyLowSP node
        Node EnemyCritical = new TestNode(RecoverCost, EnemyLowSP, enemy.HP, 0, enemyLowHP);
        // If enemy Misses, return Miss, else return Finish
        Node MissFinisher = new BoolNode(Miss, Finisher, EnemyMiss);
        // If enemy.SP < recover cost, Jab, else Recover
        Node FinisherCost = new TestNode(RecoverCost, MissFinisher, enemy.SP, 0, moveSet.getMoveSetCost(11));
        // If enemy HP is <= enemyLowHP, RecoverCost node, else EnemyLowSP node
        Node PlayerCritical = new TestNode(FinisherCost, EnemyCritical, player.HP, 0, playerLowHP);

        // START / ROOT:

        // if enemy miss, return miss, else return counter
        Node MissCounter = new BoolNode(Miss, Counter, EnemyMiss);
        // if enemy.SP < Counter's SP cost, Jab, else MissCounter node
        Node CounterCost = new TestNode(Jab, MissCounter, enemy.SP, 0, moveSet.getMoveSetCost(0));
        // If player is dazed, RecoverCost node, else PlayerCritical node
        Node PlayerDazed = new BoolNode(RecoverCost, PlayerCritical, player.Dazed);
        // If enemy is dazed, return Dazed, else PlayerDazed node
        Node EnemyDazed = new BoolNode(Dazed, PlayerDazed, enemy.Dazed);
        // ROOT node, if player missed then CounterCost node, else EnemyDazed node
        Node RootPlayerMiss = new TestNode(CounterCost, EnemyDazed, player.prevMove, 9, 9);

        answerAI = RootPlayerMiss.RunNode();

        return answerAI;
    }


    public void testAI2()
    {
        int answerAI = -1;

        // build tree...

        Node action3 = new ActionNode(3);
        Node action6 = new ActionNode(6);
        // action3 = new ActionNode(7);

        // yesnode, falsenode, bool
        //        Node root0 = new DecisionNode(action3, action6, false);
        // yesnode, falsenode, testint, min, max
        //        Node root1 = new DecisionNode(action3, action6, 12, 11, 11);



        Node root1 = new TestNode(action3, action6, 0, 0, 0);
        answerAI = root1.RunNode();

        Node root0 = new BoolNode(action3, action6, true);
        answerAI = root0.RunNode();

        Debug.Log("OUTPUT OF AI TREE: " + answerAI);
    }

/*
    public void testTrueAI(PlayerObject player, EnemyObject enemy, MatchTurn turn, MoveSetScript moveSet)
    {

        int answerAI = -1; // Holds move number result; returns -1 if error
        int divNum = 3; // Number of turns between special attacks
        int lowSP = 20; // Threshold for SP before attempt SP recovery
        int enemyLowHP = 20; // Threshold for enemy HP
        int playerLowHP = 20; // Threshold for player HP

        bool EnemyMiss = (Random.Range(0.0f, 1.0f) < player.EVA) ? true : false;
        bool PlayerDefense = (player.prevMove == 6 || player.prevMove == 7) ? true : false;
        bool RandoHookChance = (Random.Range(0.0f, 1.0f) < 0.55f) ? true : false;

        // LEAF NODES:
        Node Counter = new ActionNode(0);
        Node Jab = new ActionNode(1);
        Node Straight = new ActionNode(2);
        Node Hook = new ActionNode(3);
        Node Sunday = new ActionNode(4);
        Node Dynamite = new ActionNode(5);
        Node Guard = new ActionNode(6);
        Node Recover = new ActionNode(7);
        Node ButterBee = new ActionNode(8);
        Node Miss = new ActionNode(9);
        Node Dazed = new ActionNode(10);
        Node Finisher = new ActionNode(11);

        // SPECIAL BRANCH:

        player.prevMove = 9;

        // Test if player rank == 2, if yes Special move is ButterBee, else Dynamite
        Node PlayerRank2 = new TestNode(ButterBee, Dynamite, player.PlayerRank, 2, 2);
        // Test if player rank == 3, if yes Special move is Sunday Punch, else PlayerRank2 node
        Node PlayerRank3 = new TestNode(Sunday, PlayerRank2, player.PlayerRank, 3, 3);
        // Test if match is training match, if yes return heavy, else PlayerRank3 node
        Node SpecialTraining = new BoolNode(Hook, PlayerRank3, player.Sparring);

        // LOW SP BRANCH: 

        // Decide between Hook or Straight by Random number (see RandoHookChance above)
        Node RandomHook = new BoolNode(Hook, Straight, RandoHookChance);
        // If enemy Misses, return Miss, else RandomHook node
        Node MissRegular = new BoolNode(Miss, RandomHook, EnemyMiss);
        // If enemy HP is <= player HP, Recover else Jab
        Node FreeGetHP = new TestNode(Recover, Jab, enemy.HP, 0, player.HP);
        // If player last move was defensive FreeGetHP node, else RandomHook node
        Node PlayerDefensive = new BoolNode(FreeGetHP, MissRegular, PlayerDefense);
        // If miss true Jab, else SpecialTraining node
        Node MissSpecial = new BoolNode(Jab, SpecialTraining, EnemyMiss);
        // If enemy.turnCount is divisible by divNum, MissSpecial node, else PlayerDefensive node
        Node TurnDivisible = new TestNode(MissSpecial, PlayerDefensive, (enemy.turnCounter % divNum), 0, 0);
        // If enemy HP is <= player HP, Guard, else Jab
        Node LowStamGetSP = new TestNode(Guard, Jab, enemy.HP, 0, player.HP);
        // If enemy.sp < lowSP, LowStamGetSP, else TurnDivisible
        Node EnemyLowSP = new TestNode(LowStamGetSP, TurnDivisible, enemy.SP, 0, lowSP);

        // CRITICAL HEALTH BRANCH:

        // If enemy.SP < recover cost, Jab, else Recover
        Node RecoverCost = new TestNode(Jab, Recover, enemy.SP, 0, moveSet.getMoveSetCost(7));
        // If enemy HP is <= enemyLowHP, RecoverCost node, else EnemyLowSP node
        Node EnemyCritical = new TestNode(RecoverCost, EnemyLowSP, enemy.HP, 0, enemyLowHP);
        // If enemy Misses, return Miss, else return Finish
        Node MissFinisher = new BoolNode(Miss, Finisher, EnemyMiss);
        // If enemy.SP < recover cost, Jab, else Recover
        Node FinisherCost = new TestNode(Jab, MissFinisher, enemy.SP, 0, moveSet.getMoveSetCost(11));
        // If enemy HP is <= enemyLowHP, RecoverCost node, else EnemyLowSP node
        Node PlayerCritical = new TestNode(FinisherCost, EnemyCritical, player.HP, 0, playerLowHP);

        // START / ROOT:

        // if enemy miss, return miss, else return counter
        Node MissCounter = new BoolNode(Miss, Counter, EnemyMiss);
        // if enemy.SP < Counter's SP cost, Jab, else MissCounter node
        Node CounterCost = new TestNode(Jab, MissCounter, enemy.SP, 0, moveSet.getMoveSetCost(0));
        // If player is dazed, RecoverCost node, else PlayerCritical node
        Node PlayerDazed = new BoolNode(RecoverCost, PlayerCritical, player.Dazed);
        // If enemy is dazed, return Dazed, else PlayerDazed node
        Node EnemyDazed = new BoolNode(Dazed, PlayerDazed, enemy.Dazed);
        // ROOT node, if player missed then CounterCost node, else EnemyDazed node
        Node RootPlayerMiss = new TestNode(CounterCost, EnemyDazed, player.prevMove, 9, 9);

        answerAI = RootPlayerMiss.RunNode();
        Debug.Log("OUTPUT OF AI TREE: " + answerAI);
    }

*/
}
