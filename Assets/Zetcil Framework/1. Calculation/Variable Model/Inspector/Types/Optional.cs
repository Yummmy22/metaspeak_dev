using System;
using TechnomediaLabs.Internal;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TechnomediaLabs
{
	[Serializable] public class OptionalFloat : Optional<float> { }
	[Serializable] public class OptionalInt : Optional<int> { }
	[Serializable] public class OptionalString : Optional<string> { }
	[Serializable] public class OptionalGameObject : Optional<GameObject> { }
	[Serializable] public class OptionalComponent : Optional<Component> { }
}

namespace TechnomediaLabs.Internal
{
	[Serializable]
	public class Optional<T> : OptionalParent
	{
		public bool IsSet;
		public T Value;
	}
	
	[Serializable]
	public class OptionalParent {}
}

#if UNITY_EDITOR
namespace TechnomediaLabs.Internal
{
	[CustomPropertyDrawer(typeof(OptionalParent), true)]
	public class OptionalFloatPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var value = property.FindPropertyRelative("Value");
			var isSet = property.FindPropertyRelative("IsSet");

			var checkWidth = 14;
			var spaceWidth = 4;
			var valWidth = position.width - checkWidth - spaceWidth;
			
			position.width = checkWidth;
			isSet.boolValue = EditorGUI.Toggle(position, GUIContent.none, isSet.boolValue);
			
			position.x += checkWidth + spaceWidth;
			position.width = valWidth;
			if (isSet.boolValue) EditorGUI.PropertyField(position, value, GUIContent.none);

			if (GUI.changed) property.serializedObject.ApplyModifiedProperties();

			EditorGUI.EndProperty();
		}
	}
}
#endif