using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] itemManage;
    public string INAME;
    public GameObject inven;
    Inventory _inven;

    private void Awake()
    {
        _inven = inven.GetComponent<Inventory>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        for(int cnt = 0; cnt < _inven.items.Count; cnt++)
        {
            if (_inven.items[cnt] != null)
            {
                if (_inven.items[cnt].itemName == INAME)
                {
                    return;
                }
            }
        }
        if (_inven.items[10] == null) // 만약 10번째 인자의 아이템 값이 null이라면(아이템이 꽉 차지 않았다면)
        {
            for (int j = 0; j < itemManage.Length; j++) // itemManage.Length : 현재 11임
            {
                if (itemManage[j].itemName == INAME) // 내가 얻은 아이템의(INAME) 정보를 itemManage에서 찾아, 넘겨주기 위해서
                                                     // 이름을 비교. 만약 같다면 그것을 AddItem에 넘겨줌.
                {
                    _inven.AddItem(itemManage[j]);
                    break; // 반복해서 비교하는 for문 종료
                }
            }
        }
        else if (_inven.items[10] != null) // 만약 마지막 7번째 인자에 아이템 정보가 들어있다면
        {
            Debug.Log("슬롯이 가득 차 있습니다."); // 아이템을 보관하지 않고 슬롯이 가득 차 있다는 메세지 출력
        }
        gameObject.SetActive(false);
    }
}
