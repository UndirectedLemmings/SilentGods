using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public int maxHealth = 100;
	public int defence=0;
	[SyncVar]public int Threat;
	[SyncVar(hook ="OnChangeHealth")]public int currentHealth = 100;
    RectTransform hb;
    float initialWidth;

    void Start() {
        if (!isLocalPlayer) {
            return;
        }
        GameObject go = GameObject.FindGameObjectWithTag("HealthBar");
        hb = go.GetComponent<RectTransform>();
        initialWidth = hb.sizeDelta.x;
    }



    void OnChangeHealth(int health) {
        if (hb != null) {
            float x = initialWidth * ((float)health / maxHealth);
            hb.sizeDelta = new Vector2(x, hb.sizeDelta.y);
        }
    }




	public void TakeDamage(int amount)
	{
		if (!isServer) {
			return;
		}

		int dmg = amount - defence;
		if (dmg < 1) {
			dmg = 1;
		}
		currentHealth -= dmg;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Destroy (gameObject);
		}
	}
	public void Heal(int amount){
		if (!isServer) {
			return;
		}
		currentHealth += amount;
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
	}
}
