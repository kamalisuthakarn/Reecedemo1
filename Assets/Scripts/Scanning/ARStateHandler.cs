using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Scanning
{
    public enum ARScanningStates
    {
        NOT_TRACKING,
        TRACKING
    }


    public class ARStateHandler : MonoBehaviour
    {
        private static ARStateHandler _instance = null;
        public bool IsTrackedAreaGood => _averagePlaneArea > thresholdArea;
        public bool IsTrackingAndTrackedAreaGood => IsTracking() && IsTrackedAreaGood;
        private float _averagePlaneArea = 0;

        [SerializeField] private float thresholdArea = 1000;


        public static ARStateHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ARStateHandler>();
                    if (!_instance)
                    {
                        Debug.LogWarning("ARState Handler is needed but not present in scene");
                    }
                }

                return _instance;
            }
        }

        [SerializeField] private ARPlaneManager arPlaneManager;
        public UnityEvent<bool> isARTrackingEvent;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(this);
            }
        }

        private ARScanningStates currentTrackingState = ARScanningStates.NOT_TRACKING;

        private void Start()
        {
            if (!arPlaneManager)
            {
                arPlaneManager = FindObjectOfType<ARPlaneManager>();
            }

            if (arPlaneManager)
                arPlaneManager.planesChanged += CalculatePlaneArea;
        }

        private void Update()
        {
            CheckCurrentTrackingState();
        }


        private void CheckCurrentTrackingState()
        {
            var state = ARScanningStates.NOT_TRACKING;
            foreach (var trackable in arPlaneManager.trackables)
            {
                if (trackable.trackingState == TrackingState.Tracking)
                {
                    state = ARScanningStates.TRACKING;
                    break;
                }
            }

            if (state != currentTrackingState)
            {
                SetCurrentState(state);
            }
        }

        void SetCurrentState(ARScanningStates state)
        {
            currentTrackingState = state;
            isARTrackingEvent?.Invoke(IsTracking());
        }

        public bool IsTracking()
        {
            return currentTrackingState == ARScanningStates.TRACKING;
        }

        private void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }

        public void SetAllPlanesActive(bool value)
        {
            foreach (var plane in arPlaneManager.trackables)
                plane.gameObject.SetActive(value);
        }

        public void ToggleARTracking(bool isUserInsideTheRoom)
        {
            arPlaneManager.SetTrackablesActive(!isUserInsideTheRoom);
            arPlaneManager.enabled = !isUserInsideTheRoom;
        }

        private void CalculatePlaneArea(ARPlanesChangedEventArgs planes)
        {
            float totalArea = 0;
            foreach (var plane in planes.added)
            {
                totalArea += (plane.size.x * plane.size.y);
            }

            foreach (var plane in planes.updated)
            {
                totalArea += (plane.size.x * plane.size.y);
            }

            _averagePlaneArea += totalArea / (planes.updated.Count + planes.added.Count);
        }
    }
}