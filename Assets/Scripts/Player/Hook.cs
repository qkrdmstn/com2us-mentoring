using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    Player player;
    float velocity;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if(player.isAttach)
        {
            Vector3 curPos = transform.position;
            Vector3 nxtPos = Vector3.left * velocity * Time.deltaTime;

            transform.position = curPos + nxtPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ceiling"))
        {
            player.isAttach = true;
            Debug.Log("attach");
            velocity = collision.gameObject.GetComponentInParent<MoveMap>().speed;
        }
    }
}
