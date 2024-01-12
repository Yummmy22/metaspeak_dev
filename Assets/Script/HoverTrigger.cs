using UnityEngine;
using UnityEngine.Events;

public class HoverTrigger : MonoBehaviour
{
    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;

    private void OnMouseEnter()
    {
        // Mouse masuk ke aset di Canvas
        onHoverEnter.Invoke();
    }

    private void OnMouseExit()
    {
        // Mouse keluar dari aset di Canvas
        onHoverExit.Invoke();
    }
}
