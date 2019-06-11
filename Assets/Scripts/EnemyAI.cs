using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector]
    public Transform Player;
    public float movementSpeed;
    public AudioClip Hit;

    private Rigidbody rigidbody;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Debug.Log(Player.name);

        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.LookAt(Player);
        rigidbody.transform.position = Vector3.MoveTowards(transform.position, Player.position, movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            EnemySpawning.Instance.LookingAtEnemy = false;
            Player.GetChild(2).GetComponent<PlayerScript>().UpdateScore();
            Destroy(gameObject);
        }
    }

    public void EnemyDeath()
    {
        GetComponent<AudioSource>().clip = Hit;
        GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }
    
}
