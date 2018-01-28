using UnityEngine;
using System.Collections;

public class Plug : MonoBehaviour
{
    [Tooltip("The cable this plugs belongs to")]
    public Cable cable;
    [HideInInspector]
    public PlugConnector connectedTo;
    public bool locked = false;

    int connectId = 0;
    public int ConnectId { get { return connectId; } }

    public bool Disconnect()
    {
        if (locked)
        {
            return false;
        }

        if (connectedTo != null)
        {
            cable.Disconnect(connectedTo);
            connectedTo.Disconnect();
            connectedTo = null;
        }

        connectId = 0;
        return true;
    }

    public void Connect(PlugConnector con)
    {
        Disconnect();
        cable.Connect(con);
        connectedTo = con;
        connectedTo.Connect(this);

        connectId = con.Id;
    }
}
