using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{


    public Vector2 CamChangeMax;
    public Vector2 CamChangeMin;
    public Vector3 PlayerCh;
    private CameraMovement Cam;  //how to reference a script (needs completion)
    public bool needtext;  //does the place need text
    public string placeName;  //new text
    public GameObject text;   // the text object itself
    public Text placeText;    // Text on that object
    
    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main.GetComponent<CameraMovement>(); // complete the script reference Object.Unity.Getcomponent <script>
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Cam.MaxP += CamChangeMax;
            Cam.MinP += CamChangeMin;
            other.transform.position += PlayerCh;
        }
        if(needtext && !other.isTrigger)
        {
            StartCoroutine(placeNameCo());
        }
    }

     public IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
