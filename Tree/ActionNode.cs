using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public int ActionNum;

    // Action node constructor
    public ActionNode(int num)
    {
        ActionNum = num;
    }

    // Return the action value as the decision of tree run
    public override int RunNode()
    {
        return ActionNum;
    }
}
