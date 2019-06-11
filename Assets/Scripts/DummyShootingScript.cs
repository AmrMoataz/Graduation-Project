using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform Aim;
    public float bulletForce = 1000;


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject Bullet = Instantiate(bulletPrefab, Aim.transform.position, Aim.transform.rotation);
            Rigidbody bulletrb = Bullet.GetComponent<Rigidbody>();
            bulletrb.AddForce(transform.forward * bulletForce * Time.deltaTime);
        }
    }
}
