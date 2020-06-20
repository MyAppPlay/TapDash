using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class MenuScripts : MonoBehaviour
{
    public static MenuScripts _instance;

    public Animator _animMenu;
    public GameObject levelPanel;

    public GameObject soundOff;
    public GameObject soundOn;

    public GameObject OpitionsMenu;
    public GameObject MenuLevel;
    public GameObject MenuMain;
    public Text levelTextToPlay;

    //count ads
    private int count;

    private void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        levelTextToPlay.text = "Play level: " + PlayerPrefs.GetInt("ValueMaxLevel");
        OpitionsMenu.SetActive(false);
        MenuLevel.SetActive(false);
        MenuMain.SetActive(true);
        //_animMenu.SetBool("Menu", true);

        //_animation = panel.GetComponent<Animation>();
        CharacterManager._instance.unlock = true;


        for (int i = 0; i < 8; i++)
        {
            PlayerPrefs.GetInt("Choosed" + i);
        }


    }

    public void BackToMenu()
    {
        //Demo._instance.showIntertitial();
        StartCoroutine(setActive(MenuLevel, 0.5f));
        MenuMain.SetActive(true);
        OpitionsMenu.SetActive(false);
        _animMenu.SetBool("Back", true);
        _animMenu.SetBool("Level", false);
    }

    public void NextToOptions()
    {
        //   Demo._instance.ShowVideo();
        OpitionsMenu.SetActive(true);
        StartCoroutine(setActive(MenuMain, 0.5f));
        levelPanel.SetActive(false);
        MenuLevel.SetActive(false);
        _animMenu.SetBool("Options", true);
    }

    public void BackToMenuFromOptionos()
    {
        MenuMain.SetActive(true);
        StartCoroutine(setActive(OpitionsMenu, 0.5f));
        //MenuLevel.SetActive(true);
        _animMenu.SetBool("Options", false);
    }

    public void BackToLevelPanel()
    {
        count++;
        if (count == 4)
        {
            //Demo._instance.showIntertitial();
            count = 0;
        }

        //Demo._instance.showIntertitial();
        //  }

        MenuLevel.SetActive(true);
        StartCoroutine(setActive(MenuMain, 0.5f));
        OpitionsMenu.SetActive(false);
        _animMenu.SetBool("Level", true);
    }

    private IEnumerator setActive(GameObject gameObj, float time)
    {
        yield return new WaitForSeconds(time);
        gameObj.SetActive(false);
    }
}