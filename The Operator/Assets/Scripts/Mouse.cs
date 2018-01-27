using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Mouse : MonoBehaviour {

    LineRenderer line;
    public float line_distance = 5f;
    public float plug_distance = 2f;
    public LayerMask board_mask;
    public LayerMask plug_mask;
    public LayerMask plug_connector_mask;
    public Transform main_board;
    public bool show_line = true;

    private Plug picked_plug;
    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        MouseRay();
    }

    void MouseRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int final_mask = board_mask.value;

        if (picked_plug == null)
        {
            final_mask |= plug_mask.value;
        }
        else
        {
            final_mask |= plug_connector_mask.value;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, final_mask))
        {
            if (show_line)
            {
                line.enabled = true;
                line.SetPosition(0, hit.point - main_board.forward * line_distance);
                line.SetPosition(1, hit.point);
            }

            if (picked_plug != null)
            {
                picked_plug.transform.position = hit.point - main_board.forward * line_distance;
                if (Input.GetMouseButtonDown(0))
                {
                    ReleasePlug(hit.collider);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PickUpPlug(hit.collider);
                }
            }
        }
        else
        {
            if (show_line)
            {
                line.enabled = false;
            }
        }
    }

    void PickUpPlug(Collider plug)
    {
        picked_plug = plug.transform.GetComponent<Plug>();
        if (picked_plug != null)
        {
            if (!picked_plug.Disconnect())
            {
                picked_plug = null;
            }            
        }
    }

    void ReleasePlug(Collider plug_connector)
    {
        if (picked_plug == null)
        {
            return;
        }
        PlugConnector con = plug_connector.GetComponent<PlugConnector>();
        if (con == null || !con.IsFree())
        {
            return;
        }
        picked_plug.transform.position = con.transform.position - main_board.forward * plug_distance;
        picked_plug.Connect(con);
        picked_plug = null;
    }
}


