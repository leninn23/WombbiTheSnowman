using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour, IDamagable
{

    public float speed;
    public float jumpForce;
    public GameObject snowBallPrefab;
    public float initialHealth;
    public float maxHealth;
    public float maxSize;
    public GameObject snowBulletPrefab;
    public float shootForce;
    
    private Vector2 _input;
    private Rigidbody _rb;
    private bool isGrounded;
    private Vector3 _playerFordward;
    private GameObject _snowBall;
    private Camera _mainCamera;
    public float[] sizes1 = { 0.7f, 1f, 1.5f, 2f, 2.5f };

    public float[] posCuelloy = { -0.3f, 0.1f, 0.92f, 1.71f, 2.47f };
    public float[] posCabezaz = { 0.0021f, 0.004295351f, 0.0127f, 0.0206f, 0.0282f };

    public float[] sizesCuellox = { 298.46f, 100f, 100f, 100f, 100f };
    public float[] sizesCuelloy = { 83.26533f, 100f, 100f, 100f, 100f };

    public int[] speeds = { 10, 9 ,7, 6, 4 };
    public int[] jumpForces = { 15, 10, 8, 5, 3 };

    //public float coyoteTime = 0.2f;
    //public float jumpBufferTime = 1f;

    private float _coyoteTimeCounter;
    private float _jumpBufferCounter;

    private int index;

    public Animator animator;
    private bool isFacingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        AssignValues(initialHealth);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (!_snowBall && Input.GetKeyDown(KeyCode.Space) && isGrounded )
        {
            Jump();
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.E))
        {
            SpawnDespawnSnowBall();
        }
        if (_snowBall && Input.GetKeyDown(KeyCode.Q))
        {
            ConsumeSnowBall();
        }
        if (!_snowBall && Input.GetMouseButtonDown(0))
        {
           Shoot();
        }
        
       
    }

    void AssignValues(float value)
    {
        // Calcula el tamaño de cada rango
        float rangeSize = (float)maxHealth / sizes1.Length;

        // Determina en qué rango cae el valor dado
        index = Mathf.FloorToInt(value / rangeSize);
        index = Mathf.Clamp(index, 0, sizes1.Length - 1); // Asegura que el índice esté en el rango de valores disponibles
        //Ajustes bola
        transform.GetChild(4).localScale = new Vector3(1, sizes1[index], sizes1[index]);
        //Ajustes bufanda
        transform.GetChild(1).localPosition = new Vector3(-0.01983261f, posCuelloy[index], 0);
        transform.GetChild(1).localScale = new Vector3(sizesCuellox[index], sizesCuelloy[index], 100);
        //Ajustes cabeza
        transform.GetChild(0).transform.GetChild(2).localPosition = new Vector3(0.0001843905f, -0.0001024498f, posCabezaz[index]);

        speed = speeds[index];
        jumpForce = jumpForces[index];
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        isGrounded = isGroundPlayer();
        if (!isGrounded)
        {
            x *= 0.7f;
            animator.SetFloat("Speed", 0);
        }
        else if (isGrounded)
        {
            animator.SetFloat("Speed", Math.Abs(x));
        }

        if (x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (x < 0 && isFacingRight)
        {
            Flip();
        }
        _rb.velocity = new Vector2(-x * speed, _rb.velocity.y);
        //_rb.AddForce(-Vector3.up*2f, ForceMode.Acceleration);

        if (x != 0)
        {
            //transform.LookAt(transform.position + Vector3.right * (x > 0 ? 1 : -1))
            Vector3 direction = new Vector3(-x, 0, 0).normalized;
            transform.forward = direction;
        }

        if (Math.Abs(x)>0.01 && _snowBall)
        {
            animator.SetBool("_snowBall", true);
        }
        else
        {
            animator.SetBool("_snowBall", false);
        }
    }

    public void Flip()
    {
        /*isFacingRight = !isFacingRight;

        // Invertir la escala en el eje X
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        BoxCollider box = GetComponent<BoxCollider>();
        if (box != null)
        {
            Vector3 size = box.size;
            size.x = Mathf.Abs(size.x); // Force positive size
            box.size = size;
        }*/
        isFacingRight = !isFacingRight;

        // Rotate the character instead of scaling
        transform.rotation = Quaternion.Euler(0, isFacingRight ? 0 : 180, 0);
    }

    void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector3.up * jumpForces[index], ForceMode.Impulse);
    }

    void ConsumeSnowBall()
    {
        var inc = _snowBall.GetComponent<snowBall>().GetStadistics();
        Debug.Log("Inc : " + inc);
        var incHealth = initialHealth + inc*3;
        initialHealth = Math.Min(maxHealth, incHealth);

        AssignValues(initialHealth);
        //var incSize = Math.Min(inc / 3,maxSize);
        
        Destroy(_snowBall);
    }

    void SpawnDespawnSnowBall()
    {
        if (!_snowBall)
        {
            //float direction = Mathf.Sign(transform.localScale.x);
            //transform1.localScale.x
            var transform1 = transform;
            float direction = isFacingRight ? 1f : -1f;
            Debug.Log("Derecha : " + isFacingRight);

            var position = new Vector3(
                transform1.position.x + transform.forward.x * direction * transform1.lossyScale.x * 1.4f,
                transform1.position.y - transform1.lossyScale.y*1.5f,
                transform1.position.z);
            _snowBall = Instantiate(snowBallPrefab, position, Quaternion.identity);
            // transform.position = new Vector3(transform.position.x + _parentTransform.forward.x* 0.00005f, heightLand + transform.lossyScale.y/2f, transform.position.z);
            Vector3 initialSize = new Vector3(transform.GetChild(4).localScale.z-0.2f, transform.GetChild(4).localScale.y-0.2f, transform.GetChild(4).localScale.x-0.2f); // Tamaño deseado
            _snowBall.GetComponent<snowBall>().SetInitialSize(initialSize);
            _snowBall.GetComponent<snowBall>().OnSnowBallDestroyed += HandleSnowBallDestroyed;
            _snowBall.transform.SetParent(transform);

            //Ajustes bola
            transform.GetChild(4).localScale = new Vector3(1, sizes1[0], sizes1[0]);
            //Ajustes bufanda
            transform.GetChild(1).localPosition = new Vector3(-0.01983261f, posCuelloy[0], 0);
            transform.GetChild(1).localScale = new Vector3(sizesCuellox[0], sizesCuelloy[0], 100);
            //Ajustes cabeza
            transform.GetChild(0).transform.GetChild(2).localPosition = new Vector3(0.0001843905f, -0.0001024498f, posCabezaz[0]);
        }
        /*else 
        {
            Destroy(_snowBall);
            AssignValues(initialHealth);
        }*/

    }
    void Shoot()
    {
        /*if (transform.lossyScale.x < 0.1f || initialHealth == 1) return;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var direcction = hit.point - transform.position;
            direcction = new Vector3(direcction.x, direcction.y, 0);
            direcction.Normalize();
            var bullet = Instantiate(snowBulletPrefab, transform.position + Vector3.up * transform.lossyScale.y / 2f, Quaternion.identity);
            var rbBullet = bullet.GetComponent<Rigidbody>();
            rbBullet.AddForce(direcction * shootForce, ForceMode.Impulse);
        }
        initialHealth -= 1f;
        //if(transform.lossyScale.x > 0.1f)
        //var a = transform.localScale.x - transform.localScale.x / initialHealth;
        //if(a>0.1f)
        //    transform.localScale -= transform.localScale/initialHealth;
        AssignValues(initialHealth);
        //var incSize = Math.Min(inc / 3,maxSize);*/

        if (initialHealth <= 1f) return;

        // Crear el proyectil enfrente del jugador
        var spawnPosition = transform.position + transform.forward * 0.5f + Vector3.up * transform.lossyScale.y / 2f;
        var bullet = Instantiate(snowBulletPrefab, spawnPosition, Quaternion.identity);

        // Hacer que el proyectil se mueva hacia adelante
        var rbBullet = bullet.GetComponent<Rigidbody>();
        if (rbBullet != null)
        {
            rbBullet.AddForce(transform.forward * shootForce, ForceMode.Impulse);
        }

        // Reducir la vida
        initialHealth -= 1f;

        // Actualizar el tamaño o estado del objeto (si aplica)
        AssignValues(initialHealth);
    }

    public void Damage(float damage)
    {
        initialHealth -= damage;
        if (initialHealth < 0)
            Destroy(gameObject);
    }

    private bool isGroundPlayer()
    {
        Debug.Log(transform.lossyScale);
        Debug.DrawRay(transform.position, -transform.up * 2.1f, Color.red, 1f);
        bool grounded = Physics.BoxCast(transform.position + Vector3.up * 0.1f, transform.lossyScale / 2f, Vector3.down, Quaternion.identity, transform.lossyScale.y + 0.2f, LayerMask.GetMask("suelo"));
        Debug.Log($"Grounded: {grounded}"); // Debug if the player is grounded
        return grounded;
        //return Physics.Raycast(transform.position + Vector3.right * transform.lossyScale.x / 2f, Vector3.down, transform.lossyScale.y / 2f + 0.1f, LayerMask.GetMask("suelo"))
        //    || Physics.Raycast(transform.position - Vector3.right * transform.lossyScale.x / 2f, Vector3.down, transform.lossyScale.y / 2f + 0.1f, LayerMask.GetMask("suelo"));
    }

    private void HandleSnowBallDestroyed(int stats)
    {
        Debug.Log($"La bola de nieve fue destruida con estadísticas: {stats}");
        var incHealth = initialHealth + stats * 3;
        initialHealth = Math.Min(maxHealth, incHealth);

        AssignValues(initialHealth);
        // Aquí puedes agregar lógica para devolver las estadísticas al jugador.
    }

}
