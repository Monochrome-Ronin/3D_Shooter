using System.Collections;
using UnityEngine;
using StarterAssets;
using UnityEngine.Animations.Rigging;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private float volume = 0.2f;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform hitMetal;
    [SerializeField] private Transform hitSoftBody;
    [SerializeField] private Transform muzzleFlash;
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private int ammoAmount;
    [SerializeField] private float fireRate;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private bool automatic = false;
    [SerializeField] private bool knife = false;
    [SerializeField] private bool onPlayer = true;
    [SerializeField] private float bulletRange = 100;
    [SerializeField] private float damage = 20;

    [SerializeField] Transform rightRef;
    [SerializeField] Transform leftRef;
    [SerializeField] TwoBoneIKConstraint rightConsrtaint;
    [SerializeField] TwoBoneIKConstraint leftConsrtaint;
    [SerializeField] RigBuilder rigBuilder;



    private int currentAmmoAmount;
    private float nextTimeToRire = 0f;
    private bool isReloading = false;
    private AudioSource audioSource;

    public string CurrentAmmoAmount { get { return currentAmmoAmount.ToString(); } }


    private void Start()
    {
        currentAmmoAmount = ammoAmount;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (isReloading)
        {
            return;
        }

        if(currentAmmoAmount <= 0 && !knife)
        {
            StartCoroutine(Reload());
            return;
        }

        if (!automatic)
        {
            if (starterAssetsInputs.shoot && currentAmmoAmount > 0 && Time.time >= nextTimeToRire)
            {
                nextTimeToRire = Time.time + 1f / fireRate;
                currentAmmoAmount -= 1;

                Shoot();

            }
            else
            {
                starterAssetsInputs.shoot = false;
            }
        }
        else
        {
            if (starterAssetsInputs.shoot && currentAmmoAmount > 0 && Time.time >= nextTimeToRire)
            {
                nextTimeToRire = Time.time + 1f / fireRate;
                currentAmmoAmount -= 1;

                Shoot();

            }
        }

        
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        audioSource.PlayOneShot(reloadSound, volume);
        yield return new WaitForSeconds(reloadTime);

        currentAmmoAmount = ammoAmount;
        isReloading = false;
        starterAssetsInputs.shoot = false;
    }

    private void Shoot()
    {
        Ray ray = new Ray(transform.position, new Vector3(0, 0.05f, 1));
        if (onPlayer)
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        }
        
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, bulletRange, aimColliderLayerMask))
        {
            hitTransform = raycastHit.transform;
        }

        if (!knife)
        {
            Transform _muzzleFlash = Instantiate(muzzleFlash, spawnBulletPosition.position, Quaternion.LookRotation(transform.position - raycastHit.point));
            _muzzleFlash.SetParent(spawnBulletPosition);
        }
        

        if (hitTransform != null)
        {
            audioSource.PlayOneShot(shootSound, volume);
            if (hitTransform.GetComponent<BulletTarget>() != null)
            {
                Instantiate(hitMetal, raycastHit.point, Quaternion.LookRotation(transform.position - raycastHit.point));
                raycastHit.collider.transform.GetComponent<HealthController>().hit(damage);
            }
            else
            {
                Instantiate(hitSoftBody, raycastHit.point, Quaternion.LookRotation(transform.position - raycastHit.point));
            }
        }
        if (!automatic)
        {
            starterAssetsInputs.shoot = false;
        }
    }

    private void SetRefs()
    {
        rigBuilder.enabled = false;
        rightConsrtaint.data.target = rightRef;
        leftConsrtaint.data.target = leftRef;
        rigBuilder.enabled = true;
    }

    void OnEnable()
    {
        SetRefs();
    }
}
