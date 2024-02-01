using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{

    public Transform player;
    public GameEnding gameEnding;
    public AudioSource tinkDetected;
    public GameObject exclamation;
    
    
    bool m_IsPlayerInRange;
    bool m_HasAudioPlayed;

    float m_Timer;
    float caughtTimer = 2f;


    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
            tinkDetected.Play();
            Debug.Log("Ding!");
            m_HasAudioPlayed = true;
            m_Timer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
            m_HasAudioPlayed = false;
            exclamation.transform.localPosition = new Vector3(0f, -4, -0.6f);
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            exclamation.transform.localPosition = new Vector3(0f, 0.6f, -0.7f);

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    m_Timer += Time.deltaTime;
                    

                    if (m_Timer > caughtTimer)
                    {
                        gameEnding.CaughtPlayer();
                    }
                    
                }
            }
        }
    }
}
