using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform ShootingAim;
    public float BulletForce;

    private GameObject Bullet;


    public void ShootBullet()
    {
        Bullet = Instantiate(BulletPrefab, ShootingAim.transform.position, ShootingAim.transform.rotation);
        Rigidbody BulletRigidBody = Bullet.GetComponent<Rigidbody>();
        BulletRigidBody.AddForce(transform.forward * BulletForce);
    }



}
