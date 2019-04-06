using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseCharacter : MonoBehaviour{
    
    public Sprite shrek;
    public Sprite shreik;
    public Sprite shrump;

    public string player1_name = "Shrek";
    public string player2_name = "Shrek";
    
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
        PlayerInfo.player1 = player1_name;
        PlayerInfo.player2 = player2_name;
        SceneManager.LoadScene("SampleScene");
    }

    public void selectShrek() {
        if (counter % 2 == 0) {
            player1_sprite.sprite = shrek;
            player1_name = "Shrek";
        }
        else {
            player2_sprite.sprite = shrek;
            player2_name = "Shrek";
        }
        counter++;
    }
    

    public void selectShreik() {
        if (counter % 2 == 0) {
            player1_sprite.sprite = shreik;
            player1_name = "Shreikh";
        }
        else {
            player2_sprite.sprite = shreik;
            player2_name = "Shreikh";
        }
        counter++;
    }

    public void selectShrump() {
        if (counter % 2 == 0) {
            player1_sprite.sprite = shrump;
            player1_name = "Shrump";
        }
        else {
            player2_sprite.sprite = shrump;
            player2_name = "Shrump";
        }
        counter++;
    }

    
}
