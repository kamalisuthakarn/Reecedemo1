using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Scanning
{
    public interface IRayCast
        {
            public List<ARRaycastHit> Raycast(Vector2 screenPoint, List<ARRaycastHit> hitResults);
        }
}
