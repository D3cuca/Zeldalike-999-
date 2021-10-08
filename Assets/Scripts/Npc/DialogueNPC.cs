using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueNPC : Interactable
{
    [SerializeField] private TextAssetValue DialogValue;
    [SerializeField] private TextAsset MyDialog;
    [SerializeField] private GameObject DialogCanvas;
    [SerializeField] private GameObject DialogPanel;
    [SerializeField] private GameObject ResponsePanel;
    [SerializeField] private SignalSender BranchingDialogNotification;
    [SerializeField] private RenderTexture NpcRenderTexture;
    [SerializeField] private Animator anim;
    public Transform Player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (PlayerinRange)
        {
            ChooseDirection();
            if (Input.GetButtonDown("Interact") && !DialogPanel.activeInHierarchy)
            {
                DialogCanvas.gameObject.GetComponent<BranchingDialogController>().DialogValue = DialogValue;
                DialogCanvas.gameObject.GetComponent<BranchingDialogController>().FaceImage.texture = NpcRenderTexture;
                DialogValue.Value = MyDialog;
                BranchingDialogNotification.Raise();

            }
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.CompareTag("Player"))
        {
            DialogPanel.SetActive(false);
            ResponsePanel.SetActive(false);
        }
    }

    private void ChooseDirection()
    {
        Vector2 direction = Player.position - transform.position;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            anim.SetFloat("moveY", 0);
            if (direction.x > 0)
            {
                anim.SetFloat("moveX", 1);

            }
            else if (direction.x < 0)
            {
                anim.SetFloat("moveX", -1);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            anim.SetFloat("moveX", 0);
            if (direction.y > 0)
            {
                anim.SetFloat("moveY", 1);
            }
            else if (direction.y < 0)
            {
                anim.SetFloat("moveY", -1);
            }
        }
    }
}
