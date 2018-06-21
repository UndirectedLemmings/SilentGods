using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WalkingWithArrows : MonoBehaviour {
    CrossPlatformInputManager.VirtualAxis horizontal;
    CrossPlatformInputManager.VirtualAxis vertical;
	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
        enabled = true;
#else
#if UNITY_STANDALONE
        enabled=true;
#else
        enabled=false;
#endif
#endif

    }
	
	// Update is called once per frame
	void Update () {
        if (horizontal != null && vertical != null)
        {

            horizontal.Update(Input.GetAxis("Horizontal"));
            vertical.Update(Input.GetAxis("Vertical"));
        }
        else {
            horizontal = CrossPlatformInputManager.VirtualAxisReference("Horizontal");
            vertical = CrossPlatformInputManager.VirtualAxisReference("Vertical");
        }
        
	}
}
