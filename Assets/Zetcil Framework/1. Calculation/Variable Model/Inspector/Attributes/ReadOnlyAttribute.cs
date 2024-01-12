using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TechnomediaLabs
{
	public class ReadOnlyAttribute : PropertyAttribute
	{
	}
}

#if UNITY_EDITOR
namespace TechnomediaLabs.Internal
{
	[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
	public class ReadOnlyAttributeDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			GUI.enabled = false;
			EditorGUI.PropertyField(position, property, label, true);
			GUI.enabled = true;
		}
	}
}
#endif