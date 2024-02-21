using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class BreakableObstacle : MonoBehaviour
{
    private Player player;
    private Animator anim;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") &&
            ((player.stateMachine.currentState == player.wireJumpState || player.stateMachine.currentState == player.onWireState)))
        {
            anim.SetBool("IsBroken", true);
            Debug.Log("Obstacle Break!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.Log(GameManager.Instance.curScore);

            GameManager.Instance.curScore += 100;
            Debug.Log(GameManager.Instance.curScore);
        }
    }

    public void EndAnim()
    {
        gameObject.SetActive(false);
    }
}
