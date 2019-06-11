using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShooting : MonoBehaviour
{
    public float Range = 50.0f;
    public float FireRate = 0.25f;
    public GameObject GunEnd;
    public GameObject Player;
    public GameObject MuzzleFlash;
    


    private float _nextFire;
    private Camera _camera;
    private float _timer;
    private AudioSource _gunSound;
    private Animator _anime;
    

    private void Start()
    {
        _camera = GetComponentInParent<Camera>();
        _gunSound = transform.GetChild(1).GetComponent<AudioSource>();
        _anime = transform.GetChild(1).GetComponent<Animator>();
    }


    public void Shoot()
    {

        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + FireRate;
            
            Vector3 rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            
            if(_gunSound != null)
                _gunSound.Play();
                
            _anime.SetTrigger("Fire");
            StartCoroutine(muzzleFlashing());
            
            if (Physics.Raycast(rayOrigin, _camera.transform.forward, out hit, Range))
            {
               
                

                if (hit.collider.CompareTag("Enemy"))
                {
                    EnemySpawning.Instance.LookingAtEnemy = false;
                    Player.transform.GetChild(1).GetComponent<PlayerScript>().UpdateScore();
                    hit.collider.GetComponent<EnemyAI>().EnemyDeath();
                }
//                else if ( /*hit online player*/)
//                {
//                    
//                }
            }
        }        
    }

    private IEnumerator muzzleFlashing()
    {
        MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        MuzzleFlash.SetActive(false);
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
      //  Vector3 rayOrigin = Player.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        //Debug.DrawRay(rayOrigin, Player.GetComponent<Camera>().transform.forward * Range, Color.blue);
    }
}
