using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImagesDataClass
{
    public string referenceName;
    public string Imageame;
    public string Imagedescription;
}

[CreateAssetMenu(fileName = "ImagesData", menuName = "SO_ImagesData", order = 1)]
public class ImagesData : ScriptableObject
{
    public List<ImagesDataClass> imagesDataList;
}
