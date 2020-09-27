using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSound : MonoBehaviour
{
    public AudioClip duck;
    AudioSource ear;
    private void Start()
    {
        ear = Camera.main.GetComponent<AudioSource>();
    }
    public void Duck()
    {
        ear.PlayOneShot(duck);
    }
}
