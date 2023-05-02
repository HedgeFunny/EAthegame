using Jacob.Scripts.Controllers;
using UnityEditor;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(ClampControl))]
	public class ClampControlEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var script = target as ClampControl;

			CamEditor.ClampProperties(script);

			Utilities.CheckIfGUIChanged(script);
		}
	}
}