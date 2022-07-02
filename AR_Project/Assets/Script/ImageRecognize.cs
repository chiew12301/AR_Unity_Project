using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class ImageRecognize : MonoBehaviour
{
    [SerializeField] private ImagesData m_soImagesData = null;

    [SerializeField] private ARTrackedImageManager m_arTrackedImageManager = null;

    [SerializeField] private List<ImageSpawnedObjectContainer> m_objectsToDisplay = new List<ImageSpawnedObjectContainer>();

    [SerializeField] private bool m_optimizeObjects = false;

    [SerializeField] private TextMeshProUGUI m_debugText = null;
    [SerializeField] private TextMeshProUGUI m_optimizeButtonText = null;

    private Dictionary<string, ImageSpawnedObjectContainer> m_spawnedObjectDict = new Dictionary<string, ImageSpawnedObjectContainer>();

    private GameObject m_spawnedObject = null;

    private void Start()
    {
        this.UpdateOptimizeButtonText();
        foreach(ImageSpawnedObjectContainer prefab in this.m_objectsToDisplay)
        {
            ImageSpawnedObjectContainer newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            
            for(int i = 0; i < this.m_soImagesData.imagesDataList.Count; ++i)
            {
                if(this.m_soImagesData.imagesDataList[i].referenceName == prefab.name)
                {
                    newPrefab.Init(this.m_soImagesData.imagesDataList[i].Imageame, this.m_soImagesData.imagesDataList[i].Imagedescription);
                }
            }

            newPrefab.gameObject.SetActive(false);

            this.m_spawnedObjectDict.Add(prefab.name, newPrefab);
        }
    }

    private void OnEnable()
    {
        this.m_arTrackedImageManager.trackedImagesChanged += this.OnImageChanged;
    }

    private void OnDisable()
    {
        this.m_arTrackedImageManager.trackedImagesChanged -= this.OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            this.m_debugText.text = "add";
            this.UpdateObject(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            this.m_debugText.text = "update";
            this.UpdateObject(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in args.removed)
        {
            this.m_debugText.text = "remove";
            this.m_spawnedObjectDict[trackedImage.name].gameObject.SetActive(false);
        }
    }

    private void UpdateObject(ARTrackedImage trackedImages)
    {
        string name = trackedImages.referenceImage.name;
        Vector3 position = trackedImages.transform.position;
        Quaternion rotation = trackedImages.transform.rotation;

        ImageSpawnedObjectContainer prefab = this.m_spawnedObjectDict[name];
        prefab.transform.position = position;
        prefab.transform.rotation = rotation;

        this.m_debugText.text += prefab.name + " " + name;

        prefab.gameObject.SetActive(true);

        if(this.m_optimizeObjects)
        {
            foreach (ImageSpawnedObjectContainer go in this.m_spawnedObjectDict.Values)
            {
                if (go.name != name)
                {
                    go.gameObject.SetActive(false);
                }
            }
        }
    }

    public void Optimizestate()
    {
        this.m_optimizeObjects = !this.m_optimizeObjects;
        this.UpdateOptimizeButtonText();
    }

    private void UpdateOptimizeButtonText()
    {
        this.m_optimizeButtonText.text = "Optimized: " + this.m_optimizeObjects;
    }
}
