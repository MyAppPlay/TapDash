using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shop : SoundBase
{
    public static Shop _instance;
    public int snailValue;

    public Text snailText;

    public GameObject[] buttonSnail;

    public bool IsloadVideo;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // Use this for initialization
    private void Start()
    {
        //PlayerPrefs.DeleteKey("Snail");
        snailValue = PlayerPrefs.GetInt("Snail");

        UpdateVideoPorn();
    }

    public void UpdateVideoPorn()
    {
        if (PlayerPrefs.GetInt("Snail") > 0)
        {
            snailText.text = "" + PlayerPrefs.GetInt("Snail");
            buttonSnail[1].SetActive(true);
            buttonSnail[0].SetActive(false);
        }
        if (PlayerPrefs.GetInt("Snail") == 0)
        {
            buttonSnail[0].SetActive(true);
            buttonSnail[1].SetActive(false);
        }
        if (snailValue <= 0)
        {
            snailValue = 0;
        }

        // ScoreManager._instance.GetTextDiamond();
    }

    public void MinusDiamond()
    {
        //  PlayerPrefs.GetInt("DiamondTotal");

        //  diamondValue -= 30;
        if (PlayerPrefs.GetInt("DiamondTotal") >= 200)
        {
            PlayerPrefs.SetInt("DiamondTotal", PlayerPrefs.GetInt("DiamondTotal") - 200);
            UpdateVideoPorn();
            snailValue += 2;
            PlayerPrefs.SetInt("Snail", snailValue);
            snailText.text = "" + PlayerPrefs.GetInt("Snail");
        }
        if (snailValue > 0)
        {
            buttonSnail[1].SetActive(true);
            buttonSnail[0].SetActive(false);
        }
        ScoreManager._instance.GetTextDiamond();
    }

    public IEnumerator Video()
    {
        yield return new WaitForSeconds(0.5f);
        IsloadVideo = true;
    }

    private void Update()
    {
        // load video ads
        if (IsloadVideo)
        {
            FreeVideo();
            IsloadVideo = false;
        }
    }

    public void FreeVideo()
    {
        // Demo._instance.ShowVideo();
        snailValue += 2;
        PlayerPrefs.SetInt("Snail", snailValue);

        UpdateVideoPorn();

        UIManager._instance._animGameOver.SetBool("SnailEarned", true);
        //   soundManager.OcSenGot();
        UIManager._instance.stateGameOVer[0].SetActive(false);
        UIManager._instance.stateGameOVer[2].SetActive(true);

        UIManager._instance.stateGameOVer[1].SetActive(false);
        StartCoroutine(BackToGameOver());

        if (snailValue > 0)
        {
            buttonSnail[1].SetActive(true);
            buttonSnail[0].SetActive(false);
        }
    }

    private IEnumerator BackToGameOver()
    {
        yield return new WaitForSeconds(2f);
        UIManager._instance._animGameOver.SetBool("SetSnail", true);
        //    UIManager._instance._animGameOver.
        UIManager._instance.stateGameOVer[2].SetActive(false);
        foreach (GameObject item in UIManager._instance.buttonArray)
        {
            item.SetActive(true);
        }
        UIManager._instance.zoomMap.SetActive(true);
        UIManager._instance._animGameOver.SetBool("SnailEarned", false);
        UIManager._instance.GameOverText.enabled = true;
        UIManager._instance.stateGameOVer[1].SetActive(false);
        UIManager._instance.stateGameOVer[2].SetActive(false);
        UIManager._instance._animGameOver.SetBool("Snail", false);
        //  UIManager._instance._animGameOver.Play("GameOverAnim");
    }

    // Update is called once per frame
}