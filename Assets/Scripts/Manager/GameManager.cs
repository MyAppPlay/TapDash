using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : SoundBase
{
    public static GameManager _instance;

    //refer to level string from player
    [HideInInspector]
    public string levelToLoadAgain;

    public GameObject xDead;
    private GameObject Flag;

    [HideInInspector]
    public GameObject[] Level;

    [HideInInspector]
    public bool DesTroyer;

    [HideInInspector]
    public bool isDead;

    public Text[] text;

    public GameObject AppStoreBT, AppStoreBt2, GoogleStoreBT, GoogleStoreBT2;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        UpdateText();
        LoadBTStore();
    }

    // load level get pos from player dead
    public void LoadLevelAgain(bool isLoadLevel)
    {
        if (isLoadLevel)
        {
            LevelLoader._instance.InitiateLevel(levelToLoadAgain);
        }
    }

    private void LoadBTStore()
    {
#if UNITY_ANDROID
        AppStoreBT.SetActive(false);
        AppStoreBt2.SetActive(false);
        GoogleStoreBT.SetActive(true);
        GoogleStoreBT2.SetActive(true);
#endif
#if UNITY_IPHONE
        AppStoreBT.SetActive(true);
        AppStoreBt2.SetActive(true);
        GoogleStoreBT.SetActive(false);
        GoogleStoreBT2.SetActive(false);
#endif
    }

    public void UpdateText()
    {
        foreach (Text item in text)
        {
            item.text = "" + PlayerPrefs.GetInt("ValueMaxLevel");
        }
    }

    private Vector3 pos;

    public void Reatappstore()
    {
        Application.OpenURL("https://itunes.apple.com/us/developer/du-tan/id1121097860");
    }

    public void Reatagpstore()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ecode.AnimalLeap");
    }

    // get positinon xdead and instan
    public void InstanceXDead()
    {
        pos = PlayerController._instance.posSaveLevel;

        Instantiate(xDead, pos, Quaternion.identity);
        // Debug.Log(pos);
    }

    public bool isInstance;
    public bool inStanceNew;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        // destroylevel when call bool destroyer on button replay reset
        // DestroyerLevel();
        if (isDead)
        {
            // instan xdead
            InstanceXDead();
            isDead = false;
        }
        if (isInstance)
        {
            // tru 1 vi 2 nhan vat cham vao ve dich nen +2;;
            //  ChangeColorCamera._
            int a = PlayerPrefs.GetInt("ValuePlayLevel") - 1;
            PlayerPrefs.SetInt("ValuePlayLevel", a);
            //random neu la 1

            //
            UIManager._instance.RandomOrNoRandom();

            isInstance = false;
        }
        if (inStanceNew)
        {
            Instantiate(playerSpawn, posNew, Quaternion.identity);
            inStanceNew = false;
        }
    }

    [HideInInspector]
    public GameObject playerSpawn;

    [HideInInspector]
    public Vector3 posNew;

    //destroy all level when reset
    public void DestroyerLevel()
    {
        if (!DesTroyer) return;
        Level = GameObject.FindGameObjectsWithTag("Level");
        foreach (GameObject item in Level)
        {
            //  DesTroyer = true;
            Destroy(item);
            DesTroyer = false;
        }
    }
}