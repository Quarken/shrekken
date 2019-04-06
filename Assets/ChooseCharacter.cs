using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseCharacter : MonoBehaviour{
    
    public Sprite shrek;
    public Sprite shreik;
    public Sprite shrump;
    
    private SpriteRenderer player1_sprite;
    private SpriteRenderer player2_sprite;
    
    private int counter = 0;
    private Sprite[] shreks;


    public void Start() {
        player1_sprite = GameObject.Find("player1_sprite").GetComponent<SpriteRenderer>();
        player2_sprite = GameObject.Find("player2_sprite").GetComponent<SpriteRenderer>();       
    }

    public void backToMain() {
        SceneManager.LoadScene("StartMenu");
    }

    public void playButton() {
        SceneManager.LoadScene("SampleScene");
    }

    public void selectShrek() {
        if (counter % 2 == 0) {
            player1_sprite.sprite = shrek;
        }
        else {
            player2_sprite.sprite = shrek;
        }
        counter++;
    }
    

    public void selectShreik() {
        if (counter % 2 == 0) {
            player1_sprite.sprite = shreik;
        }
        else {
            player2_sprite.sprite = shreik;
        }
        counter++;
    }

    public void selectShrump() {
        if (counter % 2 == 0) {
            player1_sprite.sprite = shrump;
        }
        else {
            player2_sprite.sprite = shrump;
        }
        counter++;
    }

    
}
