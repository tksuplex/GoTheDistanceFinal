using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionNode : Node
{
    public Node trueNode, falseNode;

    // Constructor defines true node & false node
    public DecisionNode(Node trueNode, Node falseNode)
    {
        this.trueNode = trueNode;
        this.falseNode = falseNode;
    }

    // get the next node based on bool or int vs min/max
    public abstract Node getDecision();

    // Run the next node
    public override int RunNode()
    {
        return getDecision().RunNode();
    }
}
