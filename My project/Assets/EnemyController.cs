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
    private bool _enemyDetected;
    public GameObject _fireBallPrefab;
    private GameObject _target = null;
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
            Move(); Debug.LogError("wow" + " : " + _enemyDetected);
        }
        else 
        {
            //var fireBall = Instantiate(_fireBallPrefab, transform.position, Quaternion.identity);
            //fireBall.GetComponent<shoot>().direction = _target.transform.position - transform.position;
        }
    }
    void Move()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, transform.lossyScale.y / 2f + 0.1f, LayerMask.GetMask("suelo"));
        if (!_isGrounded)
            _direction *= -1;
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
