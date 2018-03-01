using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {
     
    public  List<bird> birds;
    public  List<pig> pigs;
    public static gameManager instance;
    public Vector3 temppos;
    public GameObject lose;
    public GameObject win;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    private int score=0;
    public void Awake()
    {
        instance = this;
        temppos = birds[0].transform.position;
    }
    public void start()
    {
        initialize();
    }


    public void initialize()
    {
        for (int i = 0; i < birds.Count;i++)
        {
            if (i == 0)
            {
                birds[0].enabled = true;
                birds[0].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }

        }
    }

    public void nextbirds()
    {
        if(pigs.Count>0)
        {
            if(birds.Count>0)
            {
                initialize();
            }
            else //failed
            {
                lose.SetActive(true);
            }

        }
        else //succeeded
        {
            win.SetActive(true);
        }
    }
    public void showstar()
    {
        score = score + birds.Count * 5000;
        if(score-5000*pigs.Capacity<10000)
        {
            star1.SetActive(true);
        }
        else if(score - 5000 * pigs.Capacity <15000)
        {
            star1.SetActive(true);
            Invoke("showstar2", 0.8f);
        }
        else
        {
            star1.SetActive(true);
            Invoke("showstar2", 0.8f);
            Invoke("showstar3", 1.6f);

        }
    }
    public void showstar2()
    {
        star2.SetActive(true);
    }
    public void showstar3()
    {
        star3.SetActive(true);
    }
    public void addscore(int i)
    {
        score = score + i;
    }
}
