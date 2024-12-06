using System;
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

    private BoxCollider _collider;
    private Vector3 colliderCenter;
    private Vector3 colliderExtents;

    private Animator animator;

    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
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
        //_isGrounded = Physics.Raycast(transform.position, Vector3.down, transform.lossyScale.y / 2f + 0.1f, LayerMask.GetMask("suelo"));
        //_hitWallR = Physics.Raycast(transform.position, Vector3.right, transform.lossyScale.x / 2f + 0.1f, LayerMask.GetMask("suelo"));
        //_hitWallL = Physics.Raycast(transform.position, Vector3.left, transform.lossyScale.x / 2f + 0.1f, LayerMask.GetMask("suelo"));
        colliderCenter = _collider.bounds.center;
        colliderExtents = _collider.bounds.extents;

        _isGrounded = Physics.Raycast(colliderCenter, Vector3.down, colliderExtents.y + 0.5f, LayerMask.GetMask("sueloEnemigo"));
        _hitWallR = Physics.Raycast(colliderCenter, Vector3.right, colliderExtents.x + 0.1f, LayerMask.GetMask("suelo"));
        _hitWallL = Physics.Raycast(colliderCenter, Vector3.left, colliderExtents.x + 0.1f, LayerMask.GetMask("suelo"));

        Debug.DrawRay(colliderCenter, Vector3.down * (colliderExtents.y + 0.1f), Color.green);
        Debug.DrawRay(colliderCenter, Vector3.right * (colliderExtents.x + 0.1f), Color.red);
        Debug.DrawRay(colliderCenter, Vector3.left * (colliderExtents.x + 0.1f), Color.blue);

        if (_isGrounded && !_hitWallR && !_hitWallL)
        {
            changingDirection = false;
            Debug.Log("No cambio");
        }

        if ((!_isGrounded || _hitWallL || _hitWallR)&&!changingDirection)
        {
            changingDirection = true;
            _direction *= -1;
            Debug.Log("cambio");
        }

        if (_direction > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (_direction < 0 && isFacingRight)
        {
            Flip();
        }
        _rb.velocity = new Vector2(_direction * speed, 0);

        animator.SetFloat("Speed", _direction * speed);
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;

        // Rotate the character instead of scaling
        transform.rotation = Quaternion.Euler(0, isFacingRight ? 0 : 180, 0);
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
