using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Windows.WebCam;

public class EnemyTwo : MonoBehaviour,IDamagable
{
    public string layerEnemy;
    public float timeResting =5f;
    private float currentTimeResting = 0f;
    public bool enemyClose;
    public float rangeVision;
    public float maxTimeMoving;
    public float acceleration = 10f; 
    public float maxTimeAcceleration = 2f; 

    private float _currentTimeMoving;
    private Rigidbody _rb;
    private float elapsedTime = 0f;
    private float xDirection;

   
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        currentTimeResting = timeResting;
    }

    // Update is called once per frame
    void Update()
    {
        if(!enemyClose)
            CheckEnemyClose();
    }

    private void FixedUpdate()
    {
        //if(enemyClose && currentTimeResting>=timeResting)
        //    Move1(xDirection);
    }

    private void CheckEnemyClose()
    {
        var enemigo = Physics.OverlapSphere(transform.position, rangeVision, LayerMask.GetMask("Player"));
        if (enemigo.Length > 0 && _currentTimeMoving<maxTimeMoving)
        {
            Debug.Log("Enemigo detectado");
            _currentTimeMoving += Time.deltaTime;
            enemyClose = true;
            var direction = enemigo[0].transform.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            xDirection = direction.x;
            StartCoroutine(Move(xDirection));
        }
        else
        {
            _currentTimeMoving = 0;
            enemyClose = false;
        }
    }

    public void Damage(float damage)
    {
        if (!enemyClose) return;
    }

    private IEnumerator Move(float direction)
    {
        yield return new WaitForSeconds(2f);//tiempo que tarda en volver a arremeter
        while (elapsedTime < maxTimeAcceleration)
        {
            elapsedTime += Time.fixedDeltaTime;
            _rb.AddForce(new Vector3(direction,0,0) * acceleration, ForceMode.Acceleration);
            yield return new WaitForFixedUpdate();
        }
        elapsedTime = 0;
        while (elapsedTime < maxTimeAcceleration)
        {
            float t = elapsedTime / maxTimeAcceleration;
            t = Mathf.Clamp01(t);
            elapsedTime += Time.fixedDeltaTime;
            _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, t);
            if(elapsedTime>=maxTimeAcceleration)
            {
                elapsedTime = 0;
                enemyClose = false;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    
}
