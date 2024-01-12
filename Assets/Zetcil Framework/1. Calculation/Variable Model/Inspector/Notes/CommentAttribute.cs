#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class CommentAttribute : PropertyAttribute
{
    public readonly string tooltip;
    public readonly string comment;

    public CommentAttribute(string comment, string tooltip)
    {
        this.tooltip = tooltip;
        this.comment = comment;
    }
}

#endif