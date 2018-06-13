using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarControl : MonoBehaviour {

    public float maxTorque = 250f, maxAngle = 45f, brakeTorque = 30000f;
    public float fuel = 250f, damage = 0f;
    public int points = 0;
    public bool disableFuel = false;

    private WheelCollider[] wheels;
    [HideInInspector]
    public List<Transform> Nodes;
    public int currentnode, currentlap;
    public Transform Path;
    public GameObject wheelShape, gameOverUI;
    public bool paused, newlap;

    public Material normalMat, brokenMat;

    [HideInInspector]
    public float respawnTimer;
    [HideInInspector]
    public bool allowedToRespawn = false;

	// Use this for initialization
	void Start () {
        currentlap = 1;
        Transform[] PathTransForms = Path.GetComponentsInChildren<Transform>();
        Nodes = new List<Transform>();

        for (int i = 0; i < PathTransForms.Length; i++)
        {
            if (PathTransForms[i] != Path.transform)
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

        SetMaterialOnAllPaintable(normalMat);
	}
	
	// Update is called once per frame
	void Update () {
        paused = GameObject.Find("PauseObject").GetComponent<pauseScript>().paused;
        if (paused == false)
        {
            CheckNodes();

            float torque = maxTorque * Input.GetAxis("Vertical");
            float angle = maxAngle * Input.GetAxis("Horizontal");
            float handbrake;

            if (!disableFuel)
            {
                fuel -= Mathf.Abs(torque) * 0.0003f;
                if (fuel < 10f) torque = torque / 2;
                if (fuel < 5f) torque = torque / 2;
                if (fuel <= 0)
                {
                    torque = 0;
                    fuel = 0f;
                }
                if (fuel > 250) fuel = 250;
            }

            if (damage >= 100) torque = 0;

            if (Input.GetKey(KeyCode.X)) handbrake = brakeTorque;
            else handbrake = 0;

            foreach (WheelCollider wheel in wheels)
            {
                if (wheel.transform.localPosition.z > 0)
                    wheel.steerAngle = angle;

                if (wheel.transform.localPosition.z < 0)
                {
                    wheel.motorTorque = torque;
                    wheel.brakeTorque = handbrake;
                }

                if (wheelShape)
                {
                    Quaternion q;
                    Vector3 p;
                    wheel.GetWorldPose(out p, out q);

                    q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z + 90);

                    Transform shapeTransform = wheel.transform.GetChild(0);
                    shapeTransform.position = p;
                    shapeTransform.rotation = q;
                }
            }

            if (fuel < 10 || damage > 90 || (gameObject.transform.rotation.eulerAngles.x > 35 && gameObject.transform.rotation.eulerAngles.x < 360 - 35) ||
                (gameObject.transform.rotation.eulerAngles.z > 35 && gameObject.transform.rotation.eulerAngles.z < 360 - 35))
            {
                allowedToRespawn = true;
                SetMaterialOnAllPaintable(brokenMat);
            }
            
            if (Input.GetKey(KeyCode.F))
            {
                if (allowedToRespawn)
                {
                    respawnTimer += Time.deltaTime;
                    if (respawnTimer >= 2.5f)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, gameObject.transform.rotation.eulerAngles.y, 0));
                        damage = 0;
                        fuel = 250;
                        respawnTimer = 0;
                        allowedToRespawn = false;
                        SetMaterialOnAllPaintable(normalMat);
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                respawnTimer = 0;
            }

            if (currentnode == 0 && newlap)
            {
                currentlap++;
                newlap = false;
            }
            else if (currentnode == 1)
            {
                newlap = true;
            }

            if (currentlap == 4)
            {
                Instantiate(gameOverUI, GameObject.Find("Canvas").transform);
                GameObject.Find("Canvas/HUD").SetActive(false);
                GameObject.Find("PauseObject").GetComponent<pauseScript>().paused = true;
            }
        }

        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].GetComponent<MeshRenderer>().enabled = false;
            Nodes[i].localScale = new Vector3(1f, 1f, 1f);
            if (i == currentnode || i == currentnode + 1 || i == currentnode + 2 || i == currentnode + 3 || i == currentnode + 4)
            {
                Nodes[i].GetComponent<MeshRenderer>().enabled = true;
            }

            if (i == currentnode + 1)
            {
                Nodes[i].localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }
            else if (i == currentnode + 2)
            {
                Nodes[i].localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else if (i == currentnode + 3)
            {
                Nodes[i].localScale = new Vector3(0.25f, 0.25f, 0.25f);
            }
            else if (i == currentnode + 4)
            {
                Nodes[i].localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
	}

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag != "NoDamage")
        {
            damage += coll.relativeVelocity.magnitude * 3;
            if (damage > 100)
            {
                damage = 100;
            }
        }
    }

    void CheckNodes()
    {
        if (Vector3.Distance(transform.position, Nodes[currentnode].position) < 2f)
        {
            if (currentnode == Nodes.Count - 1)
            {
                currentnode = 0;
            }
            else
            {
                currentnode++;
            }
        }
    }

    void SetMaterialOnAllPaintable(Material m)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "CarPaint")
            {
                child.GetComponent<Renderer>().material = m;
            }
        }
    }
}
