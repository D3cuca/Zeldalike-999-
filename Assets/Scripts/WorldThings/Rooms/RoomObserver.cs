using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomObserver : MonoBehaviour
{
    public Enemy[] Enemies;
    public Vase[] Vases;
    public GameObject VirtualCam;
    public Camera MainCamera;
    [SerializeField] private int VcamOrthographicSize;

    
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        VirtualCam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = VcamOrthographicSize;
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            for(int i = 0; i < Enemies.Length; i++)
            {
                ChangeActivation(Enemies[i], true) ;

            }
            for (int i = 0; i < Vases.Length; i++)
            {
                ChangeActivation(Vases[i], true);

            }
            VirtualCam.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                ChangeActivation(Enemies[i], false);

            }
            for (int i = 0; i < Vases.Length; i++)
            {
                ChangeActivation(Vases[i], false);

            }               
            VirtualCam.SetActive(false);
        }
    }

    public void OnDisable()
    {
        VirtualCam.SetActive(false);
    }

    public void ChangeActivation (Component Component, bool Activation)
    {
        Component.gameObject.SetActive(Activation);
    }
}
