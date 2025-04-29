using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("Shooting Settings")]
    public float shotDuration = 0.2f;
    public float maxDistance = 100f;

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float bulletDuration = 5f;
    

    [Header("Muzzle Flash")]
    public GameObject muzzleFlash;
    public Transform muzzleFlashPoint;
    public float muzzleFlashDuration = 0.1f;

    [Header("Recoil Settings")]
    public float recoilDuration = 0.1f;
    public Vector3 recoilRotation = new Vector3(-10f, 0f, 0f);

    [Header("Object References")]
    public Transform firePoint;
    public GameObject user;
    public Camera userCamera;

    Equipper equipper;

    Boolean canShoot;
    Coroutine recoilCoroutine;

    void Start()
    {
        canShoot = true;
        equipper = user.GetComponent<Equipper>();
        if (equipper == null)
        {
            Debug.LogError("Equipper component not found on the shooter object.");
        }

    }

    // Update is called once per frame
    void Update()
    {   
        if(canShoot && equipper.isEquipped() && Input.GetMouseButtonDown(0)){
            shoot();
        }
    }

    void shoot(){
        if(bulletPrefab == null){
            Debug.LogError("Bullet prefab not assigned.");
            return;
        }

        if(firePoint == null){
            Debug.LogError("Fire point not assigned.");
            return;
        }
        if(muzzleFlash == null){
            Debug.LogError("Muzzle flash not assigned.");
            return;
        }
        if(muzzleFlashPoint == null){
            Debug.LogError("Muzzle flash point not assigned.");
            return;
        }
        if(userCamera == null){
            Debug.LogError("Player camera not assigned.");
            return;
        }

        //apply recoil to gun
        if(recoilCoroutine != null){
            StopCoroutine(recoilCoroutine);
        }
        recoilCoroutine = StartCoroutine(Recoil());

        Ray ray = userCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        //bullet hit something
        if(Physics.Raycast(ray, out hit, maxDistance)){
            targetPoint = hit.point;
        }
        //buillet did not hit anything
        else{
            targetPoint = ray.GetPoint(maxDistance);
        }

        //direction of firePoint to targetPoint
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        //spawn and shoot the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if(rb != null){
            rb.linearVelocity = direction * bulletSpeed;
        }else{
            Debug.LogError("Bullet prefab does not have a Rigidbody component.");
        }
        Destroy(bullet, bulletDuration);

        StartCoroutine(ShootCooldown());

        //create muzzle flash
        GameObject flash = Instantiate(muzzleFlash, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
        flash.transform.parent = muzzleFlashPoint;
        Destroy(flash, muzzleFlashDuration);
    }
    IEnumerator ShootCooldown(){
        canShoot = false;
        yield return new WaitForSeconds(shotDuration);
        canShoot = true;
    }

    IEnumerator Recoil(){
        Quaternion originalRotation = transform.localRotation;
        Quaternion targetRotation = originalRotation * Quaternion.Euler(recoilRotation);
        float elapsedTime = 0f;
        while (elapsedTime < recoilDuration/2)
        {
            float progress = elapsedTime / recoilDuration;
            transform.localRotation = Quaternion.Slerp(originalRotation, targetRotation, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        while (elapsedTime < recoilDuration){
            float progress = elapsedTime / recoilDuration;
            elapsedTime += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(targetRotation, originalRotation, progress);
            yield return null;
        }
        transform.localRotation = originalRotation;
    }
}
