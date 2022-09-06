using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMonster : MonoBehaviour
{
    public GameObject Wolf;
    public GameObject Skeleton;
    public CreateMonster CM;
    public GameObject Frame;
    public GameObject[] Planes;
    public GameObject PO;
    public PlayerMove PM;
    public Text dieText;

    public List<GameObject> wolflist = new List<GameObject>();
    public List<GameObject> skeletonlist = new List<GameObject>();
    int WSCnt;

    // Start is called before the first frame update
    void Start()
    {
        WSCnt = 15;
        PM = PO.GetComponent<PlayerMove>();
        CM = this;
        CreateWolfList(WSCnt);
        InvokeRepeating("CreateWolf", 0f, 60f);
        InvokeRepeating("CreateSkeleton", 0f, 60f);
    }

    void CreateWolf()
    {
        for (int j = 0; j < wolflist.Count; j++)
        {
            int planeselect = Random.Range(0, 12);
            if (wolflist[j].activeSelf == false)
            {
                wolflist[j].transform.position = Planes[planeselect].transform.position;
                wolflist[j].SetActive(true);
            }
        }
    }

    void CreateSkeleton()
    {
        for (int j = 0; j < skeletonlist.Count; j++)
        {
            int planeselect = Random.Range(0, 12);
            if (skeletonlist[j].activeSelf == false)
            {
                skeletonlist[j].transform.position = Planes[planeselect].transform.position;
                skeletonlist[j].SetActive(true);
            }
        }
    }

    void CreateWolfList(int cnt)
    {
        GameObject Wolfs = new GameObject();
        GameObject Skeletons = new GameObject();

        Wolfs.name = "Wolfs";
        Skeletons.name = "Skeletons";

        for (int i = 0; i < cnt; i++)
        {
            GameObject wolfobj = Instantiate(Wolf, Wolfs.transform);
            GameObject skeletonobj = Instantiate(Skeleton, Skeletons.transform);
            wolfobj.name = "wolf_" + i.ToString("00");
            skeletonobj.name = "skeleton_" + i.ToString("00");
            wolfobj.GetComponent<Wolf>().cm = CM;
            skeletonobj.GetComponent<Skeleton>().cm = CM;
            wolfobj.SetActive(false);
            skeletonobj.SetActive(false);
            wolflist.Add(wolfobj);
            skeletonlist.Add(skeletonobj);
        }
    }
}
