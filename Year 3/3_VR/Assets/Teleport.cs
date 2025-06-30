using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleport : MonoBehaviour
{
    public GameObject m_Pointer;
    public SteamVR_Action_Boolean m_TeleportAction;

    public SteamVR_Action_Boolean b_Button;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private bool m_HasPosition = false;
    private bool m_IsTeleporting = false;
    private float m_FadeTime = 0f;

    private bool on = true;

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void Update()
    {
        if (b_Button.stateDown)
        {
            if (on)
            {
                on = false;
            }
            else
            {
                on = true;  
            }
            m_Pointer.GetComponent<MeshRenderer>().enabled = on;
        }


        if (on == true)
        {
            m_HasPosition = UpdatePointer();
            m_Pointer.SetActive(m_HasPosition);

            if (m_TeleportAction.GetStateUp(m_Pose.inputSource))
                TryTeleport();
        }
    }

    private void TryTeleport()
    {
        if(!m_HasPosition || m_IsTeleporting)
            return;

        Transform cameraRig = transform.parent;
        Vector3 headPosition = transform.parent.position;

        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translateVector = m_Pointer.transform.position - groundPosition;

        StartCoroutine(MoveRig(cameraRig, translateVector));
    }

    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        m_IsTeleporting = true;

        /*steamVR_Fade.Start(Color.black, m_FadeTime, true);*/

        yield return new WaitForSeconds(m_FadeTime);
        cameraRig.position += translation;

        /*SteamVR_Fade.Start(Color.clear, m_FadeTime, true);*/

        m_IsTeleporting = false;
    }

    private bool UpdatePointer()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            m_Pointer.transform.position = hit.point;
            return true;
        }
        return false;
    }
}
