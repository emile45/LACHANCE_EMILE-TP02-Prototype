﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] ragdollRbs;
    Animator animatorVanguard;

    bool isDead;

    public bool debugKill;
    
    // Déclaration de la source audio pour le bruit de mort PNJ
    public AudioSource dyingAudioSrc;

    // Start is called before the first frame update
    void Awake()
    {
        // Lister tous les Rbs
        ragdollRbs = GetComponentsInChildren<Rigidbody>();

        animatorVanguard = GetComponent<Animator>();

        // Désactiver le Ragdoll
        toggleRagdoll(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (debugKill)
        {
            Die();
            debugKill = false;
        }
    }

    public void Die()
    {
        if (isDead)
            return;

        isDead = true;
        toggleRagdoll(true);
    }

    void toggleRagdoll(bool value)
    {
        // Si la valeur est vrai, le PNJ est mort, donc on fait jouer le bruit de mort
        if(value)
            dyingAudioSrc.Play();
        // Mettre le Kinematic à !value
        foreach (Rigidbody rb in ragdollRbs)
        {
            rb.isKinematic = !value;
        }

        // Animator
        animatorVanguard.enabled = !value;
    }
}
