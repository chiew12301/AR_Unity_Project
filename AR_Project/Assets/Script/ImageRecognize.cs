using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class ImageRecognize : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager m_arTrackedImageManager = null;

    [SerializeField] private List<GameObject> m_objectsToDisplay = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI m_debugText = null;

    private GameObject m_spawnedObject = null;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        this.m_arTrackedImageManager.trackedImagesChanged += this.OnImageChanged;
    }

    private void Update()
    {
        this.m_arTrackedImageManager.trackedImagesChanged -= this.OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        this.m_debugText.text = "Enter";
        foreach (var trackedImage in args.added)
        {
            this.m_debugText.text = trackedImage.name;
            if(trackedImage.name == "KurumiChiew")
            {
                this.m_arTrackedImageManager.trackedImagePrefab = this.m_objectsToDisplay[0];
                this.m_debugText.text += this.m_arTrackedImageManager.trackedImagePrefab.name;
            }
            else
            {
                this.m_arTrackedImageManager.trackedImagePrefab = this.m_objectsToDisplay[1];
                this.m_debugText.text += this.m_arTrackedImageManager.trackedImagePrefab.name;
            }
        }

        foreach (var trackedImage in args.updated)
        {
            this.m_debugText.text = "Updated " + trackedImage.name;
            if (trackedImage.name == "KurumiChiew")
            {
                this.m_arTrackedImageManager.trackedImagePrefab = this.m_objectsToDisplay[0];
                this.m_debugText.text += this.m_arTrackedImageManager.trackedImagePrefab.name;
            }
            else
            {
                this.m_arTrackedImageManager.trackedImagePrefab = this.m_objectsToDisplay[1];
                this.m_debugText.text += this.m_arTrackedImageManager.trackedImagePrefab.name;
            }
        }
    }

}
