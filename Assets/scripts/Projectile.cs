using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField]public Hitters canFlyTrough;
	[SerializeField]public Hitters damages;
	public Sprite sprite;
	public int damage;

	void Start(){
		GetComponent<SpriteRenderer> ().sprite = sprite;
	}


	void OnCollisionEnter2D(Collision2D coll){
		if (damages.players && coll.collider.CompareTag ("Player")) {
			//TODO: damage
		}
		if (damages.mobs && coll.collider.CompareTag ("Mob")) {
			//TODO: damage
		}
		if (canFlyTrough.players && coll.collider.CompareTag ("Player")) {
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), coll.collider, true);
		} else if (canFlyTrough.mobs && coll.collider.CompareTag ("Mob")) {
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), coll.collider, true);
		} else {
			Destroy (gameObject);
		}



	}
}


[System.Serializable]
public struct Hitters{
	public bool players;
	public bool mobs;
}