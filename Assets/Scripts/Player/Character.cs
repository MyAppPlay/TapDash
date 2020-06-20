using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character _instance;

    // Use this for initialization
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "UnLock")
        {
            GameManager._instance.posNew = transform.position;

            if (PlayerPrefs.GetInt("ValueMaxLevel") == 2 && PlayerPrefs.GetInt("Unlocked0") == 0)
            {
                GameManager._instance.playerSpawn = Resources.Load("Prefabs/Character/1") as GameObject;
                GameManager._instance.inStanceNew = true;
                Destroy(gameObject);
                // unlock check no unlock
                // CharacterManager._instance.Unlock0 = 1;
                PlayerPrefs.SetInt("Unlocked0", 1);
            }
            if (PlayerPrefs.GetInt("ValueMaxLevel") == 5 && PlayerPrefs.GetInt("Unlocked1") == 0)
            {
                GameManager._instance.playerSpawn = Resources.Load("Prefabs/Character/2") as GameObject;
                GameManager._instance.inStanceNew = true;
                Destroy(gameObject);
                PlayerPrefs.SetInt("Unlocked1", 1);
            }
            if (PlayerPrefs.GetInt("ValueMaxLevel") == 10 && PlayerPrefs.GetInt("Unlocked2") == 0)
            {
                GameManager._instance.playerSpawn = Resources.Load("Prefabs/Character/3") as GameObject;
                GameManager._instance.inStanceNew = true;
                Destroy(gameObject);
                PlayerPrefs.SetInt("Unlocked2", 1);
            }
            if (PlayerPrefs.GetInt("ValueMaxLevel") == 15 && PlayerPrefs.GetInt("Unlocked3") == 0)
            {
                //PlayerPrefs.SetInt("ValueMaxLevel", 4);

                GameManager._instance.playerSpawn = Resources.Load("Prefabs/Character/4") as GameObject;
                GameManager._instance.inStanceNew = true;
                Destroy(gameObject);
                PlayerPrefs.SetInt("Unlocked3", 1);
            }
            if (PlayerPrefs.GetInt("ValueMaxLevel") == 17 && PlayerPrefs.GetInt("Unlocked4") == 0)
            {
                //PlayerPrefs.SetInt("ValueMaxLevel", 4);

                GameManager._instance.playerSpawn = Resources.Load("Prefabs/Character/5") as GameObject;
                GameManager._instance.inStanceNew = true;
                Destroy(gameObject);
                PlayerPrefs.SetInt("Unlocked4", 1);
            }
            if (PlayerPrefs.GetInt("ValueMaxLevel") == 20 && PlayerPrefs.GetInt("Unlocked5") == 0)
            {
                //PlayerPrefs.SetInt("ValueMaxLevel", 4);

                GameManager._instance.playerSpawn = Resources.Load("Prefabs/Character/6") as GameObject;
                GameManager._instance.inStanceNew = true;
                Destroy(gameObject);
                PlayerPrefs.SetInt("Unlocked5", 1);
            }
            if (PlayerPrefs.GetInt("ValueMaxLevel") == 23 && PlayerPrefs.GetInt("Unlocked6") == 0)
            {
                //PlayerPrefs.SetInt("ValueMaxLevel", 4);

                GameManager._instance.playerSpawn = Resources.Load("Prefabs/Character/7") as GameObject;
                GameManager._instance.inStanceNew = true;
                Destroy(gameObject);
                PlayerPrefs.SetInt("Unlocked6", 1);
            }
            if (PlayerPrefs.GetInt("ValueMaxLevel") == 25 && PlayerPrefs.GetInt("Unlocked7") == 0)
            {
                //PlayerPrefs.SetInt("ValueMaxLevel", 4);

                GameManager._instance.playerSpawn = Resources.Load("Prefabs/Character/8") as GameObject;
                GameManager._instance.inStanceNew = true;
                Destroy(gameObject);
                PlayerPrefs.SetInt("Unlocked7", 1);
            }
        }
    }
}