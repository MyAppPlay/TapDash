using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SoundBase
{
    public static UIManager _instance;

    public float timerDeactiveGameOver;

    public GameObject GameOverShownPanel;

    [SerializeField]
    public Animator _animGameOver;

    [HideInInspector]
    public bool isSHOW;

    public GameObject player;
    private Transform levelTransform;

    [HideInInspector]
    public GameObject playerSpwan, playerSpawnRandom, playerSpawnSnail;

    private Transform level;

    public Text levelText;
    public Button resetBt;
    public Button ZoomBT;

    //  public Animator _gameOVerAnim;
    public GameObject ScoreIMAGE;

    public GameObject zoomMap;

    public GameObject[] buttonArray;

    public Text GameOverText;

    public GameObject[] stateGameOVer;

    public GameObject FlagDead;

    [HideInInspector]
    public Vector3 posSaveDiamond;

    [HideInInspector]
    public bool isCheckGetPositionFlag;

    [HideInInspector]
    public Vector3 savingPositionFlags;

    [HideInInspector]
    public int CheckFirstPlay;

    // count banner show
    [HideInInspector]
    public int CountBigBannerShow;

    // set count flag saving
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void UpdateText(int value)
    {
        levelText.text = +value + "/250";
    }

    // Use this for initialization
    private void Start()
    {
        savingPositionFlags = PlayerPrefsX.GetVector3("PosFlag");
        stateGameOVer[0].SetActive(false);
        stateGameOVer[1].SetActive(false);
        stateGameOVer[2].SetActive(false);
        resetBt.interactable = false;

        _animGameOver = GameOverShownPanel.GetComponent<Animator>();
        GameOverShownPanel.SetActive(false);
        CheckFirstPlay = PlayerPrefs.GetInt("CheckFlag");
    }

    // to levelmanagel
    [HideInInspector]
    public Vector3 pos;

    [HideInInspector]
    public bool isInstancePosFlag;

    //private void instanceFlag()
    //{
    //}
    [HideInInspector]
    public bool CheckFirstInstanceFlag;

    public IEnumerator findObject()
    {
        yield return new WaitForSeconds(0.1f);
        FollowCamera._instance.isFollow = true;

        if (PlayerPrefs.GetInt("CheckFlag") == 1)
        {
            CountFlag++;
            Instantiate(FlagDead, PlayerPrefsX.GetVector3("PosFlag"), Quaternion.identity);
        }

        // check
        isCheckGetPositionFlag = true;
        level = GameObject.FindGameObjectWithTag("Level").transform;
        // set leveltransform equal postion found it
        levelTransform = level.GetComponentInChildren<Transform>();
        levelTransform = GameObject.FindGameObjectWithTag("LevelUp").transform;

        // make local out side from position
        Vector3 local = levelTransform.transform.localPosition;
        //set pos equal local
        pos = local;
        pos = levelTransform.GetComponent<Collider2D>().bounds.center;
        pos.y -= 0.5f;
        //instan player and set position found
        // Transform t =
        RandomOrNoRandom();
        pos.z -= 10f;
        RotationCamera._instance.mainCamera.transform.position = pos;
    }

    public GameObject Playerr;

    // show game over when dead
    public void GameOverShow()
    {
        ScoreManager._instance.GetTextDiamond();
        FollowCamera._instance.isFollow = false;
        ZoomBT.interactable = true;
        StartCoroutine(SetActiveButton());
        RotationCamera._instance.Lerp = false;
        RotationCamera._instance.roration = false;
        ChangeColorCamera._instance.isZoomIn = false;
        ChangeColorCamera._instance.isZoomOut = false;
        StartCoroutine(ShowGameOver());
    }

    // time show gameover
    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(0.4f);
        ChangeColorCamera._instance.transition = true;
        if (GameOverShownPanel != null)
        {
            soundManager.BirdDead();
            CountBigBannerShow++;
            if (CountBigBannerShow >= 3)
            {
                CountBigBannerShow = 0;
                //Demo._instance.showIntertitial();
            }
            soundManager.GameOverShow();
            GameOverShownPanel.SetActive(true);
            //Demo._instance.hideBanner();
        }
    }

    // zoom map when game over

    public void ZoomMapGameOver()
    {
        isSHOW = !isSHOW;
        if (isSHOW)
        {
            ChangeColorCamera._instance.SnailColor = false;
            ChangeColorCamera._instance.timeMapIn = 0f;
            soundManager.ZoomMapOverPanel();
            ChangeColorCamera._instance.isZoomOut = true;
            ChangeColorCamera._instance.isZoomIn = false;
            _animGameOver.SetBool("ZoomMapBool", true);

            StartCoroutine(SetActiveScoreImg(false));
            foreach (GameObject item in buttonArray)
            {
                item.SetActive(false);
            }
        }
        else
        {
            ChangeColorCamera._instance.timeMapout = 0f;
            soundManager.ZoomMapOverPanel();
            ChangeColorCamera._instance.isZoomIn = true;
            ChangeColorCamera._instance.isZoomOut = false;
            _animGameOver.SetBool("ZoomMapBool", false);

            StartCoroutine(SetActiveScoreImg(true));
            foreach (GameObject item in buttonArray)
            {
                item.SetActive(true);
            }
        }
    }

    private IEnumerator SetActiveScoreImg(bool set)
    {
        yield return new WaitForSeconds(0.1f);

        ScoreIMAGE.SetActive(set);
    }

    // set active game over when game over
    public IEnumerator SetActiveGameOver(float time)
    {
        yield return new WaitForSeconds(time);

        GameOverShownPanel.SetActive(false);
    }

    public IEnumerator SetActiveButton()
    {
        yield return new WaitForSeconds(1f);

        resetBt.interactable = true;
    }

    [HideInInspector]
    public bool Checked;

    [HideInInspector]
    public bool CheckRandomSpawn;

    public void SpawnPlayer(Vector3 col)
    {
        if (Checked)
        {
            for (int i = 0; i < 6; i++)
            {
                if (PlayerPrefs.GetInt("Check") > 1 && PlayerPrefs.GetInt("Choosed" + i) == 1)
                {
                    CharacterManager._instance.CheckRandom = true;
                    CharacterManager._instance.RandomChar();
                }
                if (PlayerPrefs.GetInt("Check") == 1 && PlayerPrefs.GetInt("Choosed" + i) == 1)
                {
                    playerSpwan = Resources.Load("Prefabs/Character/" + i + "") as GameObject;
                }
            }
            Checked = false;
        }

        GameObject go = (GameObject)Instantiate(playerSpwan, col, Quaternion.identity);
        go.name = go.name.Replace("(Clone)", "");
    }

    public void SpawnPlayerResetButton(Vector3 col)
    {
        if (CheckRandomSpawn)
        {
            Checked = false;
            playerSpawnRandom = Resources.Load("Prefabs/Character/" + PlayerPrefs.GetInt("RandomNumber").ToString() + "") as GameObject;
            GameObject go = (GameObject)Instantiate(playerSpawnRandom, col, Quaternion.identity);
            go.name = go.name.Replace("(Clone)", "");
        }
    }

    public void InStanceSnail(Vector3 col)
    {
        CheckRandomSpawn = false;
        Checked = false;
        playerSpawnSnail = Resources.Load("Prefabs/Character/OcSen") as GameObject;
        GameObject go = (GameObject)Instantiate(playerSpawnSnail, col, Quaternion.identity);
        go.name = go.name.Replace("(Clone)", "");
    }

    // Spawn Level when dead
    public IEnumerator IsSpawn()
    {
        yield return new WaitForSeconds(0.001f);
        GameManager._instance.LoadLevelAgain(true);
        soundManager.LevelUp(0f);
    }

    public void LoadMapLevel()
    {
    }

    // play again button
    public void PlayerAgain()
    {
        Invoke("Reset", 0.05f);
        //Reset();
    }

    // button select character
    public void SelectCharacter()
    {
        stateGameOVer[0].SetActive(true);
        stateGameOVer[1].SetActive(false);
        stateGameOVer[2].SetActive(false);
        ScoreIMAGE.SetActive(false);
        _animGameOver.SetBool("Character", true);
        zoomMap.SetActive(false);
        StartCoroutine(DeativeGameOVer());
    }

    //deative from slectcharacter
    private IEnumerator DeativeGameOVer()
    {
        yield return new WaitForSeconds(0.4f);
        foreach (GameObject item in buttonArray)
        {
            item.SetActive(false);
        }
    }

    //button get snail

    public void GetSnail()
    {
        if (PlayerPrefs.GetInt("Snail") > 0)
        {
            //flag
            InstanceFlag();
            //
            PlayerController._instance.CheckColor = false;
            RotationCamera._instance._animCam.SetBool("Left", false);
            RotationCamera._instance._animCam.SetBool("Right", false);
            RotationCamera._instance._animCam.Play("Idle");

            PlayerController._instance.isClick = true;
            ScrollSnapRect._instance.CheckScroll = false;
            Shop._instance.snailValue--;
            PlayerPrefs.SetInt("Snail", Shop._instance.snailValue);

            Shop._instance.snailText.text = "" + PlayerPrefs.GetInt("Snail");
            Shop._instance.UpdateVideoPorn();

            GameManager._instance.DesTroyer = true;
            GameManager._instance.DestroyerLevel();

            GameManager._instance.LoadLevelAgain(true);

            ChangeColorCamera._instance.SwitchColor(8);

            Checked = false;
            CharacterManager._instance.CheckRandom = false;
            InStanceSnail(pos);

            //update select from character manager

            // update random from character manager

            // zoom map from ui.......
            zoomMap.SetActive(true);
            ZoomBT.interactable = false;
            //deactive button reset no found
            resetBt.interactable = false;
            //setactive resetbutton
            //same thing on up

            //
            FollowCamera._instance.isDead = true;
            //
            RotationCamera._instance.rayShoot = false;
            RotationCamera._instance.rayShootNguoc = false;
            //zoom
            RotationCamera._instance.isZoomin = false;
            RotationCamera._instance.isZoomOut = false;

            // call is spawn

            RotationCamera._instance._animCam.Play("Idle");
            ChangeColorCamera._instance.transition = false;
            ChangeColorCamera._instance.zoomIn = true;
            //  forwardTransition = true;
            // set animation
            _animGameOver.SetTrigger("GameOver");
            // deactive game over panel
            StartCoroutine(SetActiveGameOver(timerDeactiveGameOver));

            ChangeColorCamera._instance.isZoomIn = false;
            ChangeColorCamera._instance.isZoomOut = false;
        }
        else if (PlayerPrefs.GetInt("Snail") == 0)
        {
            stateGameOVer[0].SetActive(false);
            stateGameOVer[1].SetActive(true);
            stateGameOVer[2].SetActive(false);
            GameOverText.enabled = false;
            ScoreIMAGE.SetActive(true);
            zoomMap.SetActive(false);
            _animGameOver.SetBool("Snail", true);
            foreach (GameObject item in buttonArray)
            {
                item.SetActive(false);
            }
            buttonArray[7].SetActive(true);
        }
    }

    //back from get snail
    public void BackToGameOverFromSnail()
    {
        //update how many snail from button video
        Shop._instance.UpdateVideoPorn();
        ScoreIMAGE.SetActive(true);
        GameOverText.enabled = true;
        _animGameOver.SetBool("Snail", false);
        StartCoroutine(DeactiveSnail());
        zoomMap.SetActive(true);
        ZoomBT.interactable = true;
        foreach (GameObject item in buttonArray)
        {
            item.SetActive(true);
        }
    }

    private IEnumerator DeactiveSnail()
    {
        yield return _animGameOver.GetBool("Snail");
        stateGameOVer[1].SetActive(true);
    }

    private IEnumerator DeactiveFace()
    {
        yield return new WaitForSeconds(0.6f);
        stateGameOVer[0].SetActive(false);
    }

    // back to reset panel
    public void BackToMenuReset()
    {
        if (CharacterManager._instance.CheckRandom)
        {
            CharacterManager._instance.checkFace = false;

            CharacterManager._instance.RandomChar();
        }
        if (CharacterManager._instance.checkFace)
        {
            CheckRandomSpawn = false;
            CharacterManager._instance.CheckRandom = false;

            CharacterManager._instance.CheckFace();
            CharacterManager._instance.UpdateSelect();
        }
        ScoreIMAGE.SetActive(true);
        _animGameOver.SetBool("Character", false);
        StartCoroutine(DeactiveFace());
        zoomMap.SetActive(true);
        foreach (GameObject item in buttonArray)
        {
            item.SetActive(true);
        }
    }

    // check if many choosed is random, else only choose 1 face chosed
    public void RandomOrNoRandom()
    {
        if (PlayerPrefs.GetInt("Check") > 1 && PlayerPrefs.GetInt("ValuePlayLevel") > 0)
        {
            //  pos = CharacterSnail._instance.pos;
            CheckRandomSpawn = true;
            SpawnPlayerResetButton(pos);
        }
        if (PlayerPrefs.GetInt("Check") == 1 && PlayerPrefs.GetInt("ValuePlayLevel") > 0)
        {
            // pos = CharacterSnail._instance.pos;
            Checked = true;
            SpawnPlayer(pos);
        }
        //check if level is 1 --  only instan bird
        if (PlayerPrefs.GetInt("ValuePlayLevel") == 0)
        {
            Checked = false;
            CheckRandomSpawn = false;
            CharacterManager._instance.CheckRandom = false;
            CharacterManager._instance.checkFace = false;
            playerSpwan = Resources.Load("Prefabs/Character/0") as GameObject;
            SpawnPlayer(pos);
        }

        //  Debug.Log(pos);
    }

    //reset button

    public void InstanceFlag()
    {
        //count times instaniate this one if ==1 is instantiate else more than 1 no instance
        CountFlag++;

        if (isInstancePosFlag && CountFlag == 1)
        {
            PlayerPrefsX.SetVector3("PosFlag", posSaveDiamond);
            Instantiate(FlagDead, PlayerPrefsX.GetVector3("PosFlag"), Quaternion.identity);
        }
        else if (CountFlag > 1)
        {
            isInstancePosFlag = false;
        }
    }

    [HideInInspector]
    public int CountFlag;

    public void Reset()
    {
        //Demo._instance.showBanner();

        InstanceFlag();

        RandomOrNoRandom();

        // PlayerController._instance.CheckColor = true;
        //
        PlayerController._instance.isClick = true;
        FollowCamera._instance.isFollow = true;
        // BG Snail
        //CharacterManager._instance.CheckRandom = true;
        ChangeColorCamera._instance.BG[0].SetActive(true);
        ChangeColorCamera._instance.BG[1].SetActive(false);

        //reset diamond
        ScoreManager._instance._diamond = 0;
        //update select from character manager
        CharacterManager._instance.UpdateSelect();

        //
        RotationCamera._instance._animCam.SetBool("Left", false);
        RotationCamera._instance._animCam.SetBool("Right", false);

        // zoom map from ui.......
        zoomMap.SetActive(true);

        //deactive button reset no found
        resetBt.interactable = false;
        //setactive resetbutton
        //same thing on up
        ZoomBT.interactable = false;
        //
        FollowCamera._instance.isDead = true;

        //zoom
        RotationCamera._instance.isZoomin = false;
        RotationCamera._instance.isZoomOut = false;

        // call is spawn
        // RotationCamera._instance.camera.GetComponent<Camera>().
        RotationCamera._instance._animCam.Play("Idle");
        StartCoroutine(IsSpawn());
        ScoreManager._instance.ResetScore();
        GameManager._instance.DesTroyer = true;
        GameManager._instance.DestroyerLevel();
        // spawn player in position center get from player

        // change color camera
        ChangeColorCamera._instance.transition = false;
        ChangeColorCamera._instance.zoomIn = true;

        // set animation
        _animGameOver.SetTrigger("GameOver");
        // deactive game over panel
        StartCoroutine(SetActiveGameOver(timerDeactiveGameOver));
        //set false
        ChangeColorCamera._instance.isZoomIn = false;
        ChangeColorCamera._instance.isZoomOut = false;
    }
}