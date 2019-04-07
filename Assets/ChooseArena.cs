using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseArena : MonoBehaviour{
    
    public GameObject arena1;
    public GameObject arena2;
    public GameObject arena3;
    public GameObject arena4;

    private Button arena1Button;
    private Button arena2Button;
    private Button arena3Button;
    private Button arena4Button;

    private Color normalColor;
    private Color lightColor;

    
    // Start is called before the first frame update
    void Start(){
        
        arena1Button = GameObject.Find("Arena1_button").GetComponent<UnityEngine.UI.Button>();
        arena2Button = GameObject.Find("Arena2_button").GetComponent<UnityEngine.UI.Button>();
        arena3Button = GameObject.Find("Arena3_button").GetComponent<UnityEngine.UI.Button>();
        arena4Button = GameObject.Find("Arena4_button").GetComponent<UnityEngine.UI.Button>();

        normalColor = new Color(109.0f, 60.0f, 11.0f);
        lightColor = new Color(214.0f, 131.0f, 47.0f);

        selectArena1();
    }

    public void selectArena1() {
        normalSizeForAll();
        arena1.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        

        PlayerInfo.background = "Swamp";
    }
    
    public void selectArena2() {
        normalSizeForAll();
        arena2.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        

        PlayerInfo.background = "Field";
    }

    public void selectArena3() {
        normalSizeForAll();
        arena3.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);


        PlayerInfo.background = "Boxingring";
    }

    public void selectArena4() {
        normalSizeForAll();
        arena4.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);

        PlayerInfo.background = "WhiteHouse";
    }

    public void normalSizeForAll() {
        arena1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena2.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena3.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        arena4.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); 
    }

    public void normalColorForAll() {
        arena1Button.GetComponent<Image>().color = normalColor;
        arena2Button.GetComponent<Image>().color = normalColor;
        arena3Button.GetComponent<Image>().color = normalColor;
        arena4Button.GetComponent<Image>().color = normalColor;
    }


    

}
