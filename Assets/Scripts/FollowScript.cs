using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

    public Transform followTransform;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (followTransform == null)
        {
            return;
        }
        this.transform.position = followTransform.position;
	}
}
