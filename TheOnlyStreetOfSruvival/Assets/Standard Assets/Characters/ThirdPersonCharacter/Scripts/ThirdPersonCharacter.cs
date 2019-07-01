using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    // �����˳��ƶ��࣬���û������㣬����ʹ�õ��Ǹ���ͽ�����ײ����ϣ����û����ƵĽű�ThirdPersonUserControl��ֻ�õ���Move���������ƽ�ɫ 
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonCharacter : MonoBehaviour
    {
        /// <summary>
        /// �ƶ���ת����ٶ�
        /// </summary>
        [SerializeField] float m_MovingTurnSpeed = 360;//�ƶ���ת����ٶ�
        /// <summary>
        /// վ����ת����ٶ�
        /// </summary>
        [SerializeField] float m_StationaryTurnSpeed = 180;//վ����ת����ٶ�  
        /// <summary>
        /// ��Ծ����������
        /// </summary>
		[SerializeField] float m_JumpPower = 12f;//��Ծ����������  
        /// <summary>
        /// ����
        /// </summary>
		[Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;//����
        /// <summary>
        /// ��ƫ��ֵ
        /// </summary>
        [SerializeField] float m_RunCycleLegOffset = 0.2f; //��ƫ��ֵ  

        /// <summary>
        /// �ƶ��ٶ�
        /// </summary>
        [SerializeField] float m_MoveSpeedMultiplier = 1f;//�ƶ��ٶ�

        /// <summary>
        /// ���������ٶ�
        /// </summary>
        [SerializeField] float m_AnimSpeedMultiplier = 1f;//���������ٶ�

        /// <summary>
        /// ������ľ���
        /// </summary>
        [SerializeField] float m_GroundCheckDistance = 0.3f;//������ľ���  
        /// <summary>
        /// ����
        /// </summary>
		Rigidbody m_Rigidbody;//����
        /// <summary>
        /// ����״̬��
        /// </summary>
		Animator m_Animator;//����״̬��
        /// <summary>
        /// �Ƿ��ڵ�����
        /// </summary>
		bool m_IsGrounded;//�Ƿ��ڵ�����
        /// <summary>
        /// ������������ʼֵ
        /// </summary>
        float m_OrigGroundCheckDistance;//������������ʼֵ
        /// <summary>
        /// һ��
        /// </summary>
		const float k_Half = 0.5f;//һ��
        /// <summary>
        /// ת��ֵ
        /// </summary>
        float m_TurnAmount;//ת��ֵ
        /// <summary>
        /// ǰ��ֵ
        /// </summary>
        float m_ForwardAmount;//ǰ��ֵ
        /// <summary>
        /// ���淨����
        /// </summary>
		Vector3 m_GroundNormal;//���淨����
        /// <summary>
        /// ���Ҹ߶�
        /// </summary>
        float m_CapsuleHeight;//���Ҹ߶�
        /// <summary>
        /// ���ҵ�����
        /// </summary>
        Vector3 m_CapsuleCenter;//���ҵ�����
        /// <summary>
        /// ������
        /// </summary>
		CapsuleCollider m_Capsule;//������
        /// <summary>
        /// �Ƿ��Ƕ׷�״̬
        /// </summary>
		bool m_Crouching;//�Ƿ��Ƕ׷�״̬


        //��ʼ������\����ͽ�����
        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_CapsuleHeight = m_Capsule.height;//���Ҹ߶�
            m_CapsuleCenter = m_Capsule.center; //���ҵ�����

            //��������� XYZ�����ת
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            m_OrigGroundCheckDistance = m_GroundCheckDistance;//����һ�µ�����ֵ  
        }




        // ������FixedUpdate�е��õ�Move����  
        /// <summary>
        /// �ƶ�����
        /// </summary>
        /// <param name="move">�ƶ��ķ���</param>
        /// <param name="crouch">�Ƿ�׷�</param>
        /// <param name="jump">�Ƿ���Ծ</param>
        public void Move(Vector3 move, bool crouch, bool jump)
        {
            // ��һ���������������ת��Ϊ������ص�ת���ǰ���ٶȣ���Ҫ���ǵ���ɫͷ���ķ���  



            if (move.magnitude > 1f) move.Normalize(); //��������1�����Ϊ��λ����  

            move = transform.InverseTransformDirection(move);//����������ϵ�ķ����λ��ת��Ϊ��������ϵ

            //�ж��Ƿ��뿪����,
            //���޸�:m_GroundNormal:���淨����;
            //       m_IsGrounded //�Ƿ��ڵ�����
            //       m_Animator.applyRootMotion//�����Ƿ�Ӱ��ʵ��λ��
            CheckGroundStatus();

            // [Vector3.ProjectOnPlane]ʥ��:ͶӰ������һ��ƽ����,��ƽ���ɴ�ֱ���÷��ߵ�ƽ�涨�塣
            move = Vector3.ProjectOnPlane(move, m_GroundNormal);//���ݵ���ķ�����������һ����Ӧƽ����ٶȷ���.
            print("Move:" + move);

            //[Mathf.Atan2] :�Ի���Ϊ��λ���㲢���� y/x �ķ�����ֵ������ֵ��ʾ���ֱ�������ζԽǵĽǣ����� x ���ٱ߱߳����� y �ǶԱ߱߳���
            //����ֵΪx���һ�������ʼ��(x,y)������2D������֮��нǡ�
            m_TurnAmount = Mathf.Atan2(move.x, move.z);//����һ����λ�ǣ�����z��ļнǣ���������ת�� 
            Debug.Log("Mathf.Atan2(move.x, move.z)��������ת��" + m_TurnAmount);


            m_ForwardAmount = move.z;//����ǰ������ֵ

            //����������תת
            ApplyExtraTurnRotation();//Ӧ�ø���ת��  


            // ���ƺ��ٶȴ����ڵ��ϺͿ����ǲ�һ����  
            if (m_IsGrounded)//����ڵ�����
            {
                //����Ƿ�����
                HandleGroundedMovement(crouch, jump);
            }
            else
            {
                HandleAirborneMovement();
            }
            //��С������,���ж��Ƿ����վ��
            ScaleCapsuleForCrouching(crouch);

            //��ֻ���¶׵����򱣳��¶�
            PreventStandingInLowHeadroom();

            // �����������״̬���ݸ��������  
            UpdateAnimator(move);
        }

        /// <summary>
        /// ��С������ײ��
        /// </summary>
        /// <param name="crouch">�Ƿ�׷�</param>
        void ScaleCapsuleForCrouching(bool crouch)
        {   //���µ�һ˲��ѽ��Ҹ߶Ⱥ����ĸ߶ȼ���
            if (m_IsGrounded && crouch)
            {
                if (m_Crouching) return;
                m_Capsule.height = m_Capsule.height / 2f;
                m_Capsule.center = m_Capsule.center / 2f;
                m_Crouching = true;//�����ڶ�������Ϊtrue,��֤�������ִֻ��һ��
            }
            else
            {
                //����һ������λ�����Ӱ뾶һ���λ�ã����Ϸ���  
                Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);

                //���߳��ȣ�����ԭ�߶ȼ��ٰ뾶һ���λ��,  
                float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
                if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                { //��ߵ���˼�Ǵӽ�ɫ�ײ����϶�һ����Ȼ���Ǹ�k_Half��صĲ�����Ϊ�˷����ڶ���ʱ��������˵��棬����������ƫ��  

                    m_Crouching = true; //�����ˣ�˵����ɫ�޷��ص�վ��״̬  
                    return;
                }
                // û���������ص���ʼ��״̬  
                m_Capsule.height = m_CapsuleHeight;
                m_Capsule.center = m_CapsuleCenter;
                m_Crouching = false;
            }
        }




        /// <summary>
        /// ��ֻ���¶׵����򱣳��¶�  
        /// </summary>
        void PreventStandingInLowHeadroom()
        {
            // ��ֻ���¶׵����򱣳��¶�  
            if (!m_Crouching)
            {                                                                       //radius�뾶
                Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
                float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;//ɨ��ĳ���
                                                                                    //������ɨ����������ײ���ཻ������true�����򷵻�false��   QueryTriggerInteraction:ָ���Ƿ��ѯ����������
                if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                {
                    m_Crouching = true;//�¶�
                }
            }
        }



        /// <summary>
        /// �������¶���״̬�����ֵ
        /// </summary>
        /// <param name="move">�ƶ�����</param>
        void UpdateAnimator(Vector3 move)
        {
            //���¶�������
            m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
            m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
            m_Animator.SetBool("Crouch", m_Crouching);
            m_Animator.SetBool("OnGround", m_IsGrounded);
            if (!m_IsGrounded)
            {
                m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
            }

            // ������ֻ�����ں���ģ����Կ����ж���Ծ��������ֻ�����뿪����  
            // ����Ĵ���������������ܲ�ѭ��������ĳֻ�Ż���δ����0��0.5���ڳ�Խ��һֻ��  

            float runCycle = Mathf.Repeat(//��ȡ��ǰ�����ĸ��ţ�Repeat�൱��ȡģ  
                    m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);

            float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
            if (m_IsGrounded)
            {
                m_Animator.SetFloat("JumpLeg", jumpLeg);
            }

            // ��ߵķ�������������inspector��ͼ�е������������ʣ�������Ϊ���˶�Ӱ���ƶ����ٶ�  
            if (m_IsGrounded && move.magnitude > 0)
            {
                m_Animator.speed = m_AnimSpeedMultiplier;
            }
            else
            {
                // �ڿ��е�ʱ����  
                m_Animator.speed = 1;
            }
        }

        /// <summary>
        /// ���еĴ���,ע��,������Ծ���¶�ʱ�������õ�
        /// </summary>
        void HandleAirborneMovement()
        {
            //���ݳ������ö��������
            Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
            m_Rigidbody.AddForce(extraGravityForce);

            m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;//������ʱ���ж��Ƿ��ڵ�����  
        }

        /// <summary>
        /// ��Ծ����,����Ƿ�����
        /// </summary>
        /// <param name="crouch">�Ƿ�׷�</param>
        /// <param name="jump">�Ƿ���Ծ</param>
        void HandleGroundedMovement(bool crouch, bool jump)
        {

            //����Ƿ���������������ȷ��
            //m_Animator.GetCurrentAnimatorStateInfo(0)��ȡ��ǰ����״̬��Ϣ
            if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                // ��!
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);// //����x��z���ٶȣ�������y�����ϵ��ٶ�
                m_IsGrounded = false;//�Ƿ���ԾΪfalse
                m_Animator.applyRootMotion = false;//���ö�����Ӱ��ʵ��λ��
                m_GroundCheckDistance = 0.1f;//���������0.1
                print(m_GroundCheckDistance);
            }
        }

        /// <summary>
        /// ������ɫ����ת�����Ƕ����и���ת�ĸ�����  
        /// </summary>
        void ApplyExtraTurnRotation()
        {
            //���������ɫ������(���ǳ��˸���ת�Ķ���)
            // ������ɫ����ת�����Ƕ����и���ת�ĸ�����  
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);//ת��.  
        }

        /// <summary>
        /// �������������Ĭ�ϵĸ��˶�������������������Ƴ�λ�õ��ٶ�  
        /// </summary>
        public void OnAnimatorMove()
        {
            // ����ʵ����ʹ���������������������ƶ�������������������Ƴ�λ�õ��ٶ�  

            //����ʵ���������������Ĭ�ϵĸ��˶���
            //�����������޸�֮ǰλ���ٶȵ�Ӧ�á�
            if (m_IsGrounded && Time.deltaTime > 0)
            {
                Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;


                // ����һ��y����ƶ��ٶ� 
                v.y = m_Rigidbody.velocity.y;
                m_Rigidbody.velocity = v;
            }
        }

        /// <summary>
        /// ����Ƿ��뿪����
        /// </summary>
        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR

            // �����ڳ�����ͼ�и����������������
            //�ڳ�������ʾ�������ߣ��ӽ���0.1�״�������m_GroundCheckDistance�ľ��룬Ԥ����Ĭ����0.3  
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif

            // 0.1�������ǱȽ�С�ģ���������Ԥ���������õ�0.3�ǱȽϺõ�  
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
            {//�䵽�ˣ����淨�������ı��������������applyRootMotion��Ϊtrue��true�ĺ�����Ӧ�ù����ڵ��λ��
             //����˵�������˶����ʵ�ʽ�ɫ�������Ӱ�죬���ھ�ȷ�Ĳ��Ŷ���  
                m_GroundNormal = hitInfo.normal;//�����ߴ�����������ķ�������ֵ��m_GroundNormal
                m_IsGrounded = true;//�Ƿ��ڵ�����
                m_Animator.applyRootMotion = true;//����Ӱ��ʵ��λ��
            }
            else
            {
                m_IsGrounded = false;//�Ƿ��ڵ�����Ϊfalse
                m_GroundNormal = Vector3.up;
                m_Animator.applyRootMotion = false;//������Ӱ��ʵ��λ��,�����Ҹо���߲���ΪfalseҲ�ǿ��Ե�  
            }
        }
    }
}
