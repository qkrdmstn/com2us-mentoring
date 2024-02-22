using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private void Start()
    {
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = "0";
    }

    private void Update()
    {
        scoreText.text = GameManager.Instance.curScore.ToString();
    }
}
