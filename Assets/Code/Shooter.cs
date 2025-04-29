using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("Shooting Settings")]
    public float shotDuration = 0.2f;
    public float bulletSpeed = 20f;
    

    [Header("Muzzle Flash")]
    public GameObject muzzleFlash;
    public Transform muzzleFlashPoint;
    public float muzzleFlashDuration = 0.1f;
    public Vector3 recoilRotation = new Vector3(-10f, 0f, 0f);

    [Header("Object References")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject user;

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
        Debug.Log("Equipper equipped: " + equipper.isEquipped());
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

        if(recoilCoroutine != null){
            StopCoroutine(recoilCoroutine);
        }
        recoilCoroutine = StartCoroutine(Recoil());

        Transform spawnPoint = firePoint;
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if(rb != null){
            rb.AddForce(spawnPoint.forward * bulletSpeed, ForceMode.Impulse);
        }else{
            Debug.LogError("Bullet prefab does not have a Rigidbody component.");
        }
        Destroy(bullet, 8f);
        StartCoroutine(ShootCooldown());
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
        while (elapsedTime < shotDuration/2)
        {
            float progress = elapsedTime / shotDuration;
            transform.localRotation = Quaternion.Slerp(originalRotation, targetRotation, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        while (elapsedTime < shotDuration){
            float progress = elapsedTime / shotDuration;
            elapsedTime += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(targetRotation, originalRotation, progress);
            yield return null;
        }
        transform.localRotation = originalRotation;
    }
}
