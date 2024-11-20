using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{
    private float _direction;
    private Rigidbody _rb;
    public float speed;
    public float health;
    private bool _isGrounded;
    private bool _hitWallR;
    private bool _hitWallL;
    private bool _enemyDetected;
    public GameObject _fireBallPrefab;
    private GameObject _target = null;
    private bool changingDirection;
    // Start is called before the first frame update
    void Start()
    {
        _direction = 1f;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_enemyDetected)
        {
            Move();
        }
       
    }
    void Move()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, transform.lossyScale.y / 2f + 0.1f, LayerMask.GetMask("suelo"));
        _hitWallR = Physics.Raycast(transform.position, Vector3.right, transform.lossyScale.y / 2f + 0.1f, LayerMask.GetMask("suelo"));
        _hitWallL = Physics.Raycast(transform.position, Vector3.left, transform.lossyScale.y / 2f + 0.1f, LayerMask.GetMask("suelo"));

        if(_isGrounded && !_hitWallR && !_hitWallL)
        {
            changingDirection = false;
        }

        if ((!_isGrounded || _hitWallL || _hitWallR)&&!changingDirection)
        {
            changingDirection = true;
            _direction *= -1;
            Debug.Log("cambio");
        }
        _rb.velocity = new Vector2(_direction * speed, 0);
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health < 0)
            Destroy(gameObject);
    }
    public void SetEnemyDetected(bool a,GameObject o) 
    {
        if (a)
        {
            _rb.velocity = Vector2.zero;
            _rb.isKinematic = true;
            _target = o;
        }
        else 
        {
            _rb.isKinematic = false;
            _target = null;
        }

        _enemyDetected = a;
    }
}
