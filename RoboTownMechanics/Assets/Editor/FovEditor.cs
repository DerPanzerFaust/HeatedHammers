using UnityEngine;
using UnityEditor;
using Interaction.Base;

namespace CustomEdit.FieldOfView
{
    [CustomEditor(typeof(Interactor))]
    public class FovEditor : Editor
    {
        private void OnSceneGUI()
        {
            Interactor interactor = (Interactor)target;

            Handles.color = Color.red;

            Handles.DrawWireArc(interactor.transform.position, Vector3.up, Vector3.forward, 360, interactor.InteractionRange);

            Vector3 angleA = interactor.DirectionFromAngle(-interactor.FieldOfViewAngler / 2, false);
            Vector3 angleB = interactor.DirectionFromAngle(interactor.FieldOfViewAngler / 2, false);

            Handles.color = Color.yellow;

            Handles.DrawLine(interactor.transform.position, interactor.transform.position + angleA * interactor.InteractionRange);
            Handles.DrawLine(interactor.transform.position, interactor.transform.position + angleB * interactor.InteractionRange);
        }
    }
}