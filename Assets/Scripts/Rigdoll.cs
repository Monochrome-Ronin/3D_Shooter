using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class Rigdoll : MonoBehaviour
{
    [SerializeField] ActiveWeapon activeWeapon;
    private Rigidbody[] rigidbodies;
    private Animator animator;
    private RigBuilder rigBuilder;


    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        rigBuilder = GetComponent<RigBuilder>();
        DeactivateRigDoll();
    }


    public void DeactivateRigDoll()
    {
        foreach(var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        animator.enabled = true;
        rigBuilder.enabled = true;
        if(transform.TryGetComponent(out PlayerInput playerInput))
        {
            playerInput.enabled = true;
        }
        if(activeWeapon.activeWeapon.TryGetComponent(out Rigidbody rigidbody1))
        {
            rigidbody1.isKinematic = true;
        }
    }

    public void ActivateRigDoll()
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        animator.enabled = false;
        rigBuilder.enabled = false;
        if (transform.TryGetComponent(out PlayerInput playerInput))
        {
            playerInput.enabled = false;
        }
        if (activeWeapon.activeWeapon.TryGetComponent(out Rigidbody rigidbody1))
        {
            rigidbody1.isKinematic = false;
        }
        else
        {
            activeWeapon.activeWeapon.AddComponent<Rigidbody>();
        }
        
        if(transform.tag == "Player")
        {
            PlayerPrefs.SetInt("deaths", PlayerPrefs.GetInt("deaths") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("kills", PlayerPrefs.GetInt("kills") + 1);
        }
    }
}
