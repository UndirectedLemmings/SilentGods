using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerAbility : NetworkBehaviour {

    Health health;
    SpriteRenderer sr;
    PlayerAimAndFire paf;
    RectTransform EnergyBar;
    float fullBar;

    public int maxEnergy;
    public int AbilityCost;
    public int IncomePerSecond;
    public AType AbilityType;

    public int AmountHealed;
    public float HealRadius;

    public float AttackSpeedIncrease;
    public float RageTime;
    float initialFireInterval;

    public float HideTime;
    int initialThreat;

    [SyncVar(hook = "OnChangeEnergy")]
    public int currentEnergy;

    float nextrecover;
    float HideEnd;
    [SyncVar]bool HideActive;
    float RageEnd;
    [SyncVar]bool RageActive;
	// Use this for initialization
	void Start () {
        currentEnergy = maxEnergy;
        health = GetComponent<Health>();
        sr = GetComponent<SpriteRenderer>();
        paf = GetComponent<PlayerAimAndFire>();
        initialThreat = health.Threat;
        initialFireInterval = paf.FireInterval;
        if (!isLocalPlayer) {
            return;
        }
        GameObject go = GameObject.FindGameObjectWithTag("EnergyBar");
        EnergyBar = go.GetComponent<RectTransform>();
        fullBar = EnergyBar.sizeDelta.x;
	}
	
	// Update is called once per frame
	void Update () {
        sr.color = DetermineColor();
        if (!isLocalPlayer) {
            return;
        }
        if (Time.time > HideEnd) {
            health.Threat = initialThreat;
            HideActive = false;
        }
        if (Time.time > RageEnd) {
            paf.FireInterval = initialFireInterval;
            RageActive = false;
        }
        if (Time.time >= nextrecover)
        {
            nextrecover += 1f;
            currentEnergy += IncomePerSecond;
            if (currentEnergy > maxEnergy)
            {
                currentEnergy = maxEnergy;
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire3")) {
            if (currentEnergy >= AbilityCost) {
                currentEnergy -= AbilityCost;
                switch (AbilityType) {
                    case AType.Heal:
                        CmdHeal();
                        break;
                    case AType.Hide:
                        CmdHide();
                        break;
                    case AType.Rage:
                        CmdRage();
                        break;

                }

            }

        }


    }

    void OnChangeEnergy(int en) {

        if (EnergyBar != null) {
            float x = fullBar*((float)en / maxEnergy);
            EnergyBar.sizeDelta = new Vector2(x, EnergyBar.sizeDelta.y);
        }
}



    [Command]
    void CmdHeal() {
        RaycastHit2D[] creatures = Physics2D.CircleCastAll(transform.position, HealRadius, Vector2.zero, 0, LayerMask.GetMask("Creatures"));
        List<GameObject> enemies = new List<GameObject>();
        foreach (RaycastHit2D c in creatures)
        {
            if (c.transform.CompareTag("Player"))
            {
                if (c.transform.gameObject.GetComponent<Health>().Threat > 0)
                {
                    enemies.Add(c.transform.gameObject);
                }
            }
        }
        foreach (GameObject g in enemies) {
            Health h = g.GetComponent<Health>();
            if (h != null) {
                h.Heal(AmountHealed);
            }

        }

    }

    [Command]
    void CmdHide() {
        HideEnd = Time.time + HideTime;
        HideActive = true;
        initialThreat = health.Threat;
        health.Threat = 0;
    }

    [Command]
    void CmdRage() {
        RageEnd = Time.time + RageTime;
        RageActive = true;
        initialFireInterval = paf.FireInterval;
        paf.FireInterval /= AttackSpeedIncrease;
    }

    Color DetermineColor() {
        


        Color res = new Color(1,1,1);
        if (RageActive) {
            res.b *= 0.5f;
            res.g *= 0.5f;
        }
        if (HideActive) {
            res.b *= 0.75f;
            res.r *= 0.75f;
            res.g *= 0.75f;
        }
        return res;
    }


}


public enum AType
{
    Heal,
    Hide,
    Rage
}


