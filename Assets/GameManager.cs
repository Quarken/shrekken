using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isOngoing = false;
    private string[] endTextsList = {
        "It's All Ogre Now",
        "Get Shrekt",
        "Check Yourself Before You Shrek Yourself",
        "This Is My Swamp",
        "Game Ogre",
        "Shrexy Time"
    };
    enum WinState {
        PLAYER_ONE = 0,
        PLAYER_TWO,
        DRAW
    }
    public int match_time = 60;
    public Text timerText;
    public Text endText;
    public GameObject backButton;
    public GameObject replayButton;

    public ShrekController playerOne;
    public ShrekController playerTwo;

    private Vector2 playerOneSpawn;
    private Vector2 playerTwoSpawn;

    // Start is called before the first frame update
    void Start()
    {
        playerOneSpawn = playerOne.transform.position;
        playerTwoSpawn = playerTwo.transform.position;
        StartMatch();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOngoing) return;
        if(playerOne.isDead) EndMatch(WinState.PLAYER_TWO);
        else if(playerTwo.isDead) EndMatch(WinState.PLAYER_TWO);
    }

    IEnumerator Timer () {
        while(match_time > 0) {
            yield return new WaitForSeconds (1);
            match_time--;
            timerText.text = match_time.ToString();
        }
        EndMatch(WinState.DRAW);
    }

    private void StartMatch() {
        backButton.SetActive(false);   
        replayButton.SetActive(false);
        endText.text = "";
        match_time = 60;
        timerText.text = match_time.ToString();
        isOngoing = true;
        StartCoroutine("Timer");
    }
    private void EndMatch(WinState winner) {
        StopCoroutine("Timer");
        endText.text = endTextsList[Random.Range(0,endTextsList.Length)];
        isOngoing = false;
        backButton.SetActive(true);
        replayButton.SetActive(true);
    }

    public void GoBack() {
        SceneManager.LoadScene("CreateJoin");
    }

    public void Replay() {
        playerOne.transform.position = playerOneSpawn;
        playerTwo.transform.position = playerTwoSpawn;
        playerOne.Revive();
        playerTwo.Revive();
        StartMatch();
    }
}