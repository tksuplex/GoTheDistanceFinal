using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNode : DecisionNode
{
    int testInt, min, max;

    // Constructor for testing an int aganist min/max value
    public TestNode(Node trueNode, Node falseNode, int testInt, int min, int max) : base(trueNode, falseNode)
    {
        this.testInt = testInt;
        this.min = min;
        this.max = max;
    }

    // test value against min/max
    public override Node getDecision()
    {
        if (min <= testInt && testInt <= max)
        {
            return trueNode;
        }
        else
        {
            return falseNode;
        }
    }
}
