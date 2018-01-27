using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cable : MonoBehaviour
{
    private HashSet<PlugConnector> connectedTo = new HashSet<PlugConnector>();
    public HashSet<PlugConnector> ConnectedTo { get { return connectedTo; } }

    public void Connect(PlugConnector con)
    {
        connectedTo.Add(con);
    }

    public void Disconnect(PlugConnector con)
    {
        connectedTo.Remove(con);
    }
}
