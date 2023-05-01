using System;
using Jacob.Scripts.Controllers;
using Jacob.Scripts.Data.Clamp;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(Cam))]
	public class CamEditor : UnityEditor.Editor
	{
		private const string ManualCoordsHelp = "Manual Coordinates requires a ManualCoordinatesSupport script attached " +
		                                        "to a GameObject in the scene " +
		                                        "and a ManualCoordinates object (which you can create from the Create " +
		                                        "Menu) attached to the ManualCoordinatesSupport script " +
		                                        "to a GameObject so the Cam script knows what Coordinates to clamp to.";
		public override void OnInspectorGUI()
		{
			var script = target as Cam;

			script.followedObject =
				(Transform)EditorGUILayout.ObjectField("Followed Object", script.followedObject, typeof(Transform),
					true);
			ClampProperties(script);

			Utilities.CheckIfGUIChanged(script);
		}

		public static void ClampProperties(ClampingProperties script)
		{
			EditorGUILayout.LabelField("Clamp Properties", EditorStyles.boldLabel);
			script.clampProperty = EditorGUILayout.Toggle("Clamp Camera", script.clampProperty);
			using (new EditorGUI.DisabledScope(!script.clampProperty))
			{
				script.clampVerticalPosition =
					EditorGUILayout.Toggle("Clamp Vertical Position", script.clampVerticalPosition);

				script.clampingTypes = (ClampingTypes)EditorGUILayout.EnumPopup("Clamping Types", script.clampingTypes);
				switch (script.clampingTypes)
				{
					case ClampingTypes.Tilemap:
						script.tilemap =
							(TilemapCollider2D)EditorGUILayout.ObjectField("Tilemap", script.tilemap,
								typeof(TilemapCollider2D), true);
						break;
					case ClampingTypes.ManualCoordinates:
						EditorGUILayout.HelpBox(ManualCoordsHelp, MessageType.Info);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}