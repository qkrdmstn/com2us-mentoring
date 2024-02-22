using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]  //Displaying non-MonoBehaviour classes in the Inspector.
public class MapArray
{
    public GameObject[] Map;
}

public class StageManager : MonoBehaviour
{
    public MapArray[] ScoreMapPrefabs; //0 = Start, 5~10 = End
    public GameObject mapParent;

    //[SerializeField] private int mapCnt = 0;

    [SerializeField] private int curX;
    [SerializeField] private int curY;

    [SerializeField] private int prevX;
    [SerializeField] private int prevY;

    int[] weights1 = { 10, 8, 6, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1};
    int[] weights2 = { 6, 6, 6, 4, 4, 4, 2, 2, 2, 2, 2, 3, 3, 3};
    int[] weights3 = { 5, 5, 5, 5, 5, 4, 3, 3, 2, 2, 2, 4, 4, 4};

    private void Start()
    {
        InitStage();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.playTime += Time.deltaTime;
        if(mapParent.transform.childCount < 5)
        {
            SelectMaps();
            Instantiate(ScoreMapPrefabs[curX].Map[curY], new Vector3(10, -3, 0), Quaternion.identity, mapParent.transform);
        }
    }

    public void InitStage()
    {
        GameManager.Instance.playTime = 0.0f;
        GameManager.Instance.isStage = true;
        GameManager.Instance.curScore = 0;
        GameManager.Instance.isGameOver = false;
    }

    private void SelectMaps()
    {
        prevX = curX;
        prevY = curY;

        if (GameManager.Instance.playTime < 30.0f)
            curX = 0;
        else if (GameManager.Instance.playTime < 60.0f)
            curX = 1;
        else
            curX = 2;

        if (prevY == 12)
            curY = 0;
        else if (prevY >= 7)
            curY = Random.Range(1, 5);
        else
            curY = GetRandomIndex(curX);
        
        //Debug.Log(prevY.ToString() + ", " + curY.ToString());
    }

    private int GetRandomIndex(int num)
    {
        int total = 0;
        if(num == 0)
        {
            for (int i = 0; i < 14; i++)
                total += weights1[i];
        }
        else if(num == 1)
        {
            for (int i = 0; i < 14; i++)
                total += weights2[i];
        }
        else if(num == 2)
        {
            for (int i = 0; i < 14; i++)
                total += weights3[i];
        }

        float rand = Random.Range(0.0f, 1.0f);
        int pivot = Mathf.RoundToInt(total * rand);
        int weight = 0;
        int index = 0;

        if (num == 0)
        {
            for (int i = 0; i < 14; i++)
            {
                weight += weights1[i];
                if (pivot <= weight)
                {
                    index = i;
                    break;
                }
            }
        }
        else if (num == 1)
        {
            for (int i = 0; i < 14; i++)
            {
                weight += weights2[i];
                if (pivot <= weight)
                {
                    index = i;
                    break;
                }
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i < 14; i++)
            {
                weight += weights3[i];
                if (pivot <= weight)
                {
                    index = i;
                    break;
                }
            }
        }

        return index;
    }



}
