using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateList : MonoBehaviour
{
    [SerializeField]
    private int createCount;
    public int pagePerNumber = 4;

    [SerializeField]
    private GameObject uiPrefab = null;

    private List<GameObject> list = new List<GameObject>();

    private List<GameObject> currentList = new List<GameObject>();

    [SerializeField]
    private Button leftArrow = null;
    [SerializeField]
    private Button rightArrow = null;

    private int CurrnetPage;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<createCount; ++i)
        {
            GameObject obj = Instantiate(uiPrefab, Vector3.zero, Quaternion.identity);
            if(null != obj)
            {
                obj.transform.SetParent(transform);
                if(i >= pagePerNumber)
                {
                    obj.SetActive(false);
                }
                else
                {
                    currentList.Add(obj);
                }
                list.Add(obj);
            }
        }
        if(list.Count > pagePerNumber)
        {
            rightArrow.interactable = true;
        }
    }

    public void NextPage()
    {
        ++CurrnetPage;
        currentList.ForEach(elem => 
        {
            if(null != elem)
            { 
                elem.SetActive(false);
            }
        });

        int maxIndex = list.Count < pagePerNumber * (CurrnetPage + 1) ? list.Count : pagePerNumber * (CurrnetPage + 1);
        for(int i = pagePerNumber * CurrnetPage; i < maxIndex; ++i)
        {
            list[i].SetActive(true);
            currentList[i % pagePerNumber] = list[i];
        }
        rightArrow.GetComponent<Image>().color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f),1);
        leftArrow.interactable = true;
        int pageCount = (list.Count - 1) / pagePerNumber;
        if(CurrnetPage >= pageCount)
        {
            rightArrow.interactable = false;
        }
    }

    public void PreviousPage()
    {
        --CurrnetPage;
        currentList.ForEach(elem => 
        {
            if(null != elem)
            { 
                elem.SetActive(false);
            }
        });

        int pageFistIndex = pagePerNumber * CurrnetPage;
        for(int i = pageFistIndex; i < pageFistIndex + pagePerNumber; ++i)
        {
            list[i].SetActive(true);
            currentList[i % pagePerNumber] = list[i];
        }
        rightArrow.GetComponent<Image>().color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f),1);
        rightArrow.interactable = true;
        int pageCount = (list.Count - 1) / pagePerNumber;
        if(CurrnetPage <= 0)
        {
            leftArrow.interactable = false;
        }
    }
}
