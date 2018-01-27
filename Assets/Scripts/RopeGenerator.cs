using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeGenerator : MonoBehaviour {

    public Transform Node;
    public Transform baseConnector;
    public int nbrOfSegments = 10;
    public int length = 10;
	// Use this for initialization
	void Start () {
        int resolution = length / nbrOfSegments;
        for (int i = 0; i < nbrOfSegments; i++)
        {
            Transform aNode = Instantiate(Node, new Vector3(0, -1f, 0), Quaternion.identity);
            //Node.SetParent(this.transform);
            aNode.parent = this.transform;
        }

        int childCount = this.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform node = this.transform.GetChild(i);
            //Connects nodes to previous nodes
            if (i == 0)
            {
                //connect to base
                node.GetComponent<AdvancedRope>().whatTheRopeIsConnectedTo = node.GetChild(0);
                node.GetComponent<AdvancedRope>().whatIsHangingFromTheRope = baseConnector;
                
            }
            else
            {
            Transform prevNode = this.transform.GetChild(i - 1);

            Transform anchor = node.GetChild(0);

            node.GetComponent<AdvancedRope>().whatTheRopeIsConnectedTo = anchor;
            node.GetComponent<AdvancedRope>().whatIsHangingFromTheRope = prevNode.GetChild(0);

            }

            //Connect the anchors to the following nodes
            if (i < 1)
            {
                baseConnector.GetComponent<SpringJoint>().connectedBody = node.GetChild(0).GetComponent<Rigidbody>();
                Transform nextNode = this.transform.GetChild(i + 1);
                Rigidbody nextRigidbody = nextNode.GetChild(0).GetComponent<Rigidbody>();
                node.GetChild(0).GetComponent<SpringJoint>().connectedBody = nextRigidbody;

                
            }
            else if (i == childCount - 1)
            {
                //Dont connect last node
                Rigidbody rigidbody = node.GetChild(0).GetComponent<Rigidbody>();
                rigidbody.isKinematic = true;
            }
            else
            {
                Transform nextNode = this.transform.GetChild(i + 1);
                Rigidbody nextRigidbody = nextNode.GetChild(0).GetComponent<Rigidbody>();
                node.GetChild(0).GetComponent<SpringJoint>().connectedBody = nextRigidbody;
            }
            if (node != null)
            {
                SpringJoint sj = node.GetChild(0).GetComponent<SpringJoint>();
                sj.enableCollision = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
