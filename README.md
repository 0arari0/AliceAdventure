# Project: Alice Adventure

</br>

## 1. 개요
- 앨리스의 토끼 구출기

</br>

## 2. 제작기간 & 참여인원
- 약 15일
- 기획 겸 아트 1명, 프로그래머 2명

</br>

## 3. 사용언어 & 도구
- C#
- Visual Studio
- Unity 2D(에디터 버전: 2018.4.36f1)

</br>

## 4. 구동 플랫폼
- Windows(PC)

</br>

## 5. 주요 구현 이슈
<details>
<summary>오브젝트 풀링</summary>
<div markdown="1">

- 최종보스(하트퀸)가 발사하는 총알 풀링 적용

</div>
</details>

<details>
<summary>공격 패턴 다양화</summary>
<div markdown="1">

- 보스의 공격 패턴을 함수화하여 배열에 담아 랜덤으로 공격 패턴이 바뀌도록 적용

</div>
</details>

<details>
<summary>플레이어 오브젝트 및 적 오브젝트</summary>
<div markdown="1">

- 병정 오브젝트는 일정 확률로 아이템 드랍
- 플레이어(앨리스)는 아이템을 먹음으로써 강화 가능(공격력 강화, 쉴드 생성, 이동속도 강화)
- 병정들은 고유한 움직임을 가짐(플레이어에게 돌진 등)
- 플레이어는 상하좌우 방향으로 움직일 수 있음

</div>
</details>

<details>
<summary>UI</summary>
<div markdown="1">

- 1 스테이지와 2 스테이지로 구성
- 마우스 커서 모양 변경, 클릭 시 파티클 이펙트가 튀도록 추가
- 게임 플레이 시에 간략한 스토리 줄거리를 알려주는 프리뷰씬 추가
- 배경 이미지 무한 스크롤링 적용
- 씬 전환시 전환 효과 적용
- 게임 클리어 시 엔딩 스토리 진행

</div>
</details>

</br>

## 6. 스크린샷
<details>
<summary>메인화면</summary>
<div markdown="1">

<img width="362" alt="SiMaEl Main0" src="https://user-images.githubusercontent.com/76508241/219531043-ea25b6ba-afe9-474e-a0e5-17f34f06ad4a.png"> </br>
<img width="362" alt="SiMaEl Main1" src="https://user-images.githubusercontent.com/76508241/219531045-b1d23fed-e31e-459b-806d-ab97cfb0a1ff.png">
- 조작 방법 설명 창

<img width="362" alt="SiMaEl Main2" src="https://user-images.githubusercontent.com/76508241/219531046-f4e0f6cf-d43d-4dc6-94a0-bcaa20eb2fd5.png"> </br>
- 사운드 설정 창

</div>
</details>

<details>
<summary>인게임</summary>
<div markdown="1">

<img width="362" alt="SiMaEl Ingame0" src="https://user-images.githubusercontent.com/76508241/219531049-a3fc46ad-d28e-4fc3-930a-20e9810abc56.png"> </br>
- 플레이 버튼을 누른 후 진행되는 오프닝 스토리

<img width="590" alt="SiMaEl Ingame1" src="https://user-images.githubusercontent.com/76508241/219531051-876d0176-b4be-4660-9164-1c4f56335748.png"> </br>
<img width="362" alt="SiMaEl Ingame2" src="https://user-images.githubusercontent.com/76508241/219531052-a329aa33-6458-45d9-95e4-8a6774d06634.png"> </br>
- 일시정지 버튼을 눌렀을 때

<img width="362" alt="SiMaEl Ingame3" src="https://user-images.githubusercontent.com/76508241/219531053-95418969-0920-4a38-96ba-706b87aa82db.png"> </br>
- 병정에게 드랍되는 쉴드 아이템
- 총알이 강화되면 빨간색으로 일시적 변경

<img width="362" alt="SiMaEl Ingame6" src="https://user-images.githubusercontent.com/76508241/219532948-94d742b0-eace-428b-8477-d5a15ef9b7b2.png"> </br>
- 1 스테이지 보스

<img width="362" alt="SiMaEl Ingame4" src="https://user-images.githubusercontent.com/76508241/219531055-d04b18c0-a5b5-424e-883c-aa574adefaba.png"> </br>
- 1 스테이지 보스 처치 후, 최종보스 출현 전 경고 문구 표시

<img width="362" alt="SiMaEl Ingame5" src="https://user-images.githubusercontent.com/76508241/219531058-d8f2c2d7-eef8-477b-b52c-491b1a7c816b.png"> </br>
-최종보스는 장미를 날려 공격

</div>
</details>

<details>
<summary>게임오버</summary>
<div markdown="1">

<img width="362" alt="SiMaEl End0" src="https://user-images.githubusercontent.com/76508241/219531061-affcee46-c0f4-482c-847a-a548e8cd3a6b.png"> </br>
- 모든 스테이지를 클리어한 경우

<img width="362" alt="SiMaEl End1" src="https://user-images.githubusercontent.com/76508241/219531063-e4995a8a-fa3b-4723-940a-fd210f15caa5.png"> </br>
- 1 스테이지 또는 2 스테이지에서 클리어 실패한 경우

</div>
</details>

</br>

## 7. 링크
- [플레이 영상 및 다운로드](https://drive.google.com/drive/folders/14JkNgmd3A9nGALu5RteFLdkN64EDofKx)
