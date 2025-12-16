using UnityEditor;

namespace ZooWorld.Core.Configs.Editor
{
    [CustomEditor(typeof(AnimalDefinition))]
    public class AnimalDefinitionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            DrawProperty("Id");
            DrawProperty("Diet");
            DrawProperty("Prefab");
            DrawProperty("SpawnWeight");
            DrawProperty("MovementType");

            var movementTypeProp = serializedObject.FindProperty("MovementType");
            var jumpProp = serializedObject.FindProperty("Jump");
            var wanderProp = serializedObject.FindProperty("Wander");

            EditorGUILayout.Space();
            EditorGUI.indentLevel++;
            switch ((MovementType)movementTypeProp.enumValueIndex)
            {
                case MovementType.Jump:
                    EditorGUILayout.LabelField("Jump Settings", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(jumpProp, includeChildren: true);
                    break;
                case MovementType.Wander:
                    EditorGUILayout.LabelField("Wander Settings", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(wanderProp, includeChildren: true);
                    break;
                default:
                    EditorGUILayout.HelpBox("Unsupported movement type.", MessageType.Warning);
                    break;
            }
            EditorGUI.indentLevel--;

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawProperty(string propertyName)
        {
            var prop = serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                EditorGUILayout.PropertyField(prop);
            }
            else
            {
                EditorGUILayout.HelpBox($"Property '{propertyName}' not found.", MessageType.Warning);
            }
        }
    }
}
