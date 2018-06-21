using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Projectile : NetworkBehaviour {

	[SerializeField]public Hitters canFlyTrough;
	[SerializeField]public Hitters damages;
	public Sprite sprite;
	public Dmg damage;
    public Collider2D co;
    Vector2 vel;
	void Start(){
		
	}

    void Update() {
        vel = GetComponent<Rigidbody2D>().velocity;
    }

	void OnCollisionEnter2D(Collision2D coll){
		bool destroy=false;

		if (coll.collider.CompareTag ("Player")) {
			if (damages.players) {
				Health h = coll.gameObject.GetComponent<Health> ();
				if (h != null) {
					h.TakeDamage(Random.Range(damage.min,damage.max+1));
				}
			}
			if (canFlyTrough.players) {
				Physics2D.IgnoreCollision (co, coll.collider, true);
				destroy = false;
			} else {
				destroy = true;
			}
		} else if (coll.collider.CompareTag ("Mob")) {
			if (damages.mobs) {
				Health h = coll.gameObject.GetComponent<Health> ();
				if (h != null) {
					h.TakeDamage(Random.Range(damage.min,damage.max+1));
				}
			}
			if (canFlyTrough.mobs) {
				Physics2D.IgnoreCollision (co, coll.collider, true);
				destroy = false;
			} else {
				destroy = true;
			}
		} else if (coll.collider.CompareTag("Obstacle")){
            if (canFlyTrough.obstacles)
            {
                Physics2D.IgnoreCollision(co, coll.collider, true);
                destroy = false;
            }
            else {
                destroy = true;
            }
        }
        if (destroy)
        {
            Destroy(gameObject);
        }
        else {
            if (vel.magnitude > 0.1) {
                GetComponent<Rigidbody2D>().velocity = vel;
            }
           
        }

	}


	[ClientRpcAttribute]
	public void RpcActivateSprite(){
		GetComponent<SpriteRenderer> ().sprite = sprite;

		//Debug.Log ("Sprite activated!");
	}

}

[System.Serializable]
public struct Dmg{
	public int min;
	public int max;
}
[System.Serializable]
public struct Hitters{
	public bool players;
	public bool mobs;
    public bool obstacles;
}