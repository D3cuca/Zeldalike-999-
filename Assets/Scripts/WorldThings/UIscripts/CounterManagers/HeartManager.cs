using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] Hearts;
    public Sprite FullHeart;
    public Sprite HalfHeart;
    public Sprite EmptyHeart;
    public FloatValue HeartContainers;
    public FloatValue PlayerCurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    public void InitHearts ()
    {
        for (int i = 0; i < HeartContainers.RunTimeValue; i++)
        {
            if (i < Hearts.Length)
            {
                Hearts[i].gameObject.SetActive(true);
                Hearts[i].sprite = FullHeart;
            }
        }
    }

    public void UpdateHearts ()
    {
        InitHearts();
        float tempHealth = PlayerCurrentHealth.RunTimeValue / 2;
        for (int i = 0; i< HeartContainers.RunTimeValue; i++)
        {
            if (i <= tempHealth - 1)
            {
                Hearts[i].sprite = FullHeart;
            }   else if (i >= tempHealth)
            {
                Hearts[i].sprite = EmptyHeart;
            }
            else
            {
                Hearts[i].sprite = HalfHeart;
            }
        }
    }
}
