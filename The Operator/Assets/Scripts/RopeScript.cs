using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour {

    public Rigidbody RBody;
	// Use this for initialization
	void Start () {
        this.gameObject.AddComponent<Rigidbody>();
        this.RBody = this.gameObject.GetComponent<Rigidbody>();
        this.RBody.isKinematic = true;

        int childCount = this.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            
            Transform transform = this.transform.GetChild(i);
            if (transform.gameObject.name != "Anchor")
            {
                transform.gameObject.AddComponent<HingeJoint>();
                transform.gameObject.AddComponent<Rigidbody>();

                HingeJoint hingeJoint = transform.gameObject.GetComponent<HingeJoint>();

                hingeJoint.connectedBody = i == 0 ? this.RBody : this.transform.GetChild(i - 1).GetComponent<Rigidbody>();

                hingeJoint.useSpring = true;

                hingeJoint.enableCollision = false;
            }
            //Last joint

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
