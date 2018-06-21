using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaserEmitterSimple : PeriodicSpewer {
    
    public float maxLength;
    public Laser projectile;

	// Use this for initialization
	void Start () {
        _projectile = projectile;
	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        RaycastHit2D rh = Physics2D.Raycast(gun.position,gun.right,maxLength,LayerMask.GetMask("Default"));
        if (go) {
            Laser l = go.GetComponent<Laser>();
            if (rh.collider != null)
            {
                float length = Vector2.Distance(rh.point, gun.position);
                //Debug.Log("Ray hit:" + rh.collider.ToString() + " at point: " + rh.point.ToString());
                //Debug.Log("Length of created laser: "+length.ToString());
                l.len = length;
            }
            else {
                l.len = maxLength;
            }
            
        }
	}


    protected override void Fire()
    {
        go = Instantiate(_projectile.gameObject,gun);
        go.GetComponent<Projectile>().damage = damage;
        NetworkServer.Spawn(go);
        Destroy(go, bulletLifetime);
    }
}
