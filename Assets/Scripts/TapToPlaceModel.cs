using System.Collections.Generic;
using Scanning;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class TapToPlaceModel : MonoBehaviour
    {
        private ARRayCast _arRaycast;
        [SerializeField] public GameObject door;
        [SerializeField] public GameObject doorMarker;
        private IRayCast iRaycast;
        private bool _isDoorPlacedFlag;
        private Camera _camera;

        private void Start()
        {
            if (iRaycast == null)
            {
                iRaycast = GetComponent<IRayCast>();
            }
            _arRaycast = GetComponent<ARRayCast>();
            _camera = Camera.main;
        }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount == 0)
            {
                touchPosition = default;
                return false;
            }

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = GetTouchPositon();
                return true;
            }

            touchPosition = default;
            return false;
        }

        Vector2 GetTouchPositon()
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            return touchPosition;
        }

        private void Update()
        {
            door.SetActive(ARStateHandler.Instance.IsTracking() && _isDoorPlacedFlag);

            if (_isDoorPlacedFlag)
                return;

            UpdateDoorMarker();
            if (ARStateHandler.Instance.IsTracking() && TryGetTouchPosition(out Vector2 touchPosition)
                                                     && doorMarker.activeInHierarchy)
            {
                Handheld.Vibrate();
                UpdateDoorPosition(doorMarker.transform.position, doorMarker.transform.rotation);
            }
        }


        private void UpdateDoorPosition(Vector3 position, Quaternion rotation)
        {
            if (!_isDoorPlacedFlag)
            {
                _isDoorPlacedFlag = true;
                doorMarker.SetActive(false);
                door.transform.position = doorMarker.transform.position;
                door.transform.rotation = doorMarker.transform.rotation;
            }
        }

        private void UpdateDoorMarker()
        {
            var screenCenter = _camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            List<ARRaycastHit> arRaycastHits = _arRaycast.Raycast(screenCenter, hits);
            if (arRaycastHits.Count > 0)
            {
                var canShowMarker = arRaycastHits.Count > 0 &&
                                    ARStateHandler.Instance.IsTrackingAndTrackedAreaGood;
                if (canShowMarker)
                {
                    doorMarker.SetActive(true);
                    doorMarker.transform.position = arRaycastHits[0].pose.position;
                    var cameraPosition = _camera.transform.position;
                    Vector3 targetPosition = new Vector3(cameraPosition.x,
                        doorMarker.transform.position.y,
                        cameraPosition.z);
                    doorMarker.transform.LookAt(targetPosition);
                    doorMarker.transform.eulerAngles +=
                        new Vector3(0, 180, 0);
                }
                else
                {
                    doorMarker.SetActive(false);
                }
            }
        }
    }
