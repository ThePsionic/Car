using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIController : MonoBehaviour {

    private Text position, pickup, points, timer, respawnText;
    private Slider fuelMeter, damageMeter, respawnTimer;
    private CarControl carControl;
    private float countDown;
    private bool hudEnabled;

    private GameObject hudGO;

    // Use this for initialization
    void Start () {
        carControl = GetComponent<CarControl>();
        Transform canvas = GameObject.Find("Canvas").transform;
        hudGO = GameObject.Find("HUD");
        Transform hud = hudGO.transform;
        position = hud.Find("PositionLabel").GetComponent<Text>();
        pickup = hud.Find("PickupLabel").GetComponent<Text>();
        timer = canvas.Find("countDownTimer").GetComponent<Text>();
        fuelMeter = hud.Find("FuelMeter").GetComponent<Slider>();
        damageMeter = hud.Find("DamageMeter").GetComponent<Slider>();
        hudEnabled = false;
        countDown = 5f;

        Transform respawn = hud.Find("Respawns").transform;
        respawnText = respawn.Find("RespawnText").GetComponent<Text>();
        respawnTimer = respawn.Find("RespawnSlider").GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        fuelMeter.value = carControl.fuel;
        damageMeter.value = carControl.damage;

        if(GetComponent<CarControl>().maxTorque == 500)
        {
            pickup.text = "Speedboost active";
        }else
        {
            pickup.text = "No pickup active";
        }

        position.text = "At node " + carControl.currentnode + " of " + carControl.Nodes.Count + ", lap " + carControl.currentlap + " of 3";

        respawnText.gameObject.SetActive(carControl.allowedToRespawn);
        respawnTimer.gameObject.SetActive(carControl.respawnTimer > 0);
        respawnTimer.value = carControl.respawnTimer;
        Timer();
	}

    void Timer ()
    {
        hudGO.SetActive(hudEnabled);

        if(countDown > 4)
        {
            timer.text = "3";
            countDown -= 1 * Time.deltaTime;
        }
        else if (countDown > 3)
        {
            timer.text = "2";
            countDown -= 1 * Time.deltaTime;
        }
        else if (countDown > 2)
        {
            timer.text = "1";
            countDown -= 1 * Time.deltaTime;
        }
        else if (countDown > 0)
        {
            timer.text = "GO!";
            countDown -= 1 * Time.deltaTime;
            GameObject.Find("PauseObject").GetComponent<pauseScript>().paused = false;
        }
        else if (countDown > -1)
        {
            timer.text = "";
            hudEnabled = true;
        }
    }
}
