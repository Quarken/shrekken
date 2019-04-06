using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum WinState {
        PLAYER_ONE = 0,
        PLAYER_TWO,
        DRAW
    }
    public int match_time = 60;
    public Text timer_text;
    public ShrekController player_one;
    public ShrekController player_two;

    // Start is called before the first frame update
    void Start()
    {
        StartMatch();
    }

    // Update is called once per frame
    void Update()
    {
        if(player_one.isDead) EndMatch(WinState.PLAYER_TWO);
        else if(player_two.isDead) EndMatch(WinState.PLAYER_TWO);
    }

    IEnumerator Timer () {
        while(match_time > 0) {
            yield return new WaitForSeconds (1);
            match_time--;
            timer_text.text = match_time.ToString();
        }
        EndMatch(WinState.DRAW);
    }

    private void StartMatch() {
        StartCoroutine("Timer");
    }
    private void EndMatch(WinState winner) {
        Time.timeScale = 0;
    }
}
