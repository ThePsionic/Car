using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour
{

    public float boostTime = 5.0f;

    void Update()
    {
        if (this.tag == "SpeedBoost")
        {
            this.transform.Rotate(Vector3.up * 3, Space.World);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //The pickup that allows the car to move faster for a few seconds
        if (this.gameObject.tag == "SpeedBoost")
        {
            if (col.gameObject.tag == "Player")
            {
                boostTime -= Time.deltaTime;
                GameObject.FindGameObjectWithTag("Player").GetComponent<CarControl>().maxTorque = 500;
                transform.position = new Vector3(transform.position.x, -5f, transform.position.z);
                StartCoroutine("setMaxTorqueBack");
            }
        }

        //This pickup replenishes the fuel for a bit;
        if (this.gameObject.tag == "FuelBoost")
        {
            if (col.gameObject.tag == "Player")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CarControl>().fuel += 100;
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator setMaxTorqueBack()
    {
        yield return new WaitForSeconds(5);

        GameObject.FindGameObjectWithTag("Player").GetComponent<CarControl>().maxTorque = 250;
        boostTime = 5.0f;
        Destroy(this.gameObject);
    }
}