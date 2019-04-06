using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int matchTime = 60;
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        StartMatch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Timer () {
        while(matchTime > 0) {
            yield return new WaitForSeconds (1);
            matchTime--;
            timerText.text = matchTime.ToString();
        }
        EndMatch();
    }

    private void StartMatch() {
        StartCoroutine("Timer");
    }
    private void EndMatch() {

    }
}
