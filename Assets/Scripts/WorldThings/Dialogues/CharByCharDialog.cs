using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharByCharDialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextDisplay;
    public string[] Sentences;
    private int Index;
    public float TextSpeed;
    public float DefaultSpeedText;
    public GameObject ContinueButton;

    private void Start()
    {
        DefaultSpeedText = TextSpeed;
        StartCoroutine(Type());
    }

    public void Update()
    {
        if(TextDisplay.text == Sentences[Index])
        {
            ContinueButton.SetActive(true);
        }

        if (Input.GetButtonDown("Interact"))
        {
            if(TextDisplay.text != Sentences[Index])
            {
                TextSpeed = TextSpeed / 4;
            }
            if(TextDisplay.text == Sentences[Index])
            {
                NextSentence();
            }
        }
    }


    private IEnumerator Type()
    {
        foreach(char letter in Sentences[Index].ToCharArray())
        {
            TextDisplay.text += letter;
            yield return new WaitForSeconds(TextSpeed);
        }
    }

    public void NextSentence()
    {
        ContinueButton.SetActive(false);
        if(Index < Sentences.Length - 1)
        {
            Index++;
            TextDisplay.text = "";
            TextSpeed = DefaultSpeedText;
            StartCoroutine(Type());
        }
        else
        {
            TextDisplay.text = "";
        }
    }
}
