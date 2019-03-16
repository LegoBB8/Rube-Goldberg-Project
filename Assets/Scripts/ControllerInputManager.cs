using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class ControllerInputManager : MonoBehaviour
{
    public float throwForce = 1.5f;
    public SteamVR_Action_Boolean m_GrabAction = null;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    private Interactable m_CurrentInteractable = null;
    private List<Interactable> m_ContactInteractables = new List<Interactable>();

    //For Menu
    // Steam VR 2.0 or above Input refer to https://www.youtube.com/watch?v=bn8eMxBcI70
    public SteamVR_Action_Boolean touchPadDetect = null;
    private SteamVR_Behaviour_Pose m_UIPose = null;
    public List<GameObject> objectList;
    public List<GameObject> objectPrefabList;
    public int currentObject = 0;
    public GameObject objectMenu;

    //Swipe
    public float swipeSum;
    public float touchLast;
    public float touchCurrent;
    public float distance;
    public bool hasSwipedLeft;
    public bool hasSwipedRight;
    public ObjectMenuManager objectMenuManager;


    public SteamVR_Action_Vector2 touchPadAction;
    public SteamVR_Action_Boolean spawn = null;
    public SteamVR_Action_Boolean touchPadClick = null;

    //public string sceneToLoad;

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger Down");
            Pickup();
        }

        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger Up");
            Drop();
        }

        // For Menu


        if (touchPadDetect.GetStateDown(m_Pose.inputSource))
        {
            Vector2 touchpadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);

            print(touchpadValue);
            touchCurrent = touchpadValue.y;
            distance = touchCurrent - touchLast;
            touchLast = touchCurrent;
            swipeSum += distance;
            objectMenu.SetActive(true);

            if (!hasSwipedRight)
            {
                if (swipeSum > 0.5f)
                {
                    swipeSum = 0;
                    SwipeRight();
                    hasSwipedRight = true;
                    hasSwipedLeft = false;
                }
            }
            if (!hasSwipedLeft)
            {
                if (swipeSum < -0.5f)
                {
                    swipeSum = 0;
                    SwipeLeft();
                    hasSwipedRight = false;
                    hasSwipedLeft = true;
                }
            }

        }
        if (touchPadDetect.GetStateUp(m_Pose.inputSource))
        {
            swipeSum = 0;
            touchCurrent = 0;
            touchLast = 0;
            hasSwipedLeft = false;
            hasSwipedRight = false;
            objectMenu.SetActive(false);
        }


        if (touchPadClick.GetStateDown(m_Pose.inputSource))
        {
            if (objectMenu.activeSelf)
            {
                SpawnObject();
            }
        }
    }

    void SpawnObject()
    {
        objectMenuManager.SpawnCurrentObject();
        // SteamVR_LoadLevel.Begin(sceneToLoad);
    }

    void SwipeLeft()
    {
        objectMenuManager.MenuLeft();
        Debug.Log("SwipeLeft");
    }

    void SwipeRight()
    {
        objectMenuManager.MenuRight();
        Debug.Log("SwipeRight");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Throwable"))
            if (!other.gameObject.CompareTag("Structure"))
                return;


        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Throwable"))
            if (!other.gameObject.CompareTag("Structure"))
                return;

        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {
        Debug.Log("Pick up");
        // Get nearest
        m_CurrentInteractable = GetNearestInteractable();

        // Null check
        if (!m_CurrentInteractable)
            return;
        // Already held check
        if (m_CurrentInteractable.m_ActiveHand)
            m_CurrentInteractable.m_ActiveHand.Drop();

        // Position

        m_CurrentInteractable.transform.SetParent(gameObject.transform);


        // Attach
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;
        targetBody.isKinematic = true;

        // Set active hand
        m_CurrentInteractable.m_ActiveHand = this;

    }

    public void Drop()
    {
        // Null check
        if (!m_CurrentInteractable)
            return;
        // Apply velocity
        m_CurrentInteractable.transform.SetParent(null);
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();

        if (m_CurrentInteractable.gameObject.CompareTag("Throwable"))
        {
            targetBody.velocity = m_Pose.GetVelocity() * throwForce;
            targetBody.angularVelocity = m_Pose.GetAngularVelocity();
            targetBody.isKinematic = false;
        }


        // Detatch
        m_Joint.connectedBody = null;

        // Clear Variables
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;

    }


    private Interactable GetNearestInteractable()
    {

        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (Interactable interactable in m_ContactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }




}
