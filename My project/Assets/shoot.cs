using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public float timeAlive;
    public string ally;
    public string enemy;
    public float damage;
    private float timer;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeAlive)
        {
            Destroy(gameObject);
        }
        if(enemy == "Player") 
        {
            transform.Translate(direction * Time.deltaTime*5f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ally)) return;
        if (collision.gameObject.CompareTag(enemy))
        {
            if(collision.gameObject.TryGetComponent(out IDamagable d))
            {
                d.Damage(damage);
            }
        }
        Destroy(gameObject);
    }
}
