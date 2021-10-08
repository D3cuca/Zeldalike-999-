using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogPrefab : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI MyText;
    [SerializeField] public string CurrentSentence;
    public float TextSpeed;

    public void Setup(string NewDialog)
    {
        CurrentSentence = NewDialog;
        StartCoroutine(Type(NewDialog));
    }

    public void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (MyText.text != CurrentSentence)
            {
                TextSpeed = TextSpeed / 4;
            }
        }
    }

    private IEnumerator Type(string NewDialog)
    {
        foreach (char letter in NewDialog.ToCharArray())
        {
            MyText.text += letter;
            yield return new WaitForSeconds(TextSpeed);
        }
    }
}
