using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PeriodicSpewer : NetworkBehaviour {

    [SerializeField]protected Projectile _projectile;
    public float fireInterval;
    public float BulletVelocity;
    public float bulletLifetime;
    public Transform gun;
    public Dmg damage;

    float NextShot;
    protected GameObject go;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
        if (!isServer) {
            return;
        }

        if (Time.time >= NextShot) {
            NextShot = Time.time + fireInterval;
            Fire();
        }


	}

    protected virtual void Fire() {
        go = Instantiate(_projectile.gameObject, gun.position, gun.rotation);
        go.GetComponent<Rigidbody2D>().velocity = gun.right * BulletVelocity;
        go.GetComponent<Projectile>().damage = damage;
        NetworkServer.Spawn(go);
        Destroy(go, bulletLifetime);
    }

}
