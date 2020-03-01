using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmitter : MonoBehaviour
{
    public GameObject ProjectileAsset;
    public float FireDelay = 2;
    public float Speed = 50;
    public float KillTime = 5;

    private float timer = 0;
    private bool isActive = false;
    void Update()
    {
        if (isActive) {
            timer += Time.deltaTime;
            if (timer > FireDelay) {
                timer = 0;
                
                GameObject spawn = Instantiate(ProjectileAsset, transform.position, Quaternion.identity);
                Rigidbody rb = spawn.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * Speed, ForceMode.Impulse);

                Destroy(spawn, KillTime);
            }
        }
    }

    public void SetActive(bool val)
    {
        isActive = val;
    }
}
