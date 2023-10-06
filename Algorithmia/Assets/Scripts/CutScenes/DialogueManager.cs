using UnityEditor;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public AnimationClip animationClip; // Assign your Animation Clip in the Inspector
    public string propertyPath; // Adjust this to the desired property path

    void Start()
    {
        if (animationClip != null)
        {
            // Create an EditorCurveBinding for the specified property
            EditorCurveBinding curveBinding = new EditorCurveBinding();
            curveBinding.type = typeof(GameObject);
            curveBinding.path = "";
            curveBinding.propertyName = propertyPath;

            // Get the Animation Curve for the specified property
            AnimationCurve curve = AnimationUtility.GetEditorCurve(animationClip, curveBinding);

            if (curve != null)
            {
                // Access and print the times of existing keyframes
                foreach (Keyframe keyframe in curve.keys)
                {
                    Debug.Log("Keyframe Time: " + keyframe.time);
                }
            }
            else
            {
                Debug.LogWarning("No curve found for property: " + propertyPath);
            }
        }
    }
}