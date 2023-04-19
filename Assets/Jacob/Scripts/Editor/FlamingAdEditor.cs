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

			// A boolean field, changes the serialised value of runObsoleteCode (this means inside the .unity file).
			script.runObsoleteCode = EditorGUILayout.Toggle("Run Obsolete Code", script.runObsoleteCode);

			// A DisabledScope block. Fields in this block are disabled if the expression in the DisabledScope
			// constructor is true. For this one, this is a flipped runObsoleteCode (so this means if its false,
			// it would be true, disabling the fields in the block).
			using (new EditorGUI.DisabledScope(!script.runObsoleteCode))
			{
				// Another boolean field, for the serialised value of mainSceneActive
				script.mainSceneActive = EditorGUILayout.Toggle("Main Scene Active", script.mainSceneActive);
				// A property field for the onClickedEnough Unity Event. This displays the event box.
				EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickedEnough"));
			}

			// A string field, for the serialised value of layer.
			script.layer = EditorGUILayout.TextField("Layer", script.layer);
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