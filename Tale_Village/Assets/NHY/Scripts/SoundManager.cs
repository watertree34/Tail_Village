using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;  //싱글톤
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    AudioSource ear;
    public AudioClip mouseSound;
    public AudioClip buttonSound;
    public AudioClip teleportSound;
    public AudioClip lifeSound;
    public AudioClip itemSound;
    private void Start()
    {
        ear = GetComponent<AudioSource>();
    }

    public void MouseSound()
    {
        ear.PlayOneShot(mouseSound);
    }
    public void ButtonSound()
    {
        ear.PlayOneShot(buttonSound);
    }

    public void TeleportSound()
    {
        ear.PlayOneShot(teleportSound);
    }
    public void LifeSound()
    {
        ear.PlayOneShot(lifeSound);
    }
    public void ItemSound()
    {
        ear.PlayOneShot(itemSound);
    }
}
