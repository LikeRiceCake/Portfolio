using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items; // Item 클래스(이하 Item) 를 담을 수 있는 items 리스트를 생성
    // Item 에는 이미지와 이름을 담고 있는 변수들이 있음

    [SerializeField] // Inspector창에 보이게 해줌
    private Transform slotParent; // slotParent(슬롯들의 부모 오브젝트의 위치를 넣을 변수)
    [SerializeField]
    public Slot[] slots; // Slot클래스를 여러 개 담을 수 있는 배열 변수 slots 생성(Inspector창에 보임)

    private void OnValidate() // 스크립트가 수정되거나 인스펙터창에서 해당 스크립트가 변경될 경우 호출됨(플레이 모드가 아닐 때 호출)
    {
        slots = slotParent.GetComponentsInChildren<Slot>(); // 클래스 Slot형 배열 변수 slots에 GetComponentsInChildren을 사용해 Slot 클래스(컴포넌트)를 가져옴
        // 부모 오브젝트.GetComponentsInChildren<원하는 컴포넌트>() : 적혀있는 부모 오브젝트 아래에 있는 모든 자식 오브젝트들의 원하는 컴포넌트들을 전부 가져옴
        // 즉 slots 안에는 각각 Slot_00n의 Image에 있는 Slot 클래스가 담겨 있음. 순서는 List 변수이기 때문에 맨 위(001)부터 인덱스 0에 쌓임
    }
    // Start is called before the first frame update
    void Awake()
    {
        FreshSlot();
    }

    // Update is called once per frame
    public void FreshSlot()
    {
        int i = 0; // 아래 for문에 똑같이 적용시키기 위해
        for (; i < items.Count && i < slots.Length; i++) // i가 items.Count보다 작고 slots.Length보다 작을 경우
            // 여기서 items는 들어가 있는 아이템의 갯수다 현재는 4이며, slots는 아이템을 넣을 슬롯의 갯수인데 현재는 8임
            // items.Count을 사용한 이유는 아이템이 들어있는 만큼만 작동해주면 되기 때문이고
            // slots.Length를 덧붙인 이유는 아이템의 갯수가 아이템 슬롯보다 넘어가면 작동해줄 필요가 없기 때문
        {
            slots[i].item = items[i];
            // items[i] : 아이템[i]의 이름과 이미지 정보
            // slots[i].item : 만약 i가 0이라면 slots[0]에는 Slot_001 이미지에 있는 Slot 컴포넌트(스크립트)가 들어가 있는데 해당 컴포넌트에 있는
            // item에 items[i]를 넣음
            // item은 _item에 접근하기 위한 변수. 이곳에 정보를 넣으면 _item에 정보가 들어감
            // item에 값을 넣으면 set이 실행되는데, 해당 set아래에 있는 if도 실행되어 이미지가 표현됨
        }
        for(; i < slots.Length; i++) // i가 slots.Length보다 작을 경우(위의 for문이 실행되고 나서 남은 i가 이곳으로 옴)
            // 위에서 5개의 아이템이 있다면 i가 4까지 진행되는데, 5가 되면 실행되지 않음
            // 그 때 해당 for문으로 그 i를 그대로 가지고 와서 나머지 슬롯들에 null을 넣어주게 됨
        {
            slots[i].item = null; 
        }
    }

    public void AddItem(Item _item) // 아이템을 얻었을 경우 실행할 함수(Item를 자료형으로 하는 _item 변수 생성)
    {    
         for (int i = 0; i < slots.Length; i++)
        {
            if (items[i] == null) // 만약 i번째 아이템 슬롯이 비어있다면
            {
                items[i] = _item; // items의 i번째 인자에 _item(넣을 아이템의 정보가 들어있음)을 넣음
                FreshSlot();
                break;
            }
        }  
    }

    public void RemoveItem(int idx)
    {
        items[idx] = null;
        FreshSlot();
    }
}
