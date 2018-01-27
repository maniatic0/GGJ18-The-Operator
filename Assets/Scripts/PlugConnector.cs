using UnityEngine;
using System.Collections;

public class PlugConnector : MonoBehaviour
{
    [HideInInspector]
    public Plug connectedTo = null;
    [SerializeField]
    private bool available = true;

    public void Connect(Plug plug)
    {
        connectedTo = plug;
    }
    public void Disconnect()
    {
        connectedTo = null;
    }

    public bool IsFree()
    {
        return available && connectedTo == null;
    }

    public bool LockPlug()
    {
        if (connectedTo == null)
        {
            return false;
        }
        connectedTo.locked = true;
        return true;
    }

    public bool UnlockPlug()
    {
        if (connectedTo == null)
        {
            return false;
        }
        connectedTo.locked = false;
        return true;
    }
}
