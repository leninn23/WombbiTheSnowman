using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowBall : MonoBehaviour
{
    public Vector3 maxSizeScale;
    
    private Rigidbody _rbParent;
    private float heightLand;
    private Vector3 _originSize;
    private Transform _parentTransform;
    private GameObject _suelo;
    
    // Start is called before the first frame update
    void Start()
    {
        _parentTransform = transform.parent;
        _rbParent = _parentTransform.GetComponent<Rigidbody>();
        _originSize = transform.lossyScale;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f,LayerMask.GetMask("suelo")))
        {
            _suelo = hit.transform.gameObject;
            heightLand = hit.transform.position.y + hit.transform.localScale.y/2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, transform.lossyScale.y/2f +1f, LayerMask.GetMask("suelo")))
        {
            Debug.DrawRay(transform.position, Vector3.down * (transform.lossyScale.y / 2f + 0.1f), new Color(0.75f, 0.05f, 0.03f), 5f);
            Destroy(gameObject);
        }
        else
        {
            if (_rbParent.velocity.magnitude > 0.1f && Vector3.Distance(maxSizeScale,transform.lossyScale)>0.1f)
            {
                transform.localScale *= 1.0001f;
                transform.position = new Vector3(transform.position.x + _parentTransform.forward.x* 0.0001f, heightLand + transform.lossyScale.y/2f, transform.position.z);
            }
        }
        int inc = (int)(transform.lossyScale.x / _originSize.x) - 1;
        Debug.Log(inc);

    }

    public int GetStadistics()
    {
        int inc =(int) (transform.lossyScale.x / _originSize.x)-1;
        Debug.Log(inc);
        return inc;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject!=_suelo) 
            Destroy(gameObject);
    }
    
}
