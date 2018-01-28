using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLightScript : MonoBehaviour {

    private Light light;
    public GameObject plugs;
    // Use this for initialization
    void Start () {
        light = this.gameObject.GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        //MoveLight(6);
	}
    public void MoveLight(int requestID)
    {
        int rowCount = plugs.transform.childCount;

        for (int i = 0; i < rowCount; i++)
        {
            Transform row = plugs.transform.GetChild(i);
            int plugCount = row.childCount;
            for (int j = 0; j < plugCount; j++)
            {
                Transform plug = row.GetChild(j);
                var connector = plug.gameObject.GetComponent<PlugConnector>();
                if (connector != null && connector.Id == requestID)
                {
                    light.enabled = true;
                    light.transform.position = new Vector3(plug.transform.position.x, plug.transform.position.y + 0.8f, plug.transform.position.z);

                }
            }
        }
    }
    public void DisableLight()
    {
        light.enabled = false;
    }
}
