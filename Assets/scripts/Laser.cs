using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile {

    public float len;
    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        canFlyTrough.mobs = true;
        canFlyTrough.obstacles = true;
        canFlyTrough.players = true;
        sr = GetComponent<SpriteRenderer>();
        sr.size = new Vector2(len, 0.5f);
    }


    /*void Update() {
        if (sr.size.x != len) {
           
            
        }

    }*/

}
