using UnityEngine;
using System.Collections.Generic;

public class PlugConnector : MonoBehaviour
{
    [HideInInspector]
    public Plug connectedTo = null;
    [SerializeField]
    private bool available = true;

    [SerializeField]
    private int id;
    public int Id { get { return id; } }

    private static Dictionary<int, PlugConnector> id_to_PlugConnector = new Dictionary<int, PlugConnector>();

    public static HashSet<int> GetConnection(int id)
    {
        HashSet<int> ans = new HashSet<int>();

        PlugConnector looked_connector;
        if (!id_to_PlugConnector.TryGetValue(id, out looked_connector))
        {
            Debug.LogError("ID NOT FOUND!");
            return null;
        }
        ans.Add(id);

        if (looked_connector.connectedTo == null)
        {
            return ans;
        }

        foreach (var connector in looked_connector.connectedTo.cable.ConnectedTo)
        {
            ans.Add(connector.id);
        }

        return ans;
    }

    private void Start()
    {
        id_to_PlugConnector.Add(id, this);
    }

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

    private void OnDestroy()
    {
        if (id_to_PlugConnector != null)
        {
            id_to_PlugConnector.Remove(id);
        }
    }
}
