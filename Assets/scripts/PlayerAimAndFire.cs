using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAimAndFire : NetworkBehaviour {
	public Camera eyes;
	public AudioListener ears;
	public GameObject AimedAt;
	public float AimRadius=5f;
	public float FireInterval;
	public GameObject projectile;
	public Transform gun;
	public float bulletLifetime=2.0f;
	public float bulletVelocity=6.0f;

	int enemyIndex=0;
	float nextShot;
	Image aimImg;
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			eyes.enabled = false;
			ears.enabled = false;
		}
		aimImg = GameObject.FindGameObjectWithTag("Aim").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D[] creatures = Physics2D.CircleCastAll (transform.position, AimRadius, Vector2.zero, 0, LayerMask.GetMask ("Creatures"));
		List<GameObject> enemies = new List<GameObject>();
		foreach (RaycastHit2D c in creatures) {
			if (c.transform.CompareTag ("Mob")) {
				enemies.Add (c.transform.gameObject);
			}
		}
		if (CrossPlatformInputManager.GetButtonDown ("Fire2")) {
			enemyIndex++;
		}
		if (enemyIndex >= enemies.Count) {
			enemyIndex = 0;
		}
		try{
			AimedAt=enemies[enemyIndex];
		}
		catch(System.ArgumentOutOfRangeException){
			AimedAt = null;
		}

		if (AimedAt != null) {
			aimImg.enabled = true;
			//RectTransform rt = aimImg.GetComponent<RectTransform> ();

			//rt.anchoredPosition = rt.InverseTransformPoint (AimedAt.transform.position);
			//rt.position = AimedAt.transform.position;
			aimImg.rectTransform.position = eyes.WorldToScreenPoint( AimedAt.transform.position);
			if (AimedAt.transform.position.x < transform.position.x) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				GetComponent<SpriteRenderer> ().flipX = false;
			}
			if (isLocalPlayer) {
				if (CrossPlatformInputManager.GetButton ("Fire1")) {
					if (Time.time >= nextShot) {
						nextShot += FireInterval;
						CmdFire ();
					}
				}
			}
		} else {
			aimImg.enabled = false;
		}


	}
	[Command]
	void CmdFire(){
		var bullet = (GameObject)Instantiate (
			             projectile,
			             gun.position,
			             gun.rotation);
		bullet.transform.LookAt (AimedAt.transform.position);
		bullet.GetComponent<Rigidbody2D> ().velocity=bullet.transform.forward*bulletVelocity;
		NetworkServer.Spawn (bullet);
		Destroy (bullet, bulletLifetime);
	}



}
