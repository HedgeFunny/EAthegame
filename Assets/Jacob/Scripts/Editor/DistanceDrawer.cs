using Jacob.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomPropertyDrawer(typeof(Distance))]
	public class DistanceDrawer : Drawer
	{
		private Distance distance;
		
		protected override void AllocateLines(SerializedProperty property)
		{
			AllocateLines(DrawerFieldLines.PropertyField);
			AllocateLines(DrawerFieldLines.PropertyField);
			AllocateLines(DrawerFieldLines.PropertyField);
			AllocateLines(DrawerFieldLines.PropertyField);
		}

		protected override void GUICode(Rect position, SerializedProperty property)
		{
			var dist = (Distance)GetPropertyInstance(property);
			
			var prop = property.FindPropertyRelative("meter");
			var meterField = PropertyField(position, prop, "Meters", 1);
			var millimeterField = FloatBox(position, meterField, "Millimeters", dist.Millimeter, true);
			var centimeterField = FloatBox(position, millimeterField, "Centimeters", dist.Centimeter, true);
			FloatBox(position, centimeterField, "Kilometers", dist.Kilometer, true);
		}
	}
}