using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(animator.enabled)
        {
            Invoke("Shoot", 2);
        }
    }

    private void Shoot()
    {
        starterAssetsInputs.shoot = true;
    }
}
