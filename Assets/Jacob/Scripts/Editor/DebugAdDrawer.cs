using System;
using Jacob.Scripts.Controllers;
using Jacob.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomPropertyDrawer(typeof(DebugAd))]
	public class DebugAdDrawer : Drawer
	{
		private const string DebugSupportErrorMessage = "Your GameObject doesn't have the DebugSupport script. " +
		                                                "This is required for the functionality of the Debug Menu to " +
		                                                "work on that specific GameObject.";
		
		protected override void AllocateLines(SerializedProperty property)
		{
			AllocateLines(DrawerFieldLines.PropertyField);
			AllocateLines(DrawerFieldLines.PropertyField);

			var adObj = property.FindPropertyRelative("adObject");
			var adObjGameObject = adObj.objectReferenceValue as GameObject;
			
			if (adObjGameObject && !adObjGameObject.TryGetComponent<DebugSupport>(out _))
			{
				AllocateLines(DrawerFieldLines.HelpBox);
			}
		}

		protected override void GUICode(Rect position, SerializedProperty property)
		{
			var adObj = property.FindPropertyRelative("adObject");
			var adName = PropertyField(position, property.FindPropertyRelative("adName"), "Ad Name", 1);
			var adObject = PropertyField(position, adObj, "Ad Object", adName);

			var adObjGameObject = adObj.objectReferenceValue as GameObject;
			if (adObjGameObject && !adObjGameObject.TryGetComponent<DebugSupport>(out _))
			{
				HelpBox(position, DebugSupportErrorMessage, MessageType.Error, adObject);
			}
		}
	}
}