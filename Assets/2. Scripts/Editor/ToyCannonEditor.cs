using UnityEngine;
using UnityEditor;

//This is for MapScene's Cannon control

[CustomEditor(typeof(ToyCannon))]
public class ToyCannonEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var toyCannon = (ToyCannon)target;
        if (toyCannon == null) return;

        if (GUILayout.Button("Launch")) {
            toyCannon.Trigger();
        }
        if (GUILayout.Button("Reset to Default")) {
            toyCannon.ResetToIdentity();
        }
        if (GUILayout.Button("Recalculate Force")) {
            toyCannon.CalculateForce();
        }
    }
}
