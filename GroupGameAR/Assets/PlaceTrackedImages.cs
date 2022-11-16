using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundations;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{
    // reference to AR Tracked Image Manager Component
    private ARTrackedImageManager trackedImageManager;
    // empty list of game object prefabs
    public GameObject[] ArPrefabs;
    // dictionary array of prefabs
    private readonly Dictionary<string, GameObject> instantiatedPrefabs;

    private void Awake() 
    {
        //grab trackedImageManager
        trackedImageManager = GetComponent<ARTrackedImageManager>();

        initializedPrefabs = new Dictionary<string, GameObject>();
    }

    //Event Handler
    private void TrackedImageChanger(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // for every tracked image that has been detected get it's name
        //then loop over the array of Prefabs
        foreach (var trackedImage in eventArgs.added)
        {
            var imageName = trackedImage.referenceImage.name;
            
            foreach(var curPrefab in ArPrefabs)
            {
                // Check whether this prefab matches the tracked image name
                // AND check that this prefab hasn't been created yet
                if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0
                && !instantiatedPrefabs.ContainsKey(imageName))
                {
                    //Instantiate the prefab, parent it to the ARTrackedImage
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    //Add the prefab to the array
                    instantiatedPrefabs[imageName] = newPrefab;
                }
            }
        }
    }
    
    //add the event handler when tracked images change
    void OnEnable() 
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    void OnDisable() 
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

}
