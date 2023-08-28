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
        collectionInfo.name = "����ȣ";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Gumiho");
        collectionInfo.description = "����� Ȧ�� ���� ���Դ´ٰ� �˷��� ���� ��ȩ�� �޸� �����. â�Ϳ��� �������ϱ� ������ ������ ������ �������� ����� �� �󵵰� ���� �þ���.";
        collectionInfo.numbering = (int)_EMonsterType_.emtGumihoH + 1;
        collectionInfo.skillDescription = "��븦 ��Ȥ��ų �� �ִ� ����� ���� ��ɽ�Ų��.";
    }
}
