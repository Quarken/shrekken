using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioSource startSound;
    public AudioSource backgroundSound;
    public AudioSource itsOgreSound;
    public AudioSource hitSound;
    public bool silent = false;    
    // Start is called before the first frame update
    void Start() {
        backgroundSound.Play();
    }

    public void PlaySwampSound() {
        print("swamp");
        startSound.Play(0);
    }

    /* public IEnumerator PlayStartSound() {
        print("play start sound");
        backgroundSound.Pause();
        startSound.Play(0);
        yield return new WaitForSeconds(startSound.clip.length);
        backgroundSound.Play();
    }*/

    public void PlayItsOgre() {
        itsOgreSound.Play();
    }

    public void PlayHitSound() {
        print("didi");
        hitSound.Play();
    }

}
