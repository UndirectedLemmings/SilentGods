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
        bool forceChange = false;
		RaycastHit2D[] creatures = Physics2D.CircleCastAll (transform.position, AimRadius, Vector2.zero, 0, LayerMask.GetMask ("Creatures"));
		List<GameObject> enemies = new List<GameObject>();
		foreach (RaycastHit2D c in creatures) {
			if (c.transform.CompareTag ("Mob")) {
				enemies.Add (c.transform.gameObject);
			}
		}
		if (isLocalPlayer) {
			if (CrossPlatformInputManager.GetButtonDown ("Fire2")) {
				enemyIndex++;
                forceChange = true;
			}
		}
		if (enemyIndex >= enemies.Count) {
			enemyIndex = 0;
		}
        if (!enemies.Contains(AimedAt)||forceChange) {
            try
            {
                AimedAt = enemies[enemyIndex];
            }
            catch (System.ArgumentOutOfRangeException)
            {
                AimedAt = null;
            }

        }
		

		if (AimedAt != null) {
			if(isLocalPlayer){
			aimImg.enabled = true;
			//RectTransform rt = aimImg.GetComponent<RectTransform> ();

			//rt.anchoredPosition = rt.InverseTransformPoint (AimedAt.transform.position);
			//rt.position = AimedAt.transform.position;
				aimImg.rectTransform.position = eyes.WorldToScreenPoint( AimedAt.transform.position);
			}
			if (AimedAt.transform.position.x < transform.position.x) {
				GetComponent<SpriteRenderer> ().flipX = true;

			} else {
				GetComponent<SpriteRenderer> ().flipX = false;

			}
			if (isLocalPlayer) {
				if (CrossPlatformInputManager.GetButton ("Fire1")) {
					if (Time.time >= nextShot) {
						nextShot =Time.time+ FireInterval;
						CmdFire (GetComponent<SpriteRenderer>().flipX);
					}
				}
			}
		} else {
			aimImg.enabled = false;
		}


	}
	[Command]
	void CmdFire(bool flipX){
		Transform truegun = gun;
		Vector3 pos = gun.localPosition;
		pos.x = flipX ? -Mathf.Abs(pos.x) :Mathf.Abs( pos.x);
		truegun.localPosition = pos;
		var bullet = (GameObject)Instantiate (
			             projectile,
			             truegun.position,
			             truegun.rotation);
		Vector3 dir = AimedAt.transform.position - truegun.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		bullet.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		bullet.GetComponent<Rigidbody2D> ().velocity = dir.normalized * bulletVelocity;
		NetworkServer.Spawn (bullet);
		//bullet.GetComponent<Projectile> ().RpcActivateSprite ();
		Destroy (bullet, bulletLifetime);
	}



}
