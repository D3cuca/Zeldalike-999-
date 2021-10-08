using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("New Scene Variables")]
    public string SceneToLoad;
    public Vector2 PlayerPosition;
    public Vector2Value PlayerStorage;


    [Header("Transition Variables")]
    public GameObject FadeInPanel;
    public GameObject FadeOutPanel;
    public float FadeWait;

    private void Awake()
    {
        if (FadeInPanel != null)
        {
            GameObject panel = Instantiate(FadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
            
                
    }
    public void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.CompareTag("Player") && !Other.isTrigger)
        {
            //SceneManager.LoadScene(SceneToLoad);
            PlayerStorage.InitialValue = PlayerPosition;
            StartCoroutine(FadeCo());

        }
    }

    public IEnumerator FadeCo()
    {
        if (FadeOutPanel != null)
        {
            Instantiate(FadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(FadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
