using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Position Variables")]
    public Transform target;
    public float smoothing;
    public Vector2 MaxP;
    public Vector2 MinP;

    [Header("Animator")]
    public Animator Anim;

    [Header("Position Reset")]

    public Vector2Value CamMax;
    public Vector2Value CamMin;

    // Start is called before the first frame update
    void Start()
    {
        MaxP = CamMax.InitialValue;
        MinP = CamMin.InitialValue;  
        Anim = GetComponent<Animator>();
        this.gameObject.transform.position = new Vector3(target.position.x, target.position.y, transform.position.z); ;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position) 
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, MinP.x, MaxP.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, MinP.y, MaxP.y);
            // move towards a max/min

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

    public void BeginKick()
    {
        Anim.SetBool("KickActive", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo()
    {
        yield return null;
        Anim.SetBool("KickActive", false);
    }

}
