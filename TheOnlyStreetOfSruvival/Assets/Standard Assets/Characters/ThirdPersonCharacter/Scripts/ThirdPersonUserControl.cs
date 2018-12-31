using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; //ThirdPersonCharacter�Ķ��������
        private Transform m_Cam; // ���������λ��
        private Vector3 m_CamForward;  // ��ǰ�������ǰ��
        private Vector3 m_Move; //�����������ǰ�����û�������,��������������ص��ƶ�����
        private bool m_Jump;


        private void Start()
        {

            //��ȡ�����,��ߵ�ʵ�ָ����������һ����
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                // ���������������ʹ���������������ƣ�Ҳ���ⲻ��������ϣ���ģ������������پ���һ������!
                Debug.LogWarning("�������������,��Ҫ��һ��MinCamera������Player", gameObject);
            }

            // ��ȡ�����˳Ƶ��ƶ��ű����������Ϊ��  
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)//������Ծ״̬�£����������Ծ����±���  
            {
                m_Jump = Input.GetButtonDown("Jump");
            }
        }


        // �̶�֡�������������ͬ��  
        private void FixedUpdate()
        {
            // ��ȡ�û�������  
            //CrossPlatformInputManager��������ƽ̨ʹ�õ�,input��windowsƽ̨û����,������Ʒ̨���ܾͻ������
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // �����ƶ����򣬲����ݸ���ɫ  
            if (m_Cam != null)
            {
                //������������������ͬ��ǿ����ǰ����  
                //�������.forword��Y����Ϊ0���淶��
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                //  print("m_Cam.forward  =  :" + m_Cam.forward);
                //  print("m_CamForward = "+m_CamForward);
                //  print("H = "+h);
                //  print("V = " + v);
                //v*�������forWord
                //h*�������right
                m_Move = v * m_CamForward + h * m_Cam.right;
                //  print("m_move = " + m_Move);
            }
            else
            {
                // ��û�����ʱ��ֱ����������������Ϊ�ο�  
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            //��·�ٶȼ���  
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            //  �����еĲ������ݸ��ƶ���  
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;//��Ծ�Ǹ�������ֻҪ��һ�ξ͹���  
        }
    }
}
