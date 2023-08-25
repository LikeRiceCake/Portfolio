using PublicEnums.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.State;
using PublicEnums;
using PublicEnums.UI.HP;

public class Gumiho : Boss
{
    protected static int sharingGumihoHp;

    public override void InitCollectionInfo()
    {
        collectionInfo.name = "구미호";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Gumiho");
        collectionInfo.description = "사람을 홀려 간을 빼먹는다고 알려진 꼬리 아홉개 달린 여우다. 창귀에게 조종당하기 전에도 간간히 마을에 들어왔으나 현재는 그 빈도가 더욱 늘었다.";
        collectionInfo.numbering = (int)_EMonsterType_.eGumihoH + 1;
        collectionInfo.skillDescription = "상대를 매혹시킬 수 있는 기운을 날려 방심시킨다.";
    }
}
