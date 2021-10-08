using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyObsRoom : DungeonRoomObserver
{
    public Door[] Doors;


    public void CheckEnemies()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i].gameObject.activeInHierarchy && i < Enemies.Length - 1)
            {
                return;
            }
           
        }   
        OpenDoors();

    }

    public void CloseDoors()
    {
        for(int i = 0; i < Doors.Length; i++)
        {
            Doors[i].Close();
        }
    }
    public void OpenDoors()
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].Opened();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                ChangeActivation(Enemies[i], true);

            }
            for (int i = 0; i < Vases.Length; i++)
            {
                ChangeActivation(Vases[i], true);

            }  
            CloseDoors();
            VirtualCam.SetActive(true);
        }

    }

    public override void OnTriggerExit2D(Collider2D other)
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
}
