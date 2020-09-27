using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSound : MonoBehaviour
{
    public AudioClip giantWalk;
    public AudioClip giantSleep;
    public AudioClip giantScream; 
    AudioSource ear;
    // Start is called before the first frame update
    void Start()
    {
        ear = GetComponent<AudioSource>();
    }
    public void GiantWalkSound()
    {
        ear.PlayOneShot(giantWalk);
    }
    public void GiantSleepSound()
    {
        ear.PlayOneShot(giantSleep);
    }
    public void GiantScreamSound()
    {
        ear.PlayOneShot(giantScream);
    }

}
