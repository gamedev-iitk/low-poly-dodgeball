using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{

    public float FieldOfView = 45;
    
    private GameManager _gameManager;
    private GameObject _player;
    private ProjectileEmitter _emitter;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = _gameManager.GetPlayer();
        _emitter = GetComponent<ProjectileEmitter>();
    }

    void Update()
    {
        Vector3 dir = (_player.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dir) < FieldOfView) {
            transform.LookAt(_player.transform);
            _emitter.SetActive(true);
        } else {
            _emitter.SetActive(false);
        }
    }
}
