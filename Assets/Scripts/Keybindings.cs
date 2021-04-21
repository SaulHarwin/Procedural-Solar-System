using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybindings : MonoBehaviour {
    [Space]
    [SerializeField]
    [TextArea]
    private string Desciption;
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            this.GetComponent<Information>().EnableAllTrails();
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            this.GetComponent<Information>().DisableAllTrails();
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            this.GetComponent<Information>().DeleteAllTrails();
        }

        if (Input.GetKeyDown("up")) {
            this.GetComponent<Information>().IncreaseTime();
        }

        if (Input.GetKeyDown("down")) {
            this.GetComponent<Information>().DecreaseTime();
        }
    }
}
