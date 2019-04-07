using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UltStateMachine {
    private int state;
    private DateTime lastState;
    private float delta = 0.75f;
    private string kick = "kick";
    private string punchDown = "punchdown";
    private string punchUp = "punchup";
    public UltStateMachine() {
        state = 0;
        lastState = DateTime.Now;
    }

    private bool next(string action) {
        var diff = (DateTime.Now - lastState).TotalSeconds;
        if (diff > delta) state = 0;
        bool increment = false;
        
        switch(state) {
        case 0:
            increment = action.Equals(kick);
            break;
        case 1:
            increment = (action.Equals(kick));
            break;
        case 2:
            increment = action.Equals(punchDown);
            break;
        }
        Debug.Log("statemachine " + state + " " + action + " " + increment + " " + diff);
        if (increment) state++;
        else state = 0;
        lastState = DateTime.Now;
        return state == 3;
    }

    public bool Kick() {
        return next(kick);
    }

    public bool PunchDown() {
        return next(punchDown);
    }
}
