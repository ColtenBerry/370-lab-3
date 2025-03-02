using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ParallaxParent : MonoBehaviour
{
    public Sprite[] backgrounds;
    public float backgroundSize;
    
    [Tooltip("Vertical spacing between background layers")]
    public float verticalSpacing = 0.5f;

    void Awake()
    {
        for (int i = 0; i < backgrounds.Length; i++) {
            GameObject go = new GameObject("Layer" + i);
            go.transform.parent = transform;
            
            // Apply vertical spacing between layers
            float verticalOffset = i * verticalSpacing;
            go.transform.localPosition = new Vector3(0, verticalOffset, 0);
            
            ParallaxScroll ps = go.AddComponent<ParallaxScroll>();
            ps.sprite = backgrounds[i];
            ps.backgroundSize = backgroundSize;
            ps.sortOrder = backgrounds.Length - i;
            ps.parallaxSpeed = (1.0f + i) / backgrounds.Length;
            ps.Setup();
        }   
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}