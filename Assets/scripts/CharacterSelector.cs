using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterSelector : NetworkBehaviour {

    public Camera cam;
    public AudioListener lis;
    public GameObject[] chars;
    void Start() {
        if (isLocalPlayer)
        {
            gameObject.name = "local_cs";
        }
        else {
            cam.enabled = false;
            lis.enabled = false;
        }
    }

    [Command]
    public void CmdChooseCharacter(int cha) {
        GameObject go = Instantiate(chars[cha], transform.position, transform.rotation);
        NetworkServer.Spawn(go);
        bool res = NetworkServer.ReplacePlayerForConnection(connectionToClient, go, playerControllerId);
        if (res) {
            Destroy(gameObject);
        }
  
    }
}
