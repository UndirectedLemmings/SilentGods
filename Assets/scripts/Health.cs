using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public int maxHealth = 100;
	public int defence=0;
	public int Threat;
	[SyncVar]public int currentHealth = 100;

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
