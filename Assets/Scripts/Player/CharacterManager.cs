using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager _instance;
    public GameObject[] characters;

    [HideInInspector]
    private int[] CharacterValue;

    [HideInInspector]
    private bool[] click;

    // check playpref 1 is true 0 is false

    private int count;

    [HideInInspector]
    public bool unlock;

    public int aa;
    private int vavlueLevel;

    public Sprite[] characterSprite;

    // button choose
    public Image thisSprite, sprite2, sprite3, sprite4;

    // face unlock when get level
    public Image faceUnlock;

    public Image faceUnlock2;

    //
    public AudioClip[] characterClip;

    public AudioSource _audioSource;
    public AudioClip heroUnlock;
    public int CheckHero;
    public GameObject FaceUnLockLevel;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        // isCheck = true;
    }

    private int[] Unlock = new int[20];

    private void Start()
    {
        FaceUnLockLevel.gameObject.SetActive(false);
        //if (PlayerPrefs.GetInt("ValueMaxLevel") == 1)
        //{
        //    characters[0].transform.FindChild("Lock").GetComponent<Image>().gameObject.SetActive(false);
        //    PlayerPrefs.SetInt("Choosed0", 1);
        //}

        //CheckSound();
        aa = PlayerPrefs.GetInt("RandomNumber");
        //  UIManager._instance.playerSpwan = player;

        unlock = true;
        //  CheckRandom = true;
        count = PlayerPrefs.GetInt("Check");
        // PlayerPrefs.DeleteKey("FBCharacter");
        CharacterValue = new int[20];
        if (!CheckRandom)
        {
            checkFace = true;
            CheckRandom = false;
            CheckFace();
        }
        if (!checkFace)
        {
            CheckRandom = true;
            checkFace = false;
            RandomChar();
        }

        for (int i = 0; i < Unlock.Length; i++)
            Unlock[i] = PlayerPrefs.GetInt("Unlocked" + i, 0);

        LockCharacter();

        click = new bool[20];
        UpdateSelect();
        for (int i = 0; i < characters.Length; i++)
            if ((PlayerPrefs.GetInt("Choosed" + i) == 0) && (count == 0))
            {
                //PlayerPrefs.SetInt("Check", 1);
                count = 1;

                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed0", 1);
                characters[0].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
            }
    }

    public bool checkFace;

    public void CheckFace()
    {
        if (checkFace)
            for (int i = 0; i < characters.Length; i++)
                if (PlayerPrefs.GetInt("Choosed" + i) == 1)
                    thisSprite.sprite = characterSprite[i];
    }

    public bool CheckRandom;

    // random character
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("LevelGame");
            //   UnityEngine.SceneManagement.LoadScene("LevelGame");
        }
        //z   Debug.Log(count);
    }

    private int random;

    // random charactere
    public void RandomChar()
    {
        if (CheckRandom)
        {        //CheckRandom = true;
            checkFace = false;

            GameObject objectRan = _gameObjlist[Random.Range(0, _gameObjlist.Count)];

            //   aa = gameObjlist.IndexOf(objectRan);
            string bbb = objectRan.name;

            aa = int.Parse(bbb);

            PlayerPrefs.SetInt("RandomNumber", aa);
            thisSprite.sprite = characterSprite[aa];

            // random from 0 to
        }
    }

    // update state choose from character
    public void UpdateSelect()
    {
        for (int j = 0; j < characters.Length; j++)
        {
            CharacterValue[j] = PlayerPrefs.GetInt("Choosed" + j);
            if (CharacterValue[j] == 1)
            {
                click[j] = true;
                _gameObjlist.Add(Resources.Load("Prefabs/Character/" + j + "") as GameObject);
                characters[j].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/" + j + "") as GameObject;
            }
            else if (CharacterValue[j] == 0)
            {
                // gameObjlist.RemoveAt(j);
                click[j] = false;
                characters[j].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
            }
        }
        if (PlayerPrefs.GetInt("Check") == 1)
            CheckRandom = false;
    }

    // button select 0

    #region vùng này để check button click

    private List<GameObject> _gameObjlist = new List<GameObject>();

    public void SelectCharacter0()
    {
        checkFace = true;
        click[0] = !click[0];
        bool isCheck = true;
        if (isCheck)
            if (click[0] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/0") as GameObject);

                _audioSource.clip = characterClip[Random.Range(0, 3)];
                _audioSource.Play();
                count++;
                //  count = 0;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed0", 1);
                characters[0].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
            }
        // if count value is 1 stop plus value cont
        if (count == 1)
        {
            isCheck = false;
            click[0] = true;
        }
        else if (!click[0] && (count > 1))
        {
            _gameObjlist.Remove(Resources.Load("Prefabs/Character/0") as GameObject);
            _gameObjlist.Clear();

            count--;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed0", 0);
            characters[0].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
            PlayerPrefs.Save();
        }

        UpdateSelect();
    }

    // next by next same things
    public void SelectCharacter1()
    {
        checkFace = true;
        click[1] = !click[1];
        bool isCheck = true;
        if (isCheck)
            if (click[1] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/1") as GameObject);

                _audioSource.clip = characterClip[Random.Range(3, 6)];
                _audioSource.Play();
                count++;
                //  count = 0;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed1", 1);
                characters[1].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/1") as GameObject;
            }
        if (count == 1)
        {
            isCheck = false;
            click[1] = true;
        }
        else if (!click[1] && (count > 1))
        {
            count--;
            _gameObjlist.Remove(Resources.Load("Prefabs/Character/1") as GameObject);
            _gameObjlist.Clear();
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed1", 0);
            characters[1].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
            PlayerPrefs.Save();
        }

        UpdateSelect();
    }

    public void SelectCharacter2()
    {
        checkFace = true;
        click[2] = !click[2];
        bool isCheck = true;
        if (isCheck)
            if (click[2] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/2") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(6, 9)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed2", 1);
                characters[2].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/2") as GameObject;
            }
        if (count == 1)
        {
            isCheck = false;
            click[2] = true;
        }
        else if (!click[2] && (count > 1))
        {
            _gameObjlist.Remove(Resources.Load("Prefabs/Character/2") as GameObject);
            _gameObjlist.Clear();
            count--;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed2", 0);
            characters[2].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
            PlayerPrefs.Save();
        }

        UpdateSelect();
    }

    public void SelectCharacter3()
    {
        checkFace = true;
        click[3] = !click[3];
        bool isCheck = true;
        if (isCheck)
            if (click[3] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/3") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(9, 12)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed3", 1);
                characters[3].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                // UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/3") as GameObject;
            }
        if (count == 1)
        {
            isCheck = false;
            click[3] = true;
        }
        else if (!click[3] && (count > 1))
        {
            _gameObjlist.Add(Resources.Load("Prefabs/Character/3") as GameObject);
            _gameObjlist.Clear();
            count--;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed3", 0);
            characters[3].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
            PlayerPrefs.Save();
        }
        UpdateSelect();
        //  CheckFace();
    }

    public void SelectCharacter4()
    {
        checkFace = true;
        click[4] = !click[4];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[4] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/4") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(12, 15)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed4", 1);
                characters[3].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/4") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[4] = true;
            }
            else if (!click[4] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/4") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed4", 0);
                characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter5()
    {
        checkFace = true;
        click[5] = !click[5];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[5] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/5") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(15, 18)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed5", 1);
                characters[3].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/5") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[5] = true;
            }
            else if (!click[5] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/5") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed5", 0);
                characters[5].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter6()
    {
        checkFace = true;
        click[6] = !click[6];
        bool isCheck = true;
        if (isCheck)
            if (click[6] && (count >= 0))
            {
                //
                _audioSource.clip = characterClip[Random.Range(18, 21)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed6", 1);
                characters[6].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/6") as GameObject;
            }
        if (count == 1)
        {
            isCheck = false;
            click[6] = true;
        }
        else if (!click[6] && (count > 1))
        {
            count--;
            _gameObjlist.Remove(Resources.Load("Prefabs/Character/6") as GameObject);
            _gameObjlist.Clear();
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed6", 0);
            characters[6].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
            PlayerPrefs.Save();
        }
        UpdateSelect();
        //  CheckFace();
        //  resourStr = name;
    }

    public void SelectCharacter7()
    {
        checkFace = true;
        click[7] = !click[7];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[7] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/7") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(21, 24)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed7", 1);
                characters[7].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/7") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[7] = true;
            }
            else if (!click[7] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/7") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed7", 0);
                characters[7].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter8()
    {
        checkFace = true;
        click[8] = !click[8];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[8] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/8") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(24, 27)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed8", 1);
                characters[8].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/8") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[8] = true;
            }
            else if (!click[8] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/8") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed8", 0);
                characters[8].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter9()
    {
        checkFace = true;
        click[9] = !click[9];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[9] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/9") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(27, 30)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed9", 1);
                characters[9].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/9") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[9] = true;
            }
            else if (!click[9] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/9") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed9", 0);
                characters[9].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter10()
    {
        checkFace = true;
        click[10] = !click[10];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[10] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/10") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(30, 33)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed10", 1);
                characters[10].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/10") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[10] = true;
            }
            else if (!click[10] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/10") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed10", 0);
                characters[10].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter11()
    {
        checkFace = true;
        click[11] = !click[11];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[11] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/11") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(33, 36)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed11", 1);
                characters[11].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/11") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[11] = true;
            }
            else if (!click[11] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/11") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed11", 0);
                characters[11].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter12()
    {
        checkFace = true;
        click[12] = !click[12];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[12] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/12") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(36, 39)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed12", 1);
                characters[12].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/12") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[12] = true;
            }
            else if (!click[12] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/12") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed12", 0);
                characters[12].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter13()
    {
        checkFace = true;
        click[13] = !click[13];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[13] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/13") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(39, 42)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed13", 1);
                characters[13].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/13") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[13] = true;
            }
            else if (!click[13] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/13") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed13", 0);
                characters[13].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter14()
    {
        checkFace = true;
        click[14] = !click[14];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[14] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/14") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(42, 45)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed14", 1);
                characters[14].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/14") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[14] = true;
            }
            else if (!click[14] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/14") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed14", 0);
                characters[14].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter15()
    {
        checkFace = true;
        click[15] = !click[15];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[15] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/15") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(45, 48)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed15", 1);
                characters[15].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/15") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[15] = true;
            }
            else if (!click[15] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/15") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed15", 0);
                characters[15].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter16()
    {
        checkFace = true;
        click[16] = !click[16];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[16] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/16") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(48, 51)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed16", 1);
                characters[16].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/16") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[16] = true;
            }
            else if (!click[16] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/16") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed16", 0);
                characters[16].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter17()
    {
        checkFace = true;
        click[17] = !click[17];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[17] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/17") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(51, 54)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed17", 1);
                characters[17].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/17") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[17] = true;
            }
            else if (!click[17] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/17") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed17", 0);
                characters[17].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter18()
    {
        checkFace = true;
        click[18] = !click[18];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[18] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/18") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(54, 57)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed18", 1);
                characters[18].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/18") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[18] = true;
            }
            else if (!click[18] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/18") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed18", 0);
                characters[18].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    public void SelectCharacter19()
    {
        checkFace = true;
        click[19] = !click[19];
        bool isCheck = true;
        if (isCheck)
        {
            if (click[19] && (count >= 0))
            {
                _gameObjlist.Add(Resources.Load("Prefabs/Character/19") as GameObject);

                //
                _audioSource.clip = characterClip[Random.Range(57, 60)];
                _audioSource.Play();
                count++;
                // count = 1;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed19", 1);
                characters[19].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

                UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/19") as GameObject;
            }
            if (count == 1)
            {
                isCheck = false;
                click[19] = true;
            }
            else if (!click[19] && (count > 1))
            {
                _gameObjlist.Remove(Resources.Load("Prefabs/Character/19") as GameObject);
                _gameObjlist.Clear();

                count--;
                PlayerPrefs.SetInt("Check", count);
                PlayerPrefs.SetInt("Choosed19", 0);
                characters[19].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(false);
                PlayerPrefs.Save();
            }
            UpdateSelect();
            //   CheckFace();
        }
    }

    #endregion vùng này để check button click

    // check if unlock level is unlock character

    private IEnumerator deactiveFaceUnlock()
    {
        yield return new WaitForSeconds(2f);
        FaceUnLockLevel.gameObject.SetActive(false);
    }

    public void CheckUnLockedLevel()
    {
        if ((PlayerPrefs.GetInt("ValueMaxLevel") == 2) && !PlayerPrefs.HasKey("Unlocked" + 0))
            PlayerPrefs.SetInt("Unlocked" + 0, 1);
    }

    // check sound unlock and display face character on top
    public void CheckSound()
    {
        //  PlayerController._instance.isMoveCenter = true;
        if ((vavlueLevel == 2) && (PlayerPrefs.GetInt("Unlocked0") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock.gameObject.SetActive(true);
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.sprite = characterSprite[1];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();

            _gameObjlist.Add(Resources.Load("Prefabs/Character/1") as GameObject);

            //

            //

            count++;
            click[1] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed1", 1);
            characters[1].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/1") as GameObject;
            //UIManager._instance.RandomOrNoRandom();

            //
        }

        if ((vavlueLevel == 5) && (PlayerPrefs.GetInt("Unlocked1") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            // 2 tai
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock.gameObject.SetActive(false);
            faceUnlock2.gameObject.SetActive(true);
            faceUnlock2.sprite = characterSprite[2];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            _gameObjlist.Add(Resources.Load("Prefabs/Character/2") as GameObject);

            //

            count++;
            click[2] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed2", 1);
            characters[2].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/2") as GameObject;
            //
        }
        if ((vavlueLevel == 10) && (PlayerPrefs.GetInt("Unlocked2") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock.gameObject.SetActive(false);
            faceUnlock2.gameObject.SetActive(true);
            faceUnlock2.sprite = characterSprite[3];

            StartCoroutine(deactiveFaceUnlock());
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/3") as GameObject);

            //

            count++;
            click[3] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed3", 1);
            characters[3].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/3") as GameObject;
            //   _audioSource.Stop();
        }
        if ((vavlueLevel == 15) && (PlayerPrefs.GetInt("Unlocked3") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[4];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //Check unlock
            _gameObjlist.Add(Resources.Load("Prefabs/Character/4") as GameObject);

            //

            count++;
            click[4] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed4", 1);
            characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/4") as GameObject;
        }
        if ((vavlueLevel == 17) && (PlayerPrefs.GetInt("Unlocked4") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[5];

            StartCoroutine(deactiveFaceUnlock());

            _gameObjlist.Add(Resources.Load("Prefabs/Character/5") as GameObject);

            //

            count++;
            click[5] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed5", 1);
            characters[5].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/5") as GameObject;
            //   _audioSource.Stop();
        }
        if ((vavlueLevel == 20) && (PlayerPrefs.GetInt("Unlocked5") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[6];

            StartCoroutine(deactiveFaceUnlock());

            //check
            _gameObjlist.Add(Resources.Load("Prefabs/Character/6") as GameObject);

            //

            count++;
            click[6] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed6", 1);
            characters[6].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/6") as GameObject;
            //   _audioSource.Stop();
        }
        if ((vavlueLevel == 23) && (PlayerPrefs.GetInt("Unlocked6") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[7];

            StartCoroutine(deactiveFaceUnlock());
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/7") as GameObject);

            //

            count++;
            click[7] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed7", 1);
            characters[7].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/7") as GameObject;
            //   _audioSource.Stop();
        }
        if ((vavlueLevel == 25) && (PlayerPrefs.GetInt("Unlocked7") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[8];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/8") as GameObject);

            //

            count++;
            click[8] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed8", 1);
            characters[8].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/8") as GameObject;
        }
        if ((vavlueLevel == 27) && (PlayerPrefs.GetInt("Unlocked8") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[9];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/9") as GameObject);

            //

            count++;
            click[9] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed9", 1);
            characters[9].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/9") as GameObject;
        }
        if ((vavlueLevel == 30) && (PlayerPrefs.GetInt("Unlocked9") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[10];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/10") as GameObject);

            //

            count++;
            click[10] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed10", 1);
            characters[10].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/10") as GameObject;
        }
        if ((vavlueLevel == 35) && (PlayerPrefs.GetInt("Unlocked10") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[11];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/11") as GameObject);

            //

            count++;
            click[11] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed11", 1);
            characters[11].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/11") as GameObject;
        }
        if ((vavlueLevel == 40) && (PlayerPrefs.GetInt("Unlocked11") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[12];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/12") as GameObject);

            //

            count++;
            click[12] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed12", 1);
            characters[12].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/12") as GameObject;
        }
        if ((vavlueLevel == 45) && (PlayerPrefs.GetInt("Unlocked12") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[13];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/13") as GameObject);

            //

            count++;
            click[13] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed13", 1);
            characters[13].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/13") as GameObject;
        }
        if ((vavlueLevel == 50) && (PlayerPrefs.GetInt("Unlocked13") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[14];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/14") as GameObject);

            //

            count++;
            click[14] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed14", 1);
            characters[14].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/14") as GameObject;
        }
        if ((vavlueLevel == 55) && (PlayerPrefs.GetInt("Unlocked14") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[15];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/15") as GameObject);

            //

            count++;
            click[15] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed15", 1);
            characters[15].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/15") as GameObject;
        }
        if ((vavlueLevel == 60) && (PlayerPrefs.GetInt("Unlocked15") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[16];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/16") as GameObject);

            //

            count++;
            click[16] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed16", 1);
            characters[16].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/16") as GameObject;
        }
        if ((vavlueLevel == 65) && (PlayerPrefs.GetInt("Unlocked16") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[17];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/17") as GameObject);

            //

            count++;
            click[17] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed17", 1);
            characters[17].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/17") as GameObject;
        }
        if ((vavlueLevel == 70) && (PlayerPrefs.GetInt("Unlocked17") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[18];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/18") as GameObject);

            //

            count++;
            click[18] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed18", 1);
            characters[18].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/18") as GameObject;
        }
        if ((PlayerPrefs.GetInt("FBCharacter") == 1) && (PlayerPrefs.GetInt("Unlocked18") == 0))
        {
            FaceUnLockLevel.SetActive(true);
            _audioSource.clip = heroUnlock;
            _audioSource.Play();
            faceUnlock2.gameObject.SetActive(false);
            faceUnlock.gameObject.SetActive(true);
            faceUnlock.sprite = characterSprite[19];

            StartCoroutine(deactiveFaceUnlock());
            //   _audioSource.Stop();
            //
            _gameObjlist.Add(Resources.Load("Prefabs/Character/19") as GameObject);

            //

            count++;
            click[19] = true;
            // count = 1;
            PlayerPrefs.SetInt("Check", count);
            PlayerPrefs.SetInt("Choosed19", 1);
            characters[19].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);

            UIManager._instance.playerSpwan = Resources.Load("Prefabs/Character/19") as GameObject;
        }
    }

    // check unlock charater if get more level
    public void LockCharacter()
    {
        //if no unlock over else do that
        if (!unlock) return;
        vavlueLevel = PlayerPrefs.GetInt("ValueMaxLevel");
        foreach (GameObject t in characters)
            if (t != null)
            {
                t.transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(true);
                t.GetComponent<Button>().interactable = false;
                if (vavlueLevel >= 0)
                {
                    characters[0].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[0].GetComponent<Button>().interactable = true;

                    //
                    //   characters[0].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 2)
                {
                    characters[1].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[1].GetComponent<Button>().interactable = true;
                    //
                }

                if (vavlueLevel >= 5)
                {
                    characters[2].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[2].GetComponent<Button>().interactable = true;
                }

                if (vavlueLevel >= 10)
                {
                    characters[3].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[3].GetComponent<Button>().interactable = true;
                    //  click[3] = true;

                    //  characters[3].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }

                if (vavlueLevel >= 15)
                {
                    characters[4].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[4].GetComponent<Button>().interactable = true;
                    //  click[4] = true;

                    //    characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }

                if (vavlueLevel >= 17)
                {
                    characters[5].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[5].GetComponent<Button>().interactable = true;
                    //   click[5] = true;

                    //  characters[5].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }

                if (vavlueLevel >= 20)
                {
                    characters[6].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[6].GetComponent<Button>().interactable = true;
                    // click[6] = true;

                    //  characters[6].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }

                if (vavlueLevel >= 23)
                {
                    characters[7].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[7].GetComponent<Button>().interactable = true;
                    //  click[7] = true;

                    //  characters[8].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 25)
                {
                    characters[8].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[8].GetComponent<Button>().interactable = true;
                    //   click[8] = true;

                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 27)
                {
                    characters[9].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[9].GetComponent<Button>().interactable = true;
                    //   click[8] = true;

                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 30)
                {
                    characters[10].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[10].GetComponent<Button>().interactable = true;
                    //   click[8] = true;

                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 35)
                {
                    characters[11].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[11].GetComponent<Button>().interactable = true;
                    //   click[8] = true;

                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 40)
                {
                    characters[12].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[12].GetComponent<Button>().interactable = true;
                    //   click[8] = true;

                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 45)
                {
                    characters[13].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[13].GetComponent<Button>().interactable = true;
                    //   click[8] = true;

                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 50)
                {
                    characters[14].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[14].GetComponent<Button>().interactable = true;
                    //   click[8] = true;

                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 55)
                {
                    characters[15].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[15].GetComponent<Button>().interactable = true;
                    //   click[8] = true;

                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 60)
                {
                    characters[16].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[16].GetComponent<Button>().interactable = true;
                    //   click[8] = true;

                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 65)
                {
                    characters[17].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[17].GetComponent<Button>().interactable = true;
                    //   click[8] = true;

                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (vavlueLevel >= 70)
                {
                    characters[18].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[18].GetComponent<Button>().interactable = true;
                    //   click[8] = true;

                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                if (PlayerPrefs.GetInt("FBCharacter") == 1)
                {
                    characters[19].transform.Find("Lock").GetComponent<Image>().gameObject.SetActive(false);
                    characters[19].GetComponent<Button>().interactable = true;
                    //   click[8] = true;
                    //Unlock character when logged facebook
                    PlayerPrefs.SetInt("Unlocked18", 1);
                    // characters[4].transform.Find("Choose").GetComponent<Image>().gameObject.SetActive(true);
                }
                //  check sound
            }

        unlock = false;
    }
}