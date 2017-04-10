using UnityEngine;
using UnityEditor;
using System.Collections;

namespace orisox.com
{
    [CustomEditor(typeof(LightmapSave))]
    public class LightmapSaveEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var Obj = target as LightmapSave;

            if (GUILayout.Button("Bake Lightmap Async"))
            {
                Lightmapping.giWorkflowMode = Lightmapping.GIWorkflowMode.OnDemand;
                UnityEditor.Lightmapping.BakeAsync();
            }

            if (GUILayout.Button("Save"))
            {
                Obj.Save();
            }

            if (GUILayout.Button("Clear"))
            {
                Obj.Clear();
            }
        }
    }
}
