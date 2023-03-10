using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject, objectToPlace;
    public ARRaycastManager raycastManager;

    public bool useCursor = true;

    private void Start()
    {
        cursorChildObject.SetActive(useCursor);
    }

    private void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (useCursor)
            {
                GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

                if(hits.Count > 0)
                {
                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                }

            }
        }
    }

    void UpdateCursor()
    {
        Vector2 screenpoint = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenpoint, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if(hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

        }
    }
}
