using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _GameName.Scripts.Tools
{
    public class PositionDemonstrator : MonoBehaviour
    {
        [SerializeField] private Material lineMaterial;

        private LineRenderer xLine;
        private LineRenderer yLine;

        private LineRenderer connectionLine;
        private Vector3 lastClickedPosition = Vector3.zero;
        
        private LineRenderer CreateNewLineRenderer()
        {
            var newLineRenderer = new GameObject("LineRenderer").AddComponent<LineRenderer>();
            newLineRenderer.useWorldSpace = true;
            newLineRenderer.positionCount = 0;
            newLineRenderer.material = lineMaterial;
            newLineRenderer.widthMultiplier = .1f;
            return newLineRenderer;
        }

        private void Awake()
        {
            Application.targetFrameRate = 5;
            
            xLine = CreateNewLineRenderer();
            xLine.startColor = Color.red;
            xLine.endColor = Color.red;
            yLine = CreateNewLineRenderer();
            yLine.startColor = Color.green;
            yLine.endColor = Color.green;

            connectionLine = CreateNewLineRenderer();
            connectionLine.startColor = Color.cyan;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition.WithZ(10));

                xLine.positionCount = 0;
                xLine.AddPoint(Vector3.zero);
                xLine.AddPoint(new Vector3(clickPosition.x, 0, 0));


                yLine.positionCount = 0;
                yLine.AddPoint(new Vector3(clickPosition.x, 0, 0));
                yLine.AddPoint(new Vector3(clickPosition.x, clickPosition.y, 0));

                var positionMarker = CreateNewLineRenderer();
                positionMarker.TurnIntoCircle(.1f, .25f);
                positionMarker.transform.forward = Vector3.up;
                positionMarker.transform.position = clickPosition;

                connectionLine.positionCount = 0;
                connectionLine.AddPoint(lastClickedPosition);

                var fromLastClickedToClickDirectionalVector = clickPosition - lastClickedPosition;
                var length = fromLastClickedToClickDirectionalVector.magnitude;
                
                fromLastClickedToClickDirectionalVector.Normalize();
                fromLastClickedToClickDirectionalVector *= length;
                
                connectionLine.AddPoint(lastClickedPosition + fromLastClickedToClickDirectionalVector);

                lastClickedPosition = clickPosition;
            }
            
            
        }
    }
}