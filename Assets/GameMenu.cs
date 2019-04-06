using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour{

    public InputField ipInput;
    public TextMeshProUGUI joinGameText;

    public void Start() {

    }

    public void CreateGame() {

    }

    public void JoinGame() {
        
    }
    
    public void BackToMain() {
        SceneManager.LoadScene("StartMenu");
    }
}
