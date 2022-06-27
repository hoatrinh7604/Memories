using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberContentController : MonoBehaviour
{
    [SerializeField] GameObject showNumberPrefab;
    [SerializeField] Transform content;
    [SerializeField] List<GameObject> list = new List<GameObject>();
    [SerializeField] Sprite[] sprites;

    public void Spaw(int leng)
    {
        ClearContent();
        list.Clear();
        list = new List<GameObject>();
        for (int i = 0; i < leng; i++)
        {
            GameObject item = Instantiate(showNumberPrefab, Vector3.zero, Quaternion.identity, content);
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<ShowNumberController>().SetIndex(i);
            list.Add(item);
        }
    }

    public void UpdateInfo(List<int> arr)
    {
        RandomSortSprites();
        int count = 0;
        var listSprites = new List<int>() {1,1,1,1};
        var temp = new List<int>();

        for (int j = 0; j < arr.Count; j++)
        {
            temp.Clear();
            for (int i = 0; i < listSprites.Count; i++)
            {
                if (listSprites[i] != 0)
                {
                    temp.Add(i);
                }
            }

            int index = Random.Range(0, temp.Count);
            listSprites[temp[index]] = 0;
            list[temp[index]].GetComponent<ShowNumberController>().SetInfo(arr[temp[index]], sprites[count++]);
        }
    }

    private void RandomSortSprites()
    {
        var list = new List<int>() { 1,1,1,1};
        var temp = new List<int>();
        var result = new List<Sprite>() { null, null, null, null};
        for(int i = 0; i <sprites.Length; i++)
        {
            temp.Clear();
            if (list[i] != 0)
            {
                temp.Add(i);
            }
            int index = Random.Range(0, temp.Count);
            list[temp[index]] = 0;
            result[i] = sprites[temp[index]];
        }

        sprites = result.ToArray();
    }

    public void ShowItem(int index)
    {
        list[index].GetComponent<ShowNumberController>().DisplayControl(true);
    }

    public void HideItem()
    {
        for(int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<ShowNumberController>().DisplayControl(false);
        }
    }

    private void ClearContent()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
