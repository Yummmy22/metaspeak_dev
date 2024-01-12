using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.Events;
public class Cashier : MonoBehaviour
{

    Collider m_collider;

    [SerializeField] private NPCConversation myConversation;

    public UnityEvent onConversationStart;
    public UnityEvent onConversationEnd;

    private void Start()
    {
        m_collider = GetComponent<Collider>();
    }

    private void OnMouseOver()
    {
        // hoverring
    }
    private void OnMouseDown()
    {
        // Invoke the start event and start the conversation
        onConversationStart.Invoke();
        ConversationManager.Instance.StartConversation(myConversation);
        // Optional: You may want to unsubscribe and then resubscribe to ensure it's only called once per conversation
        ConversationManager.OnConversationEnded -= OnConversationEnded;
        ConversationManager.OnConversationEnded += OnConversationEnded;
        m_collider.enabled = false;

    }

    private void OnConversationEnded()
    {
        // Invoke the end event
        onConversationEnd.Invoke();
        // Unsubscribe from the event
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }
}
