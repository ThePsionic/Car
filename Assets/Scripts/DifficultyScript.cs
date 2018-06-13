using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DifficultyScript : MonoBehaviour {

    int difficulty;
    bool doneOnce;
    Scene scene;

    Transform SpawnpointEasy;
    Transform SpawnpointMedium;
    Transform SpawnpointHard;

    GameObject EasyPath;
    GameObject MediumPath;
    GameObject HardPath;
    GameObject Camera;

    public GameObject Player;
    public GameObject FinishLine;
    public GameObject Enemy;
    public GameObject FuelPickup;
    public GameObject SpeedPickup;

    public AudioClip easySong;
    public AudioClip mediumSong;
    public AudioClip hardSong;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
	
	void Update ()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "GameScene" && doneOnce == false)
        {
            EasyPath = GameObject.Find("Makkelijke Pad");
            MediumPath = GameObject.Find("Middelmatig Pad");
            HardPath = GameObject.Find("Moeilijk Pad");
            difficulty = PlayerPrefs.GetInt("Degree");
            Camera = GameObject.Find("Main Camera");
            AudioSource audio = GetComponent<AudioSource>();


            if (difficulty == 1)
            {
                doneOnce = true;
                MediumPath.gameObject.SetActive(false);
                HardPath.gameObject.SetActive(false);

                audio.clip = easySong;
                audio.Play();

                SpawnpointEasy = GameObject.Find("PlayerSpawnEasy").transform;
                GameObject InstantiatedPlayer = Instantiate(Player, new Vector3(SpawnpointEasy.position.x, SpawnpointEasy.position.y, SpawnpointEasy.position.z), Quaternion.Euler(0, 180, 0));
                InstantiatedPlayer.GetComponent<CarControl>().Path = EasyPath.transform;
                Camera.GetComponent<CameraFollowScript>().target = InstantiatedPlayer.transform;
                Instantiate(FinishLine, new Vector3(0.584f, SpawnpointEasy.position.y -0.5f, 15.874f), Quaternion.Euler(0, 90, 0));

                for(int i = 0; i < 3; i++)
                {
                    GameObject InstantiatedEnemy = Instantiate(Enemy, new Vector3(SpawnpointEasy.position.x + Random.Range(-4f, 4f), SpawnpointEasy.position.y, SpawnpointEasy.position.z + 2 +(2 * i)), Quaternion.Euler(0, 180, 0));
                    InstantiatedEnemy.GetComponent<AIControler>().Path = EasyPath.transform;
                }

                for (int i = 0; i < 4; i++)
                {
                    GameObject Node = GameObject.Find("Node (" + Random.Range(2, 67) + ")");
                    Instantiate(FuelPickup, new Vector3(Node.transform.position.x, Node.transform.position.y, Node.transform.position.z), Quaternion.Euler(0, 180, 0));
                }

                for (int i = 0; i < 2; i++)
                {
                    GameObject Node = GameObject.Find("Node (" + Random.Range(2, 67) + ")");
                    Instantiate(SpeedPickup, new Vector3(Node.transform.position.x, Node.transform.position.y, Node.transform.position.z), Quaternion.Euler(0, 180, 0));
                }
            }

            if (difficulty == 2)
            {
                doneOnce = true;
                EasyPath.gameObject.SetActive(false);
                HardPath.gameObject.SetActive(false);

                audio.clip = mediumSong;
                audio.Play();

                SpawnpointMedium = GameObject.Find("PlayerSpawnMedium").transform;
                GameObject InstantiatedPlayer = Instantiate(Player, new Vector3(SpawnpointMedium.position.x, SpawnpointMedium.position.y, SpawnpointMedium.position.z), Quaternion.Euler(0, 180, 0));
                InstantiatedPlayer.GetComponent<CarControl>().Path = MediumPath.transform;
                Camera.GetComponent<CameraFollowScript>().target = InstantiatedPlayer.transform;
                Instantiate(FinishLine, new Vector3(SpawnpointMedium.position.x, SpawnpointMedium.position.y - 0.5f, 68.021f), Quaternion.Euler(0, 90, 0));

                for (int i = 0; i < 4; i++)
                {
                    GameObject InstantiatedEnemy = Instantiate(Enemy, new Vector3(SpawnpointMedium.position.x, SpawnpointMedium.position.y, SpawnpointMedium.position.z + 2 + (2 * i)), Quaternion.Euler(0, 180, 0));
                    InstantiatedEnemy.GetComponent<AIControler>().Path = MediumPath.transform;
                }

                for (int i = 0; i < 6; i++)
                {
                    GameObject Node = GameObject.Find("Node (" + Random.Range(2, 79) + ")");
                    Instantiate(FuelPickup, new Vector3(Node.transform.position.x, Node.transform.position.y, Node.transform.position.z), Quaternion.Euler(0, 180, 0));
                }

                for (int i = 0; i < 4; i++)
                {
                    GameObject Node = GameObject.Find("Node (" + Random.Range(2, 67) + ")");
                    Instantiate(SpeedPickup, new Vector3(Node.transform.position.x, Node.transform.position.y, Node.transform.position.z), Quaternion.Euler(0, 180, 0));
                }
            }

            if (difficulty == 3)
            {
                doneOnce = true;
                EasyPath.gameObject.SetActive(false);
                MediumPath.gameObject.SetActive(false);

                audio.clip = hardSong;
                audio.Play();

                SpawnpointHard = GameObject.Find("PlayerSpawnHard").transform;
                GameObject InstantiatedPlayer = Instantiate(Player, new Vector3(SpawnpointHard.position.x, SpawnpointHard.position.y, SpawnpointHard.position.z), Quaternion.Euler(0, 270, 0));
                InstantiatedPlayer.GetComponent<CarControl>().Path = HardPath.transform;
                Camera.GetComponent<CameraFollowScript>().target = InstantiatedPlayer.transform;
                Instantiate(FinishLine, new Vector3(-41.51f, SpawnpointHard.position.y - 0.5f, -3.992f), Quaternion.Euler(0, 0, 0));

                for (int i = 0; i < 5; i++)
                {
                    GameObject InstantiatedEnemy = Instantiate(Enemy, new Vector3(SpawnpointHard.position.x + 2 + (2 * i), SpawnpointHard.position.y, SpawnpointHard.position.z + Random.Range(-4f, 4f)), Quaternion.Euler(0, 270, 0));
                    InstantiatedEnemy.GetComponent<AIControler>().Path = HardPath.transform;
                }

                for (int i = 0; i < 9; i++)
                {
                    GameObject Node = GameObject.Find("Node (" + Random.Range(2, 170) + ")");
                    Instantiate(FuelPickup, new Vector3(Node.transform.position.x, Node.transform.position.y, Node.transform.position.z), Quaternion.Euler(0, 180, 0));
                }

                for (int i = 0; i < 7; i++)
                {
                    GameObject Node = GameObject.Find("Node (" + Random.Range(2, 67) + ")");
                    Instantiate(SpeedPickup, new Vector3(Node.transform.position.x, Node.transform.position.y, Node.transform.position.z), Quaternion.Euler(0, 180, 0));
                }
            }
        }
    }

    public void Click()
    {
        if(this.gameObject.name == "EasyButton")
        {
            difficulty = 1;
            PlayerPrefs.SetInt("Degree", difficulty);
            SceneManager.LoadScene("GameScene");
        }

        if (this.gameObject.name == "MediumButton")
        {
            difficulty = 2;
            PlayerPrefs.SetInt("Degree", difficulty);
            SceneManager.LoadScene("GameScene");
        }

        if (this.gameObject.name == "HardButton")
        {
            difficulty = 3;
            PlayerPrefs.SetInt("Degree", difficulty);
            SceneManager.LoadScene("GameScene");
        }
    }
}
