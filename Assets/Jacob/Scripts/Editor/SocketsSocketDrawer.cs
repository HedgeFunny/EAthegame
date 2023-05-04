using Jacob.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomPropertyDrawer(typeof(SocketsSocket))]
	public class SocketsSocketDrawer : Drawer
	{
		private const string OverrideWarning = "By default, " + "if your Incorrect Object Position is <0, 0>, " +
		                                       "the Sockets script won't teleport your Object.\n" +
		                                       "Press the Override toggle below to override this protection.";

		protected override void AllocateLines(SerializedProperty property)
		{
			var incorrectObjectPosition = property.FindPropertyRelative("incorrectObjectPosition");

			AllocateLines(DrawerFieldLines.PropertyField);
			AllocateLines(DrawerFieldLines.PropertyField);
			AllocateLines(DrawerFieldLines.PropertyField);
			AllocateLines(DrawerFieldLines.UnityEvent);
			if (incorrectObjectPosition.vector2Value != Vector2.zero) return;
			AllocateLines(DrawerFieldLines.HelpBox);
			AllocateLines(DrawerFieldLines.PropertyField);
		}

		protected override void GUICode(Rect position, SerializedProperty property)
		{
			var incorrectObj = property.FindPropertyRelative("incorrectObjectPosition");
			
			var socket = PropertyField(position, property.FindPropertyRelative("socket"), "Socket", 1);
			var correctGameObject = PropertyField(position, property.FindPropertyRelative("correctGameObject"),
				"Correct GameObject", socket);
			var incorrectObjectPosition =
				PropertyField(position, incorrectObj, "Incorrect Object Position", correctGameObject);
			var onIncorrect = UnityEventField(position, property.FindPropertyRelative("onIncorrect"), "On Incorrect", incorrectObjectPosition);
			if (incorrectObj.vector2Value != Vector2.zero) return;
			var overrideWarning = HelpBox(position, OverrideWarning, MessageType.Warning, onIncorrect);
			PropertyField(position, property.FindPropertyRelative("overrideDefaultProtection"),
				"Override Default Protection", overrideWarning);
		}
	}
}