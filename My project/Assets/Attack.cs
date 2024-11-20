using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public string enemyLayer;
    private GameObject target = null;
    private bool _attacking;
    private float timeLastHit;
    private float attackSpeed = 1.0f;
    public GameObject fireBallPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(2, 2, 2), Quaternion.identity, LayerMask.GetMask(enemyLayer));
        if (!target && colliders.Length > 0)
        {
            target = colliders[0].gameObject;
            if(TryGetComponent(out EnemyController e))
            {
                _attacking = true;
                e.SetEnemyDetected(true,target);
            }
        }
        else if(target && colliders.Length == 0)
        {
            if (TryGetComponent(out EnemyController e))
            {
                e.SetEnemyDetected(false,null);
            }
            _attacking = false;
            target = null;
        }
      

        if (_attacking && target) 
        {
            if(Time.time >= timeLastHit + attackSpeed)
        	{
                var fireBall = Instantiate(fireBallPrefab,transform.position,Quaternion.identity);
                Vector3 h = target.transform.position - transform.position;
                h.Normalize();
                fireBall.GetComponent<shoot>().direction = h;
                timeLastHit = Time.time;
            }
        }
    }
}
