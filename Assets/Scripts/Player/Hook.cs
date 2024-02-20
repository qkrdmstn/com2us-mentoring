using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    GrapplingHook grappling;
    Player player;

    private void Start()
    {
        grappling = GameObject.Find("Player").GetComponent<GrapplingHook>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ceiling"))
        {
            grappling.isAttach = true;
            Debug.Log("attach");
        }
    }
}
