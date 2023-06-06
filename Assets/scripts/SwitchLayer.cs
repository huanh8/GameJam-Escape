using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLayer : MonoBehaviour
{
    // switch all  children layer to players layer
    public List<GameObject> Children = new List<GameObject>();
    public List<ParticleSystem> TorchParticles = new List<ParticleSystem>();
    public string currentLayer;
  

    private void Awake() {
        //get all children recursively
        foreach (Transform child in transform)
        {
            Children.Add(child.gameObject);
        }
      // get current layer
        currentLayer = LayerMask.LayerToName(gameObject.layer);
        }
    private void Update() {
        
        //if the current layer has changed  then update 
        if (currentLayer != LayerMask.LayerToName(gameObject.layer))
        {
            ChangeLayer(LayerMask.LayerToName(gameObject.layer));
            currentLayer = LayerMask.LayerToName(gameObject.layer);
        }
    }
    public void ChangeLayer(string layer)
    {
        foreach (GameObject child in Children)
        {
            child.layer = LayerMask.NameToLayer(layer);
        }

        // switch layer in renderer of particle system
        foreach (ParticleSystem ps in TorchParticles)
        {
            ps.GetComponent<Renderer>().sortingLayerName = layer;
        }
    }
}
