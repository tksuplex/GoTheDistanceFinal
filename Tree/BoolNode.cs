using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolNode : DecisionNode
{
    bool testBool;

    // Constructor for getting next node from a given bool
    public BoolNode(Node trueNode, Node falseNode, bool testBool) : base(trueNode, falseNode)
    {
        this.testBool = testBool;
    }

    // Get next node from given bool value
    public override Node getDecision()
    {
        if (testBool)
        {
            return trueNode;
        }
        else
        {
            return falseNode;
        }
    }
}
