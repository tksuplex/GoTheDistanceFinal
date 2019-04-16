using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    // Constructor
    public Node() { }

    // Def. function that is overridden in other node classes
    public abstract int RunNode();
}
