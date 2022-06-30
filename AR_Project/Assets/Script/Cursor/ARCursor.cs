using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using TMPro;

public class ARCursor : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Camera m_camToUse = null;
    [SerializeField] private GameObject m_cursorChildObject = null;
    [SerializeField] private GameObject m_objectToPlane = null;
    [SerializeField] private ARRaycastManager m_raycastManager = null;

    [Header("UI")]
    [SerializeField] private Button m_modeButton = null;
    [SerializeField] private TextMeshProUGUI m_buttonText = null;

    public bool useCursor = false;

    // Start is called before the first frame update
    private void Start()
    {
        this.m_buttonText.text = "Cursor: " + this.useCursor.ToString();
        this.m_cursorChildObject.SetActive(this.useCursor);
    }

    // Update is called once per frame
    private void Update()
    {
        if(this.useCursor)
        {
            this.UpdateCursor();
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(this.useCursor)
            {
                Instantiate(this.m_objectToPlane, this.transform.position, this.transform.rotation);
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                this.m_raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

                if(hits.Count > 0)
                {
                    Instantiate(this.m_objectToPlane, hits[0].pose.position, hits[0].pose.rotation);
                }
            }
        }
    }

    private void UpdateCursor()
    {
        Vector3 screenPosition = this.m_camToUse.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        this.m_raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

        if(hits.Count > 0)
        {
            this.transform.position = hits[0].pose.position;
            this.transform.rotation = hits[0].pose.rotation;
        }
    }

    public void CursorModeOnAndOff()
    {
        this.useCursor = !this.useCursor;
        this.m_cursorChildObject.SetActive(this.useCursor);
        this.m_buttonText.text = "Cursor: " + this.useCursor.ToString();
    }

}