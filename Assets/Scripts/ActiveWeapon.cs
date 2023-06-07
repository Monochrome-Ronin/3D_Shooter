using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private GameObject firstWeapon;
    [SerializeField] private GameObject secondWeapon;
    [SerializeField] private GameObject thirdWeapon;

    [SerializeField] Text amooText;

    public GameObject activeWeapon;

    void Awake()
    {
        activeWeapon = firstWeapon;
    }

    
    void Update()
    {
        if (activeWeapon != null)
        {
            amooText.text = activeWeapon.transform.GetComponent<WeaponController>().CurrentAmmoAmount;
        }

        if (starterAssetsInputs.firstWeapon)
        {
            firstWeapon.SetActive(true);
            secondWeapon.SetActive(false);
            thirdWeapon.SetActive(false);
            activeWeapon = firstWeapon;
        }
        if (starterAssetsInputs.secondWeapon)
        {
            firstWeapon.SetActive(false);
            secondWeapon.SetActive(true);
            thirdWeapon.SetActive(false);
            activeWeapon = secondWeapon;
        }
        if (starterAssetsInputs.thirdWeapon)
        {
            firstWeapon.SetActive(false);
            secondWeapon.SetActive(false);
            thirdWeapon.SetActive(true);
            activeWeapon = thirdWeapon;
        }
    }

    
}
