using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;
    Vector3 distanciaCameraPlayer;

    void Start()
    {
        distanciaCameraPlayer = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + distanciaCameraPlayer;
    }
}
