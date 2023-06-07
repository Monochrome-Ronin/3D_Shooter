using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] float HP = 100;
    [SerializeField] Slider heathSlider;

    private Animator animator;
    private Rigdoll rigdoll;
    private bool dead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigdoll = GetComponent<Rigdoll>();
    }

    void Update()
    {
        heathSlider.value = HP;
        if (HP < 1 && !dead)
        {
            rigdoll.ActivateRigDoll();
            dead = true;
        }
    }

    public void hit(float damage)
    {
        HP -= damage;
        animator.SetLayerWeight(1, 0.5f);
        animator.SetTrigger("hit");
        Invoke("SetLayerWeight", 1.2f);
    }

    private void SetLayerWeight()
    {
        animator.SetLayerWeight(1, 0f);
    }
}
