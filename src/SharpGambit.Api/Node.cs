namespace SharpGambit;

using System.Collections.Generic;

public class Node
{
    public InfoSet? InfoSet {  get; protected set; } 
    
    public List<Node> Children { get; protected set;  } = new List<Node>();
}
