using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BranchingDialogController : MonoBehaviour
{
    [SerializeField] private GameObject BranchingCanvas;
    [SerializeField] private GameObject ResponseCanvas;
    [SerializeField] private GameObject DialogObjectPrefab;
    [SerializeField] private GameObject ChoiceObjectPrefab;
    [SerializeField] public TextAssetValue DialogValue;
    [SerializeField] private Story MyStory;
    [SerializeField] private GameObject DialogHolder;
    [SerializeField] private GameObject ChoiceHolder;
    [SerializeField] private ScrollRect DialogScroll;
    [SerializeField] public RawImage FaceImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableCanvas()
    {
        ResponseCanvas.SetActive(true);
        BranchingCanvas.SetActive(true);
        SetStory();
        RefreshView();
    }

    public void SetStory()
    {
        if (DialogValue.Value)
        {
            DeleteOldDialog();
            MyStory = new Story(DialogValue.Value.text);

        }
       
    }

    public void RefreshView()
    {
        while (MyStory.canContinue)
        {
            DeleteOldDialog();
            MakeNewDialog(MyStory.Continue());

        }
        if(MyStory.currentChoices.Count > 0)
        {
            EventSystem EV = EventSystem.current;
            MakeNewChoices();
            EV.firstSelectedGameObject = ChoiceHolder.GetComponentInChildren<Button>().gameObject;

        }
        else
        {
            ResponseCanvas.SetActive(false);
            BranchingCanvas.SetActive(false);
        }
        StartCoroutine(ScrollCo());
    }

    void DeleteOldDialog()
    {
        for (int i = 0; i < DialogHolder.transform.childCount; i++)
        {
            Destroy(DialogHolder.transform.GetChild(i).gameObject);
        }
    }

    IEnumerator ScrollCo()
    {
        yield return null;
        DialogScroll.verticalNormalizedPosition = 0f;
    }

    void MakeNewDialog(string NewDialog)
    {
        DialogPrefab NewDialogObject = Instantiate(DialogObjectPrefab, DialogHolder.transform).GetComponent<DialogPrefab>();
        NewDialogObject.Setup(NewDialog);
    }
    
    void MakeNewResponse(string NewResponse, int ChoiceValue)
    {
        ResponsePrefab NewResponseObject = Instantiate(ChoiceObjectPrefab, ChoiceHolder.transform).GetComponent<ResponsePrefab>();
        NewResponseObject.Setup(NewResponse, ChoiceValue);
        Button ResponseButton = NewResponseObject.gameObject.GetComponent<Button>();
        if (ResponseButton)
        {
            //This is how to put a function in a button
            ResponseButton.onClick.AddListener(delegate { ChooseChoice(ChoiceValue); });
        }
    }
    void MakeNewChoices()
    {
        for(int i = 0; i < ChoiceHolder.transform.childCount ; i++)
        {
            Destroy(ChoiceHolder.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < MyStory.currentChoices.Count; i++)
        {
            MakeNewResponse(MyStory.currentChoices[i].text, i);
        }
    }

    void ChooseChoice(int Choice)
    {
        if (DialogHolder.GetComponentInChildren<DialogPrefab>().MyText.text != DialogHolder.GetComponentInChildren<DialogPrefab>().CurrentSentence)
        {
            DialogHolder.GetComponentInChildren<DialogPrefab>().TextSpeed = DialogHolder.GetComponentInChildren<DialogPrefab>().TextSpeed / 4;
        }
        else
        {
            MyStory.ChooseChoiceIndex(Choice);
            RefreshView();
        }
    }
}
