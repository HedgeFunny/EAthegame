using Jacob.Scripts.Controllers;
using UnityEditor;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(FlamingAd))]
	public class FlamingAdEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			// A FlamingAd script object.
			var script = target as FlamingAd;
			// A property field for the onClickedEnough Unity Event. This displays the event box.
			EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickedEnough"));
			// A Vector2 field, for the serialised value of movePlayerTo.
			script.movePlayerTo = EditorGUILayout.Vector2Field("Move Player to", script.movePlayerTo);

			// Applies the Modified Properties of serialisedObjects (this means the Unity Event's can be modified).
			serializedObject.ApplyModifiedProperties();
			// Helper method to make sure when you modify a value, Unity knows you modified it meaning you can save
			// the changes
			Utilities.CheckIfGUIChanged(script);

			// Displays values not controlled by this script.
			base.OnInspectorGUI();
		}
	}
}