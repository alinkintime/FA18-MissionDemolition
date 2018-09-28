using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    //The static point of interest
    static public GameObject POI;

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    //The desired z pos of the camera
    [Header("Set Dynamically")]
    public float camZ;

    void Awake()
    {
        camZ = this.transform.position.z;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Vector3 destination;
        //If there is no poi, return to p:[0,0,0]
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            //Get the position of the poi
            destination = POI.transform.position;

            //If poi is a projectile, check to see if it's at rest
            if (POI.tag == "Projectile")
            {
                //if it is not moving
                if(POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;

                    //In the next update
                    return;
                }
            }
        }

        // Limit the X & Y to the minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //Interpolate from the current Camera position towards the destination
        destination = Vector3.Lerp(transform.position, destination, easing);

        //Force destination.z to the camZ to keep the camera far enough away
        destination.z = camZ;

        //Set the camera to the destination
        transform.position = destination;

        //Set the orthographic size of the Camera to keep the Ground in view
        Camera.main.orthographicSize = destination.y + 10;
    }
}
