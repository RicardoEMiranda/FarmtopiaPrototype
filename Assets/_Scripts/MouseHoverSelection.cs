using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverSelection : MonoBehaviour
{
    private Camera _camera;
    private RaycastHit _hit;
    private GameObject _selectedObject;
    private GameObject _lastSelectedObject;
    private Renderer _selectedObjectRenderer;
    private Renderer _lastSelectedObjectRenderer;
    
    private Material _selectedObjectMaterial;
    private Material _lastSelectedObjectMaterial;
    private Color _selectedObjectColor;
    private Color _lastSelectedObjectColor;
    void Start()
    {
        _camera = Camera.main;
    }
    void FixedUpdate()
    {
        //pulse alpha when mouse is over selected and reset when not selected

    }
}
