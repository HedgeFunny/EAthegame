using Jacob.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomPropertyDrawer(typeof(SocketsSocket))]
	public class SocketsSocketDrawer : PropertyDrawer
	{
		private SerializedProperty _socket;
		private SerializedProperty _correctGameObject;
		private SerializedProperty _incorrectObjectPosition;
		private SerializedProperty _overrideDefaultProtection;
		private int _totalLines = 1;
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			GetProperties(property);
			var foldoutBox = new Rect(position.min.x, position.min.y, position.size.x,
				EditorGUIUtility.singleLineHeight);
			property.isExpanded = EditorGUI.Foldout(foldoutBox, property.isExpanded, label);
			if (property.isExpanded)
			{
				DrawSocketField(position);
				DrawCorrectGameObjectField(position);
				DrawIncorrectObjectPositionField(position);
				if (_incorrectObjectPosition.vector2Value == Vector2.zero)
				{
					DrawOverrideLabel(position);
					DrawOverrideButton(position);
				}
			}

			EditorGUI.EndProperty();
		}


		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			GetProperties(property);
			
			var lineHeight = EditorGUIUtility.singleLineHeight;
			_totalLines = 1;
			
			if (property.isExpanded)
			{
				if (_incorrectObjectPosition.vector2Value == Vector2.zero) _totalLines += 3;
				_totalLines += 3;
			}

			return lineHeight * _totalLines;
		}

		private void GetProperties(SerializedProperty property)
		{
			_socket = property.FindPropertyRelative("socket");
			_correctGameObject = property.FindPropertyRelative("correctGameObject");
			_incorrectObjectPosition = property.FindPropertyRelative("incorrectObjectPosition");
			_overrideDefaultProtection = property.FindPropertyRelative("overrideDefaultProtection");
		}

		private void DrawSocketField(Rect position)
		{
			var singleLineHeight = EditorGUIUtility.singleLineHeight;
			var posX = position.min.x;
			var posY = position.min.y + singleLineHeight;
			var width = position.size.x;

			var drawArea = new Rect(posX, posY, width, singleLineHeight);
			EditorGUI.PropertyField(drawArea, _socket, new GUIContent("Socket"));
		}

		private void DrawCorrectGameObjectField(Rect position)
		{
			var singleLineHeight = EditorGUIUtility.singleLineHeight;
			var posX = position.min.x;
			var posY = position.min.y + singleLineHeight * 2;
			var width = position.size.x;

			var drawArea = new Rect(posX, posY, width, singleLineHeight);
			EditorGUI.PropertyField(drawArea, _correctGameObject, new GUIContent("Correct GameObject"));
		}

		private void DrawIncorrectObjectPositionField(Rect position)
		{
			var singleLineHeight = EditorGUIUtility.singleLineHeight;
			var posX = position.min.x;
			var posY = position.min.y + singleLineHeight * 3;
			var width = position.size.x;

			var drawArea = new Rect(posX, posY, width, singleLineHeight);
			EditorGUI.PropertyField(drawArea, _incorrectObjectPosition, new GUIContent("Incorrect Object Position"));
		}

		private void DrawOverrideLabel(Rect position)
		{
			var singleLineHeight = EditorGUIUtility.singleLineHeight;
			var posX = position.min.x;
			var posY = position.min.y + singleLineHeight * 4;
			var width = position.size.x;

			var drawArea = new Rect(posX, posY, width, singleLineHeight * 2);
			EditorGUI.HelpBox(drawArea,
				"By default, "+"if your Incorrect Object Position is <0, 0>, "+
				"the Sockets script won't teleport your Object.\n" +
				"Press the Override toggle below to override this protection.", MessageType.Info);
		}

		private void DrawOverrideButton(Rect position)
		{
			var singleLineHeight = EditorGUIUtility.singleLineHeight;
			var posX = position.min.x;
			var posY = position.min.y + singleLineHeight * 6;
			var width = position.size.x;

			var drawArea = new Rect(posX, posY, width, singleLineHeight);
			EditorGUI.PropertyField(drawArea, _overrideDefaultProtection,
				new GUIContent("Override Default Protection"));
		}
	}
}