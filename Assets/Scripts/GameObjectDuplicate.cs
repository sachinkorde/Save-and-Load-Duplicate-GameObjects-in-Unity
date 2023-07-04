using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDuplicate : MonoBehaviour
{
    RaycastHit2D hit;
    GameObject duplicateGo;
    public Transform parent;
    public Transform dataContainer;
    public List<SaveData> saveListData = new List<SaveData>();
    public SaveDataList lastDataSave = new SaveDataList();
    
    void Start()
    {
        Load();
    }

    public void Save()
    {
        saveListData.Clear();
        for(int i =0; i<parent.childCount; i++)
        {
            SaveData x = new SaveData();
            x.id = parent.GetChild(i).GetComponent<DragAndDropDuplicate>().id;
            x.pos = parent.GetChild(i).transform.localPosition;

            saveListData.Add(x);
        }

        string temp = saveListData.ToString();
        SaveDataList ab = new SaveDataList();

        ab.saveList = saveListData;
        temp = JsonUtility.ToJson(ab);
        PlayerPrefs.SetString("datasave", temp);

        Debug.Log(PlayerPrefs.GetString("datasave"));
    }

    public void Load()
    {
        if (PlayerPrefs.GetString("datasave") != "")
        {
            string data = PlayerPrefs.GetString("datasave");
            lastDataSave = JsonUtility.FromJson<SaveDataList>(data);

            if (lastDataSave.saveList.Count > 0)
            {
                for (int i = 0; i < lastDataSave.saveList.Count; i++)
                {
                    saveListData.Add(lastDataSave.saveList[i]);

                    GameObject temp = Instantiate(dataContainer.GetChild(saveListData[i].id - 1).gameObject, parent);
                    temp.transform.localPosition = saveListData[i].pos;
                    temp.transform.GetComponent<BoxCollider2D>().enabled = false;
                    temp.transform.GetComponent<DragAndDropDuplicate>().enabled = true;
                }
            }
        }
    }

    void Update()
    {
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                duplicateGo = Instantiate(hit.transform.gameObject, parent);
                duplicateGo.transform.localPosition = Vector3.zero;
                duplicateGo.transform.GetComponent<BoxCollider2D>().enabled = false;
                duplicateGo.transform.GetComponent<DragAndDropDuplicate>().enabled = true;
                Debug.Log(hit.transform.name);
            }
        }
    }
}


[System.Serializable]
public class SaveData
{
    public int id;
    public Vector3 pos;
}

[System.Serializable]
public class SaveDataList
{
    public List<SaveData> saveList = new List<SaveData>();
}

