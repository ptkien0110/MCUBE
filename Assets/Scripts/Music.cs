using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    private bool isMuted;
    // Start is called before the first frame update
    private void Start()
    {
        isMuted = false;
    }
    public void MutedPresses()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted; 

    }


}
