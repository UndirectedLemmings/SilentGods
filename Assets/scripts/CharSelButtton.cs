using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class CharSelButtton : MonoBehaviour {

    public ButtonDeactivator[] buttons_to_disable;
    public ButtonDeactivator[] buttons_to_enable;

    void Start() {
       
    }

    public void Press(int cha) {
        GameObject go = GameObject.Find("local_cs");
        CharacterSelector cs = go.GetComponent<CharacterSelector>();
        if (cs != null) {
            cs.CmdChooseCharacter(cha);

                foreach (ButtonDeactivator b in buttons_to_disable) {
                    b.SetActive(false);
                }
                foreach (ButtonDeactivator b in buttons_to_enable)
                {
                    b.SetActive(true);
                }
            
        }
    }
    


}


