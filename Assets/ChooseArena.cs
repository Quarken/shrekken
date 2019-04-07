using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseArena : MonoBehaviour{
    
    public GameObject arena1;
    public GameObject arena2;
    public GameObject arena3;
    public GameObject arena4;

    
    // Start is called before the first frame update
    void Start()
    {
        arena1.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void selectArena1() {
        arena1.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        arena2.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena3.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena4.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        PlayerInfo.background = "Swamp";
    }
    
    public void selectArena2() {
        arena1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena2.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        arena3.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena4.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        PlayerInfo.background = "Field";
    }

    public void selectArena3() {
        arena1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena2.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena3.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        arena4.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        PlayerInfo.background = "Boxingring";
    }

    public void selectArena4() {
        arena1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena2.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena3.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena4.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        PlayerInfo.background = "WhiteHouse";
    }
}
