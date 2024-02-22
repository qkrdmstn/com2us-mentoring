using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !GameManager.Instance.isGameOver) {
            GameManager.Instance.curScore += score;
            gameObject.SetActive(false);
        }
    }
}
