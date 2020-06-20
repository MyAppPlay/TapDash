using UnityEngine;

public class SoundBase : MonoBehaviour
{
    private SoundManager _soundManager;

    public SoundManager soundManager
    {
        get { return _soundManager ?? (_soundManager = FindObjectOfType<SoundManager>()); }
    }
}