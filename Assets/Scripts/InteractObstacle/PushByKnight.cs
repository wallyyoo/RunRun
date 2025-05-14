using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushByKnight : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool isTouchingKnight = false;
    private bool isTouchingGround = false;

    // Knight 레이어 설정
    [SerializeField] private string knightLayerName = "Knight"; // Inspector에서 Knight 레이어 이름 설정
    private int knightLayerNumber; // Knight 레이어 번호

    // Ground 레이어 설정
    [SerializeField] private string groundLayerName = "Ground"; // Inspector에서 Ground 레이어 이름 설정
    private int groundLayerNumber; // Ground 레이어 번호

    [SerializeField] private float pushForce = 3.0f; // 밀리는 힘 증가
    [SerializeField] private float gravityScale = 1.0f; // 중력 크기

    private Transform knightTransform; // 충돌한 Knight의 Transform
    private float checkGroundRadius = 0.2f; // 바닥 체크 반경

    [SerializeField] private Transform groundCheck; // 바닥 체크 위치 (Inspector에서 설정)

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;

        // Knight 레이어 번호 가져오기
        knightLayerNumber = LayerMask.NameToLayer(knightLayerName);

        // Ground 레이어 번호 가져오기
        groundLayerNumber = LayerMask.NameToLayer(groundLayerName);

        // groundCheck가 없으면 현재 오브젝트의 위치에서 약간 아래로 설정
        if (groundCheck == null)
        {
            GameObject checkObj = new GameObject("GroundCheck");
            checkObj.transform.parent = transform;
            checkObj.transform.localPosition = new Vector3(0, -0.5f, 0); // 오브젝트 하단에 위치
            groundCheck = checkObj.transform;
            Debug.Log("GroundCheck 객체 자동 생성됨");
        }

        // 시작 시 디버그 정보 출력
        Debug.Log($"PushByKnight 초기화됨. 객체 이름: {gameObject.name}, 레이어: {gameObject.layer}");
        Debug.Log($"Knight 레이어 이름: {knightLayerName}, 번호: {knightLayerNumber}");
        Debug.Log($"Ground 레이어 이름: {groundLayerName}, 번호: {groundLayerNumber}");
    }

    private void FixedUpdate()
    {
        // 바닥 체크
        CheckGround();

        // Knight와 충돌 중이면 힘을 가함
        if (isTouchingKnight && knightTransform != null)
        {
            // 충돌 중에는 Dynamic으로 변경
            if (_rigidbody.bodyType != RigidbodyType2D.Dynamic)
            {
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                _rigidbody.gravityScale = gravityScale; // 중력 적용
                Debug.Log("Rigidbody 타입 변경: Dynamic (Knight 충돌)");
            }

            // Knight의 위치에서 오브젝트로의 방향 벡터 계산
            Vector2 pushDirection = (transform.position - knightTransform.position).normalized;

            // 힘 적용
            _rigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Force);
            Debug.Log($"밀기 힘 적용: {pushDirection * pushForce}, 현재 속도: {_rigidbody.velocity}");
        }
        else if (!isTouchingKnight)
        {
            // Knight와 충돌이 없으면
            if (_rigidbody.bodyType == RigidbodyType2D.Kinematic)
            {
                // 이미 Kinematic이면 아무것도 하지 않음
                return;
            }

            if (isTouchingGround)
            {
                // 바닥과 닿아있으면 Kinematic으로 변경
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.angularVelocity = 0f;
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;
                Debug.Log("Rigidbody 타입 변경: Kinematic (바닥 접촉)");
            }
            else
            {
                // 바닥과 닿아있지 않으면 중력 유지
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                _rigidbody.gravityScale = gravityScale;
                Debug.Log("중력 적용 중 (공중에 있음)");
            }
        }
    }

    private void CheckGround()
    {
        // 바닥 체크를 위한 원형 영역 검사
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, checkGroundRadius);

        // 이전 상태 저장
        bool wasGrounded = isTouchingGround;
        isTouchingGround = false;

        // 검출된 모든 콜라이더를 확인
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject &&
                (col.gameObject.layer == groundLayerNumber || col.gameObject.CompareTag("Ground")))
            {
                isTouchingGround = true;
                break;
            }
        }

        // 바닥 상태 변화 시 로그 출력
        if (wasGrounded != isTouchingGround)
        {
            Debug.Log($"바닥 접촉 상태 변경: {isTouchingGround}");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"충돌 발생: {collision.gameObject.name}, 레이어: {collision.gameObject.layer}");

        // Knight 확인
        if (collision.gameObject.name.Contains("Knight") ||
            collision.gameObject.layer == knightLayerNumber)
        {
            Debug.Log("Knight와 충돌 시작!");
            isTouchingKnight = true;
            knightTransform = collision.transform;
        }

        // Ground 확인
        if (collision.gameObject.name.Contains("Ground") ||
            collision.gameObject.layer == groundLayerNumber ||
            collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground와 충돌 시작!");
            isTouchingGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Knight 확인
        if (collision.gameObject.name.Contains("Knight") ||
            collision.gameObject.layer == knightLayerNumber)
        {
            isTouchingKnight = true;
            knightTransform = collision.transform;
        }

        // Ground 확인
        if (collision.gameObject.name.Contains("Ground") ||
            collision.gameObject.layer == groundLayerNumber ||
            collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log($"충돌 종료: {collision.gameObject.name}");

        // Knight 확인
        if (collision.gameObject.name.Contains("Knight") ||
            collision.gameObject.layer == knightLayerNumber)
        {
            Debug.Log("Knight와 충돌 종료!");
            isTouchingKnight = false;
            knightTransform = null;
        }

        // Ground 확인
        if (collision.gameObject.name.Contains("Ground") ||
            collision.gameObject.layer == groundLayerNumber ||
            collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground와 충돌 종료!");
            // 직접 설정하지 않고 CheckGround에서 처리하도록 함
            // isTouchingGround = false;
        }
    }

    // Gizmos로 바닥 체크 영역 시각화
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isTouchingGround ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkGroundRadius);
        }
    }
}