using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;
    public event EventHandler OnDamaged;
    
    [SerializeField] private int healthMax = 100;
    [SerializeField] private int health;

    [Header("Audio")] 
    [SerializeField] private AudioClip hitAudio;
    [SerializeField] private AudioClip deathAudio;
    [SerializeField] private float volume = 1;


    private void Awake()
    {
        health = healthMax;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
            
        if (health <= 0)
        {
            health = 0;
        }
        
        OnDamaged?.Invoke(this, EventArgs.Empty);

        Vector3 loc = transform.position;
        GamePlayStatics.PlayAudioAtLoc(hitAudio, loc, volume);

        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
        
        Vector3 loc = transform.position;
        GamePlayStatics.PlayAudioAtLoc(deathAudio, loc, volume);
    }

    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }

    public void IncreaseHealth(int healthAmount)
    {
        health += healthAmount;

        if (health > healthMax)
        {
            health = healthMax;
        }
        
        OnDamaged?.Invoke(this, EventArgs.Empty);
    }
}
