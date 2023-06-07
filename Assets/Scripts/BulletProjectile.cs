using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform hitOther;
    [SerializeField] private Transform hitTarget;

    private Rigidbody bulletRigitbody;

    private void Awake()
    {
        bulletRigitbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        float speed = 50f;
        bulletRigitbody.velocity = transform.forward * speed;
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        /*if (other.GetComponent<BulletTarget>() != null)
        {
            Instantiate(hitTarget, other.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(hitOther, transform.position, Quaternion.identity);
        }
        print(transform.position);*/
        Destroy(gameObject);
    }
}
