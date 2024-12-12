using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class snowBall : MonoBehaviour
{
    public Vector3 maxSizeScale;
    public float maxSize = 6f;
    
    private Rigidbody _rbParent;
    //private float heightLand;
    private Vector3 _originSize;
    private Transform _parentTransform;
    private GameObject _suelo;
    private GameObject _suelo2;
    private int inc;

    private float inputMin = 2f;
    private float inputMax = 6f;
    private int targetMin = 1;
    private int targetMax = 30;

    public event System.Action<int> OnSnowBallDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        _parentTransform = transform.parent;
        _rbParent = _parentTransform.GetComponent<Rigidbody>();
        //_originSize = transform.lossyScale- transform.lossyScale/2;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2.0f,LayerMask.GetMask("suelo")))
        {
            _suelo = hit.transform.gameObject;
            //heightLand = hit.transform.position.y + hit.transform.localScale.y/2f;
            Debug.Log("Suelo de " + hit.transform.gameObject.name);
        }
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2.0f, LayerMask.GetMask("sueloEnemigo")))
        {
            _suelo2 = hit.transform.gameObject;
            //heightLand = hit.transform.position.y + hit.transform.localScale.y/2f;
            Debug.Log("Suelo enemigo de " + hit.transform.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //!Physics.Raycast(transform.position + Vector3.left * 0.1f, Vector3.down, transform.lossyScale.y / 2f + 1f, LayerMask.GetMask("suelo")) && !Physics.Raycast(transform.position + Vector3.right * 0.1f, Vector3.down, transform.lossyScale.y / 2f + 1f, LayerMask.GetMask("suelo"))
        if (Physics.Raycast(transform.position, Vector3.down, transform.lossyScale.y/2f +1f, LayerMask.GetMask("suelo")) || Physics.Raycast(transform.position, Vector3.down, transform.lossyScale.y / 2f + 1f, LayerMask.GetMask("sueloEnemigo")))
        {
            if (_rbParent.velocity.magnitude > 0.1f)
            {
                if (transform.localScale.y > maxSize)
                {
                    transform.localScale = new Vector3(maxSize, maxSize, this.transform.localScale.z);
                }
                else
                {
                    //transform.localScale *= 1.0003f;
                    transform.localScale = new Vector3(transform.localScale.x * 1.0005f, transform.localScale.y * 1.0005f, transform.localScale.z);
                    //transform.position = new Vector3(transform.position.x + _parentTransform.forward.x* 0.0001f, transform.position.y , transform.position.z);
                }
            }
            
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * (transform.lossyScale.y / 2f + 0.1f), new Color(0.75f, 0.05f, 0.03f), 5f);
            //Destroy(gameObject);
            NotifyAndDestroy();
            //if (_rbParent.velocity.magnitude > 0.1f && Vector3.Distance(maxSizeScale,transform.lossyScale)>0.1f)

            //heightLand + transform.lossyScale.y / 2f
        }
        /*if (transform.localScale.x > maxSize)
        {
            transform.localScale = new Vector3(maxSize, maxSize, this.transform.localScale.z);
        }*/

        //EL tamaño puede variar en 8
        //Primer rango 0,3 luego 0,5 0,5
        float preInc = Mathf.Clamp(this.transform.localScale.y, inputMin, inputMax);
        inc = (int)(targetMin + (preInc - inputMin) * (targetMax - targetMin) / (inputMax - inputMin));

        //inc = (int)(transform.lossyScale.y / _originSize.y) - 1;

        //Debug.Log("Inc : " + inc);

    }

    public void SetInitialSize(Vector3 size)
    {
        transform.localScale = size;
        _originSize = size;
    }

    public int GetStadistics()
    {
        //int inc =(int) (transform.lossyScale.x / _originSize.x)-1;
        //Debug.Log("Inc : " + inc);
        return inc;
    }

    private void NotifyAndDestroy()
    {
        OnSnowBallDestroyed?.Invoke(inc); // Llama al evento y pasa las estadísticas
        Destroy(gameObject); // Destruye el objeto
    }

    /*private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject!=_suelo || collider.gameObject != _suelo2)
        {
            Debug.Log("Se rompió" + collider.gameObject.name);
            NotifyAndDestroy();
        }

       //Destroy(gameObject);
    }*/

}
