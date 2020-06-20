using UnityEngine;

public class SoundManager : SoundBase
{
    [HideInInspector]
    public AudioSource _audioSource;

    public AudioClip touchClick;
    public AudioClip jumpBirdRed1;
    public AudioClip jumpBirdRed2;
    public AudioClip jumpBirdRed3;
    public AudioClip _diamonSound;
    public AudioClip _buttonClick;
    public AudioClip _birdRedDead;
    public AudioClip GameOverShowClip;
    public AudioClip _levelUp;
    public AudioClip _zoomMAp;
    public AudioClip helpDialog;
    public AudioClip[] Snail;
    // Use this for initialization

    public GameObject soundOff, soundOn;
    private float sound;

    private bool isEnableMusic;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        sound = PlayerPrefs.GetFloat("Volum", 1);
        //check button sound
        if (sound == 1)
        {
            isEnableMusic = true;
            AudioListener.pause = false;
            soundOff.SetActive(true);
            soundOn.SetActive(false);
        }
        if (sound == 0)
        {
            isEnableMusic = false;
            AudioListener.pause = true;
            soundOff.SetActive(false);
            soundOn.SetActive(true);
        }
    }

    public void OnOfFBX()
    {
        isEnableMusic = !isEnableMusic;
        if (isEnableMusic)
        {
            AudioListener.pause = false;
            PlayerPrefs.SetFloat("Volum", 1);
            soundOff.SetActive(true);
            soundOn.SetActive(false);
        }
        else
        {
            AudioListener.pause = true;
            PlayerPrefs.SetFloat("Volum", 0);
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
    }

    public void OcSenSound()
    {
        _audioSource.clip = Snail[Random.Range(0, Snail.Length - 1)];
        _audioSource.Play();
    }

    public void OcSenGot()
    {
        playPlayerSound(Snail[4], 1f);
    }

    public void ZoomMapOverPanel()
    {
        playPlayerSound(_zoomMAp, 1f);
    }

    // Update is called once per frame
    public void TouchClick(float volum)
    {
        playPlayerSound(touchClick, volum);
    }

    public void Jump1()
    {
        playPlayerSound(jumpBirdRed1, 0.7f);
    }

    public void Jump2()
    {
        playPlayerSound(jumpBirdRed2, 0.7f);
    }

    public void Jump3()
    {
        playPlayerSound(jumpBirdRed3, 0.7f);
    }

    public void BirdDead()
    {
        playPlayerSound(_birdRedDead, 0.8f);
    }

    public void HelpDialogSound()
    {
        playPlayerSound(helpDialog, 1f);
    }

    public void ButtonClick()
    {
        playPlayerSound(_buttonClick, 1f);
    }

    public void DiamondSound()
    {
        playPlayerSound(_diamonSound, 1f);
    }

    public void GameOverShow()
    {
        playPlayerSound(GameOverShowClip, 0.8f);
    }

    public void LevelUp(float sound)
    {
        playPlayerSound(_levelUp, sound);
    }

    public void playPlayerSound(AudioClip audioClip, float volume)
    {
        if (_audioSource != null && _audioSource != null)
        {
            _audioSource.Stop();
        }
        // play the effect once
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(audioClip);
    }
}