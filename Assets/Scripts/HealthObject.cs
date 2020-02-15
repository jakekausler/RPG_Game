using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthObject : MonoBehaviour
{
	public float maxHealth;
	public float health;
	public float tempHealth;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Heal(float amount) {
    	if (amount < 0) {
    		Damage(-amount);
    	}
    	health += amount;
    	if (health > maxHealth) {
    		health = maxHealth;
    	}
    }

    public float Damage(float amount) {
    	if (amount < 0) {
    		Heal(-amount);
    	}
    	if (tempHealth > 0) {
    		tempHealth -= amount;
    		if (tempHealth < 0) {
    			float newAmount = -tempHealth;
    			tempHealth = 0;
    			return Damage(newAmount);
    		}
    		return 0;
    	}
    	health -= amount;
    	float leftover = 0;
    	if (health < 0) {
    		leftover = -health;
    		health = 0;
    	}
    	if (IsDead()) {
    		OnDead();
    	}
    	return leftover;
    }

    public bool IsDead() {
    	return health == 0;
    }

    public virtual void OnDead() {

    }
}
