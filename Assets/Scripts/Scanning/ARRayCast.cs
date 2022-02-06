using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Scanning
{
    public class ARRayCast : MonoBehaviour, IRayCast
    {
        private ARRaycastManager raycastManager;

        public virtual List<ARRaycastHit> Raycast(Vector2 screenPoint, List<ARRaycastHit> hitResults)
        {
            raycastManager.Raycast(screenPoint, hitResults, TrackableType.PlaneWithinPolygon);
            return hitResults;
        }
    }
}

