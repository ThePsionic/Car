using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControler : MonoBehaviour {

    public Transform Path;
    private WheelCollider[] wheels;
    public GameObject wheelShape;
    public bool isBraking = false;
    /*public float sensorLength = 0.5f;
    public float frontSensorPosition = 0.5f;*/
    public float acceleration = 250, maxTorque = 250f, maxAngle = 40f, brakeTorque = 25f;

    private int Difficulty;
    private List<Transform> Nodes;
    private int currentnode = 0;
    private float slowDownOnAngle;
    private float brakeTime;
    public bool paused;

    // Use this for initialization
    void Start ()
    {
        /*Difficulty = PlayerPrefs.GetInt("Degree");

        if(Difficulty == 0)
        {
            Path = GameObject.Find("Makkelijke Pad").transform;
        }
        else if (Difficulty == 1)
        {
            Path = GameObject.Find("Middelmatig Pad").transform;
        }
        else if (Difficulty == 2)
        {
            Path = GameObject.Find("Moeilijk Pad").transform;
        }*/

        Transform[] PathTransForms = Path.GetComponentsInChildren<Transform>();
        Nodes = new List<Transform>();

        for (int i = 0; i < PathTransForms.Length; i++)
        {
            if(PathTransForms[i] != Path.transform)
            {
                Nodes.Add(PathTransForms[i]);
            }
        }

        wheels = GetComponentsInChildren<WheelCollider>();

        for (int i = 0; i < wheels.Length; i++)
        {
            WheelCollider currentWheel = wheels[i];

            currentWheel.center += new Vector3(0, currentWheel.suspensionDistance / 2, 0);

            if (wheelShape != null)
            {
                var ws = Instantiate(wheelShape);
                ws.transform.position = currentWheel.transform.position;
                ws.transform.parent = currentWheel.transform;
            }
        }
	}

    void Update()
    {
        paused = GameObject.Find("PauseObject").GetComponent<pauseScript>().paused;
        if (paused == false)
        {
            Vector3 relativeVector = transform.InverseTransformPoint(Nodes[currentnode].position);
            float newSteer = (relativeVector.x / relativeVector.magnitude);
            float torque = acceleration;
            float angle = maxAngle * newSteer;

            foreach (WheelCollider wheel in wheels)
            {
                slowDownOnAngle = 25000;

                if (wheel.transform.localPosition.z > 0)
                {
                    wheel.steerAngle = angle;
                    wheel.motorTorque = torque;
                }

                if (wheel.transform.localPosition.z < 0)
                {
                    if (relativeVector.magnitude < 20f && wheel.rpm > 250f)
                    {
                        brakeTime = 3;
                        slowDownOnAngle = 250000;
                    }
                    else if (relativeVector.magnitude < 12.5f && wheel.rpm > 125f)
                    {
                        brakeTime = 3;
                        slowDownOnAngle = 500000;
                    }
                    else if (relativeVector.magnitude < 7.5f && wheel.rpm > 75f)
                    {
                        brakeTime = 3;
                        slowDownOnAngle = 1000000;
                    }
                    else if (relativeVector.magnitude < 2.5f && wheel.rpm > 25f)
                    {
                        brakeTime = 3;
                        slowDownOnAngle = 2500000;
                    }

                    if (brakeTime > 0)
                    {
                        isBraking = true;
                        brakeTime -= 1 * Time.deltaTime;
                    }
                    else
                    {
                        isBraking = false;
                        brakeTime = 0;
                    }


                    if (isBraking)
                    {
                        wheel.brakeTorque = slowDownOnAngle * wheel.rpm;
                    }
                    else
                    {
                        wheel.brakeTorque = 0;
                    }
                    //Debug.Log("RPM:" + wheel.rpm + "    Distance: " + relativeVector.magnitude + "    Braketimer: " + brakeTime);
                }

                /*if (wheelShape)
                {
                    Quaternion q;
                    Vector3 p;
                    wheel.GetWorldPose(out p, out q);

                    q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z + 90);

                    Transform shapeTransform = wheel.transform.GetChild(0);
                    shapeTransform.position = p;
                    shapeTransform.rotation = q;
                }*/
            }
            CheckNodes();
        }
    }

    void CheckNodes()
    {
        if(Vector3.Distance(transform.position, Nodes[currentnode].position) < 2f)
        {
            if(currentnode == Nodes.Count - 1)
            {
                currentnode = 0;
            }
            else
            {
                currentnode++;
            }
        }
    }
    /*void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPosition = transform.position;
        sensorStartPosition.z += frontSensorPosition;

        if (Physics.Raycast(sensorStartPosition, transform.forward, out hit, sensorLength))
        {

        }
        Gizmos.DrawLine(sensorStartPosition, hit.point);
    }*/

}
