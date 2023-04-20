using System;
using Jacob.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	public class Drawer : PropertyDrawer
	{
		private int _totalLines;
		private readonly float _lineHeight = EditorGUIUtility.singleLineHeight;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			_totalLines = 1;
			if (!property.isExpanded) return _lineHeight * _totalLines;
			AllocateLines(property);
			return _lineHeight * _totalLines;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			FoldOutCode(position, property, label);
			if (property.isExpanded)
			{
				GUICode(position, property);
			}

			EditorGUI.EndProperty();
		}

		private void FoldOutCode(Rect position, SerializedProperty property, GUIContent label)
		{
			var foldoutBox = new Rect(position.min.x, position.min.y, position.size.x, _lineHeight);
			property.isExpanded = EditorGUI.Foldout(foldoutBox, property.isExpanded, label);
		}

		protected virtual void GUICode(Rect position, SerializedProperty property)
		{
			throw new NotImplementedException();
		}

		protected int PropertyField(Rect position, SerializedProperty property, string label, int currentLine)
		{
			var rectValues = RectValues(position, currentLine);
			var drawArea = new Rect(rectValues.posX, rectValues.posY, rectValues.width, _lineHeight);
			EditorGUI.PropertyField(drawArea, property, new GUIContent(label));
			return currentLine + DrawerFieldLines.PropertyField;
		}

		protected int HelpBox(Rect position, string message, MessageType messageType, int currentLine)
		{
			var rectValues = RectValues(position, currentLine);
			var drawArea = new Rect(rectValues.posX, rectValues.posY, rectValues.width, _lineHeight * 2);
			EditorGUI.HelpBox(drawArea, message, messageType);
			return currentLine + DrawerFieldLines.HelpBox;
		}

		protected virtual void AllocateLines(SerializedProperty property)
		{
			throw new NotImplementedException();
		}

		protected void AllocateLines(int lineAmount)
		{
			_totalLines += lineAmount;
		}

		private DrawerRectValues RectValues(Rect position, int currentLine)
		{
			return new DrawerRectValues
			{
				posX = position.min.x,
				posY = position.min.y + _lineHeight * currentLine,
				width = position.size.x
			};
		}
	}
}