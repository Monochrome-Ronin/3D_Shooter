using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimCam;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Rig aimLayerBody;
    [SerializeField] private Rig aimLayerWeapon;



    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        thirdPersonController.SetRotateOnMove(false);
    }

    void Update()
    {
        aimLayerBody.weight = 1f;
        aimLayerWeapon.weight = 1f;

        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        Vector3 aimTarget = mouseWorldPosition;
        aimTarget.y = transform.position.y;
        Vector3 aimDirection = (aimTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

        if (starterAssetsInputs.aim)
        {
            aimCam.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
        }
        else
        {
            aimCam.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
        }
        



    }
    private void OnAnimatorMove() { }
}
