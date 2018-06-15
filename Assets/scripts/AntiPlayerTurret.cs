using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AntiPlayerTurret : NetworkBehaviour {
	
	
	public float AimRadius=8f;
    public GameObject projectile;
    public float fireInterval=0.3f;

    public float bulletLifetime = 2f;
    public float bulletVelocity = 4f;
    public Transform gun;
    float nextShot;
    GameObject AimedAt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isServer) {
			return;
		}
		RaycastHit2D[] creatures = Physics2D.CircleCastAll (transform.position, AimRadius, Vector2.zero, 0, LayerMask.GetMask ("Creatures"));
		List<GameObject> enemies = new List<GameObject>();
		foreach (RaycastHit2D c in creatures) {
			if (c.transform.CompareTag ("Player")) {
				if (c.transform.gameObject.GetComponent<Health> ().Threat > 0) {
					enemies.Add (c.transform.gameObject);
				}
			}
		}
        if (enemies.Count > 1) { enemies.Sort(new GOByThreat()); }
        try { AimedAt = enemies[0]; }
        catch (System.ArgumentOutOfRangeException) {
            AimedAt = null;
        }
        if (AimedAt != null)
        {
            if (Time.time >= nextShot)
            {
                nextShot = Time.time + fireInterval;
                var bullet = Instantiate(
                             projectile,
                             gun.position,
                             gun.rotation);
                Vector3 dir = AimedAt.transform.position - gun.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletVelocity;
                NetworkServer.Spawn(bullet);
                //bullet.GetComponent<Projectile> ().RpcActivateSprite ();
                Destroy(bullet, bulletLifetime);
            }
        }

	}
}
public class GOByThreat: IComparer<GameObject>
{
	public int Compare(GameObject first, GameObject second){
		if(first==null||second==null){
			throw new System.ArgumentNullException();
		}
		Health f=first.GetComponent<Health>();
		Health s=second.GetComponent<Health>();
		return new HByThreat().Compare(f,s);
	}

}
public class HByThreat: IComparer<Health>
{
	public int Compare(Health first,Health second){
		if (first == null || second == null) {
			throw new System.ArgumentNullException();
		}
		return first.Threat - second.Threat;
	}
}

