/*----------------------------------------
File Name: UIPointer.cs
Purpose: Creates UI laser from hands
Author: Tarn Cooper
Modified: 17 November 2020

Based on work of: Eric Van de Kerckhove
Resource: https://www.raywenderlich.com/9189-htc-vive-tutorial-for-unity
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/

using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

/// <summary>
/// Creates UI laser from hands
/// </summary>
public class UIPointer : MonoBehaviour
{
    SteamVR_Behaviour_Pose controllerPose;
    public float maxRange;
    public LayerMask interactMask;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean selectAction;
    public Transform linePoint;
    Button selectedElement = null;
    public GameObject laserPrefab;
    GameObject laser;
    Transform laserTransform;
    Vector3 hitPoint;

    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    void Start()
    {
        controllerPose = GetComponent<SteamVR_Behaviour_Pose>();
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        laser.transform.parent = gameObject.transform;
        laser.SetActive(false);
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    void Update()
    {
        RaycastHit hit;
        //Test if ray collided wit something
        if (Physics.Raycast(controllerPose.transform.position, linePoint.forward, out hit, maxRange, interactMask))
        {
            ShowLaser(hit);
            hitPoint = hit.point;
            Button element;

            //Checks if has UI button component
            if (hit.transform.TryGetComponent(out element))
            {
                DisableSelection();
                selectedElement = element;
                element.targetGraphic.color = element.colors.highlightedColor;
                //Checks if button is pressed
                if (selectAction.GetLastStateDown(handType))
                {
                    element.targetGraphic.color = element.colors.pressedColor;
                    element.onClick.Invoke();
                }

            }
            else
            {
                DisableSelection();
            }          
        }
        else
        {
            DisableSelection();
            laser.SetActive(false);
        }
    }

    /// <summary>
    /// Displays laser from finger to point
    /// </summary>
    /// <param name="hit">Where did the ray hit?</param>
    void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laser.transform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, 0.5f);

        laserTransform.LookAt(hitPoint);

        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }

    /// <summary>
    /// Disables previous selection
    /// </summary>
    void DisableSelection()
    {
        //Checks if element is selected
        if (selectedElement != null)
        {
            selectedElement.targetGraphic.color = selectedElement.colors.normalColor;
            selectedElement = null;
        }
    }
}
