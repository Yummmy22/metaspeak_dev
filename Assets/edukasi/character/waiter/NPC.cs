using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.Events;
using Unity.VisualScripting;
using System.Linq;

public class NPC : MonoBehaviour
{

    Collider m_collider;

    [SerializeField] private NPCConversation conversation;

    public UnityEvent onConversationStart;
    public UnityEvent onConversationEnd;
    public Material effectMaterial;
    private SkinnedMeshRenderer renderer;

    bool outlineAdded = false;
    public Material originalMaterial;

    private void Start()
    {
        m_collider = GetComponent<Collider>();
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originalMaterial = renderer.materials[0];

    }

    private void OnMouseOver()
    {
        if (!outlineAdded)
        {
            addOutline();
        }
    }
    private void OnMouseExit()
    {
        if (outlineAdded)
        {
            removeOutline();
        }
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
        
    }

    private void OnConversationEnded()
    {
        // Invoke the end event
        onConversationEnd.Invoke();
        // Unsubscribe from the event
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }

    public void addOutline()
    {
        Material[] materialsArray = new Material[renderer.materials.Length + 1];
        renderer.materials.CopyTo(materialsArray, 0);
        materialsArray[materialsArray.Length - 1] = effectMaterial;
        renderer.materials = materialsArray;
        outlineAdded = true;
    }

    public void removeOutline()
    {
        Material[] materialsArray = new Material[renderer.materials.Length - 1];
        materialsArray[0] = originalMaterial;
        renderer.materials = materialsArray;
        outlineAdded = false;
    }
}
