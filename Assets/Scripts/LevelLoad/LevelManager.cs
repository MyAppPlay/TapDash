using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : SoundBase
{
    public static LevelManager _instance;

    [SerializeField]
    private GameObject HelpPanel;

    private int deactivePanelValue;

    //refer to level load
    public int _levelIndex;

    public GameObject[] Level;
    public int ValueMaxLevel;

    //  public int ValuePlayLevel = 0;
    public GameObject Canvas1, Canvas2;

    public GameObject cupGot;

    private void Awake()
    {
        //AudioListener.pause = false;
        // DontDestroyOnLoad(this.gameObject);
        //PlayerPrefs.DeleteAll();
        if (_instance == null)
        {
            _instance = this;
        }
        Canvas1.SetActive(false);
        Canvas2.SetActive(true);
        GetCup();
    }

    public Text diamondText;

    public void GetCup()
    {
        if (PlayerPrefs.GetFloat("ValueMaxLevel") >= PlayerPrefs.GetInt("ValuePlayLevel") + 1)
        {
            cupGot.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("ValueMaxLevel") < PlayerPrefs.GetInt("ValuePlayLevel"))
        {
            cupGot.SetActive(false);
        }
    }

    private GameObject Flag;
    public bool Saving;

    private void DestroyFlagOutMap()
    {
        Flag = GameObject.FindGameObjectWithTag("Flag");
        Destroy(Flag);
        UIManager._instance.CountFlag = 0;
        //
    }

    // map
    public void LoadMapLevel()
    {
        //set the first check instance flag
        PlayerPrefs.SetInt("First", 10);

        DestroyFlagOutMap();
        PlayerPrefs.SetInt("CheckFlag", 1);
        ScrollSnapRect._instance.Setpage();
        //UIManager._instance.CountFlag++;
        //banner destroy
        //Demo._instance.hideBanner();
        //
        UIManager._instance.CountBigBannerShow = 0;
        ////
        if (Saving)
        {
            PlayerPrefsX.SetVector3("PosFlag", UIManager._instance.posSaveDiamond);
            //    Debug.Log(UIManager._instance.CountFlag);
            Saving = false;
        }

        MenuScripts._instance.levelTextToPlay.text = "Play level: " + PlayerPrefs.GetInt("ValueMaxLevel");
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        ScrollSnapRect._instance.CheckScroll = true;
        GetLevel();
        diamondText.text = "" + PlayerPrefs.GetInt("DiamondTotal");

        ChangeColorCamera._instance.SwitchColor(9);
        ChangeColorCamera._instance.zoomIn = true;
        UIManager._instance.GameOverShownPanel.SetActive(false);
        GameManager._instance.DesTroyer = true;
        GameManager._instance.DestroyerLevel();
        Canvas1.SetActive(false);
        Canvas2.SetActive(true);
        DisableEverything();
    }

    private IEnumerator DeactiveCanvas2()
    {
        yield return new WaitForSeconds(0.05f);
        Canvas2.SetActive(false);
    }

    private void Start()
    {
        GetLevel();
        HelpPanel.SetActive(false);
        deactivePanelValue = PlayerPrefs.GetInt("HelpPanel");
        if (PlayerPrefs.GetInt("HelpPanel") == 1)
        {
            HelpPanel.SetActive(false);
        }
        //PlayerPrefs.SetInt("ValueMaxLevel", 132);
        //PlayerPrefs.SetInt("ValuePlayLevel", 131);
    }

    public GameObject LevelComplete;

    public void LevelCompelte()
    {
        LevelComplete.SetActive(false);
        Canvas1.SetActive(false);
        Canvas2.SetActive(true);

        GameManager._instance.DesTroyer = true;
        GameManager._instance.DestroyerLevel();
    }

    public void DeactiveHelpPanel()
    {
        deactivePanelValue = 1;
        PlayerPrefs.SetInt("HelpPanel", deactivePanelValue);
        HelpPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator ActiveHelpPanel()
    {
        yield return new WaitForSeconds(0.3f);
        HelpPanel.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSeconds(0.2f);
        PlayerController._instance.isClick = true;
    }

    public void GetLevel()
    {
        if (!PlayerPrefs.HasKey("ValueMaxLevel"))
        {
            PlayerPrefs.SetInt("ValueMaxLevel", 1);
            PlayerPrefs.SetInt("ValuePlayLevel", 0);
        }

        diamondText.text = "" + PlayerPrefs.GetInt("DiamondTotal");

        ValueMaxLevel = PlayerPrefs.GetInt("ValueMaxLevel");
        PlayerPrefs.Save();

        Level[ValueMaxLevel - 1].transform.Find("Avata").gameObject.SetActive(true);

        // unlock level from level 1 unlock key when collection diamonds
        for (int i = 0; i < ValueMaxLevel; i++)
        {
            Level[i].transform.Find("KeyLevel").gameObject.SetActive(false);

            if (PlayerPrefs.GetString("ItemsLevel" + (i + 1)) == "True")
            {
                Level[i].transform.Find("ButtonLevel").Find("Key").GetComponent<Image>().gameObject.SetActive(true);
            }
        }
    }

    // truyen bien vao levelLoader de load level tu menu 1 -43
    public void Selectlevel(int levelIndex)
    {
        if (deactivePanelValue != 1)
        {
            StartCoroutine(ActiveHelpPanel());
        }
        else
        {
            HelpPanel.SetActive(false);
        }
        //Demo._instance.showBanner();
        UIManager._instance.UpdateText(levelIndex);

        // disable all level panel
        ScrollSnapRect._instance.disableALl();
        // show ads
        ScrollSnapRect._instance.CheckScroll = false;
        //
        // DisableEverything();

        StartCoroutine(DeactiveCanvas2());
        Canvas1.SetActive(true);
        //
        _levelIndex = levelIndex;
        UIManager._instance.GameOverShownPanel.SetActive(false);

        // load level set to level load

        StartCoroutine(loadScene(levelIndex));

        PlayerPrefs.SetInt("ValuePlayLevel", levelIndex - 1);

        PlayerPrefs.Save();
        StartCoroutine(UIManager._instance.findObject());
    }

    public void SelectlevelMax()
    {
        // disable all level panel
        ScrollSnapRect._instance.disableALl();
        UIManager._instance.UpdateText(PlayerPrefs.GetInt("ValueMaxLevel"));
        if (deactivePanelValue != 1)
        {
            StartCoroutine(ActiveHelpPanel());
        }
        else if (deactivePanelValue == 1)
        {
            HelpPanel.SetActive(false);
        }
        //Demo._instance.showBanner();

        // show ads
        ScrollSnapRect._instance.CheckScroll = false;
        //

        StartCoroutine(DeactiveCanvas2());
        Canvas1.SetActive(true);

        _levelIndex = PlayerPrefs.GetInt("ValueMaxLevel");
        StartCoroutine(LoadMaxLevel(_levelIndex));
        //deactive help panel

        StartCoroutine(UIManager._instance.findObject());
        // PlayerPrefs.Save();
    }

    public void DisableEverything()
    {
        RotationCamera._instance._animCam.SetBool("Left", false);
        RotationCamera._instance._animCam.SetBool("Right", false);
        RotationCamera._instance._animCam.Play("Idle");

        RotationCamera._instance._animCam.Play("Idle");
        ChangeColorCamera._instance.transition = false;
        ChangeColorCamera._instance.zoomIn = true;
        ChangeColorCamera._instance.isZoomIn = false;
        ChangeColorCamera._instance.isZoomOut = false;
        //
        RotationCamera._instance.rayShoot = false;
        RotationCamera._instance.rayShootNguoc = false;
        //zoom
        RotationCamera._instance.isZoomin = false;
        RotationCamera._instance.isZoomOut = false;
        //
        UIManager._instance.GameOverShownPanel.SetActive(false);
    }

    private IEnumerator LoadMaxLevel(int index)

    {
        yield return new WaitForSeconds(0.1f);

        int number = PlayerPrefs.GetInt("ValueMaxLevel");

        LevelLoader._instance.InitiateLevel("Level" + number.ToString());
    }

    private IEnumerator loadScene(int index)
    {
        yield return new WaitForSeconds(0.1f);
        LevelLoader._instance.InitiateLevel("Level" + index.ToString());
    }
}