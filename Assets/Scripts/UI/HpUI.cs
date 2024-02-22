using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [SerializeField] private Player player;
    public GameObject[] hearts;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    
    public void InActiveHP(int damaged)
    {
        for(int i=0; i<damaged; i++)
        {
            int index = player.HP -1 - i;
            if (index < 0)
                return;
            hearts[index].SetActive(false);
        }
    }
}
