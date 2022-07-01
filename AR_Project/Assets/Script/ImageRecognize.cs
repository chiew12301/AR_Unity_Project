using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageRecognize : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager m_arTrackedImageManager = null;

    [SerializeField] private List<GameObject> m_objectsToDisplay = new List<GameObject>();

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
        foreach(var trackedImage in args.added)
        {
            if(trackedImage.name == "KurumiChiew")
            {
                this.m_arTrackedImageManager.trackedImagePrefab = this.m_objectsToDisplay[0];
            }
            else
            {
                this.m_arTrackedImageManager.trackedImagePrefab = this.m_objectsToDisplay[1];
            }
        }
    }

}
