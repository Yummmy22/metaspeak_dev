using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.Events;
using Unity.VisualScripting;

public class NPC : MonoBehaviour
{

    Collider m_collider;

    [SerializeField] private NPCConversation conversation;

    public UnityEvent onConversationStart;
    public UnityEvent onConversationEnd;
    public Material effectMaterial;
    private Renderer rend;

    private Material originalMaterial;

    private void Start()
    {
        m_collider = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        originalMaterial = rend.sharedMaterial;

    }

    private void OnMouseOver()
    {
        rend.sharedMaterial = effectMaterial;
    }
    private void OnMouseExit()
    {
        rend.sharedMaterial = originalMaterial;
    }

    private void OnMouseDown()
    {
        // Invoke the start event and start the conversation
        onConversationStart.Invoke();
        ConversationManager.Instance.StartConversation(conversation);
        // Optional: You may want to unsubscribe and then resubscribe to ensure it's only called once per conversation
        ConversationManager.OnConversationEnded -= OnConversationEnded;
        ConversationManager.OnConversationEnded += OnConversationEnded;
        m_collider.enabled = false;
        rend.material = originalMaterial;
    }

    private void OnConversationEnded()
    {
        // Invoke the end event
        onConversationEnd.Invoke();
        // Unsubscribe from the event
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }
}
