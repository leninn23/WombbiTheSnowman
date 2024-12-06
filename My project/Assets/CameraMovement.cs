using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float height;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y+0.3f, transform.position.z);
    }
}
