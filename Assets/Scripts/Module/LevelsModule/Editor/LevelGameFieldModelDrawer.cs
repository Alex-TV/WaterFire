
using Module.Levels.Models;
using UnityEditor;
using UnityEngine;

namespace Module.Levels.Editor
{
    [CustomPropertyDrawer(typeof(LevelGameFieldModel))]
    public class LevelProviderDrawer : PropertyDrawer
    {
        private float _heightValue = EditorGUIUtility.singleLineHeight + 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var rect = position;
            rect.height = EditorGUIUtility.singleLineHeight;

            var rowsProperty = property.FindPropertyRelative("_rowsCount");
            var collProperty = property.FindPropertyRelative("_collCount");
            var gridProperty = property.FindPropertyRelative("_grid");

            EditorGUI.PropertyField(rect, rowsProperty);
            rect.y += _heightValue;
            EditorGUI.PropertyField(rect, collProperty);
            var gridSize = collProperty.intValue * rowsProperty.intValue;
            if (gridSize <= 0)
            {
                return;
            }
            var currentGridSize = gridProperty.arraySize;
            if (currentGridSize < gridSize)
            {

                for (int i = gridProperty.arraySize; i < collProperty.intValue * rowsProperty.intValue; i++)
                {
                    gridProperty.InsertArrayElementAtIndex(i);
                }
            }

            if (currentGridSize > gridSize)
            {
                for (int i = currentGridSize - 1; i >= gridSize; i--)
                {
                    gridProperty.DeleteArrayElementAtIndex(i);
                }
            }

            rect.y += _heightValue;
            if (EditorGUI.PropertyField(rect, gridProperty, new GUIContent($"Grid[{collProperty.intValue},{rowsProperty.intValue}]")))
            {
                rect.x += _heightValue;
                for (int i = 0; i < gridProperty.arraySize; i++)
                {
                    var cell = gridProperty.GetArrayElementAtIndex(i);
                    rect.y += _heightValue;
                    var rowPos = i / collProperty.intValue;
                    var cellPos = i - rowPos * collProperty.intValue;
                    if (EditorGUI.PropertyField(rect, cell, new GUIContent($"cell[{cellPos},{rowPos}]")))
                    {
                        rect.y += _heightValue;
                        var elementPos = new Rect(rect);
                        elementPos.x += _heightValue;
                        EditorGUI.PropertyField(elementPos, cell.FindPropertyRelative("_element"),
                            new GUIContent("el:"));
                    }
                }
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var array = property.FindPropertyRelative("_grid");
            if (array.isExpanded)
            {
                int countOpenChilds = 0;
                for (int i = 0; i < array.arraySize; i++)
                {
                    if (array.GetArrayElementAtIndex(i).isExpanded)
                    {
                        countOpenChilds++;
                    }
                }
                return (array.arraySize + 1) * _heightValue + countOpenChilds * _heightValue + _heightValue * 2;
            }
            return _heightValue + _heightValue * 2;
        }
    }
}
