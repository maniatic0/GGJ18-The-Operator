using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Mouse : MonoBehaviour {

    LineRenderer line;
    public float line_distance = 100f;
    public LayerMask ignore_mask;
    public Transform main_board;
    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignore_mask.value))
        {
            line.enabled = true;
            line.SetPosition(0, hit.point - main_board.forward * line_distance);
            line.SetPosition(1, hit.point);
        }
        else
        {
            line.enabled = false;
        }
    }
}
