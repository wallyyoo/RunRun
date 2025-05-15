# 🧩 Knight & Wizard: 동굴 퍼즐 어드벤처 (Unity 2D 게임)

**마법사와 기사가 협력하여 퍼즐을 풀고 함정을 극복하며 동굴을 탈출하는 2D 플랫포머 어드벤처 게임입니다.**

---

## 🎮 주요 기능

- 🧙 **마법사(Wizard)**  
  - 원거리 투사체 공격  
  - 점프 / 애니메이션 분기  
  - 특정 마법 오브젝트와만 상호작용

- 🛡 **기사(Knight)**  
  - 근접 공격  
  - 특정 오브젝트를 밀 수 있음  
  - 스위치나 버튼을 눌러 퍼즐 해제

- 🧱 **퍼즐 상호작용 시스템**  
  - 물리 기반 푸시 오브젝트  
  - 버튼 → 문 열림  
  - 보석 수집 시스템

- 🎛 **UI 및 씬 전환**  
  - 설정 UI (ESC 키로 토글)  
  - 페이드 인/아웃 효과  
  - 커스텀 씬 전환 매니저

---

## 🏗 폴더 및 구조

Assets/
├── Scripts/
│ ├── Player/ (KnightController, WizardController, BaseController)
│ ├── Interact/ (PushByKnight, BtnController, GemController)
│ ├── UI/ (SettingUIController, UIFaderByElement)
│ └── Scene/ (CustomSceneManager)
├── Animations/
├── Prefabs/



---

## 🧠 기술적 특징

- **레이어 기반 충돌 제어**  
  - Knight 레이어 ↔ Push_Knight 레이어만 충돌 허용  
  - 마법사는 밀기 불가능

- **Rigidbody 상태 전환**  
  - Knight 충돌 시 `Dynamic`, 벗어나면 `Kinematic`  
  - 바닥 없을 경우 중력 적용

- **바닥 감지 시스템**  
  - `Physics2D.OverlapCircle`로 Ground 접촉 여부 체크  
  - Gizmo로 시각적 디버깅 가능

- **애니메이션 파라미터 제어**  
  - `IsRun`, `IsJump`, `IsAttack`, `IsDie` 등 상태 기반 분기  
  - 죽음 애니메이션은 AnyState → Die 전이로 즉시 출력

---

## 🛠 개발 환경

- Unity 2022.3.1f1 (LTS 버전)
- C#
- Unity UI 시스템 (Canvas, CanvasGroup 등)
- Unity 무료 에셋 사용

---

## 🌱 향후 개선 예정

- 캐릭터 전환 기능 추가  
- 퍼즐 난이도 조절 시스템  
- 배경음악 및 효과음 추가  
- 세이브/로드 기능 도입

---

© 2025 Wally Yoo – 무단 전재 및 복제 금지.

