using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGenerator : MonoBehaviour
{
    GameObject osef;
    public GameObject startRoom;
    public List<GameObject> availableRooms;
    public List<GameObject> spawnedRooms = new List<GameObject>();
    public List<GameObject> allRooms = new List<GameObject>();
    public int maxLevel = 1;
    public int curLevel = 0;
    public bool coroutineStop = true;

    // Start is called before the first frame update
    void Start()
    {
        GameObject room1 = Instantiate(startRoom);
        room1.transform.position = new Vector3(0, 0, 0);
        var link1 = room1.transform.Find("Link0").position;
        allRooms.Add(room1);
        room1.tag = "Placed";
    }

    // Update is called once per frame
    void Update()
    {
        if (curLevel < maxLevel)
        {
            if(coroutineStop)
            {
                StartCoroutine(spawnAllRooms(null, maxLevel - 1));
                curLevel++;
            }
        }
    }

    IEnumerator spawnAllRooms(GameObject parentRoom, int lvl)
    {
        coroutineStop = false;
        List<GameObject> spawnedRooms = new List<GameObject>();
        allRooms.RemoveAll(t => t == null);
        foreach (var room in allRooms)
        {
            var linksParent = getLinks(room);
            foreach (var linkP in linksParent)
            {
                foreach (var aRoom in availableRooms)
                {
                    spawnedRooms.AddRange(spawnRoom4Orientation(aRoom, room, linkP, lvl));
                }
                yield return null;
                spawnedRooms.RemoveAll(t => t == null);
                if (spawnedRooms.Count > 0)
                {
                    int randIndex = Random.Range(0, spawnedRooms.Count);
                    spawnedRooms[randIndex].tag = "Placed";
                }
            }
        }
        spawnedRooms.RemoveAll(t => t == null);
        allRooms.Clear();
        allRooms = spawnedRooms;
        coroutineStop = true;
        yield return null;
    }

    List<GameObject> spawnRoom4Orientation(GameObject room2spawn, GameObject parentRoom, 
        Vector3 linkP, int lvl)
    {
        List<GameObject> spawnedRooms = new List<GameObject>();
        int nbLinkRoom = room2spawn.GetComponent<Room>().linkCount;
        for (int i = 0; i < 4; i++)
        {
            for(int l = 0; l < nbLinkRoom; l++)
            {
                GameObject room2 = Instantiate(room2spawn);
                room2.transform.position = Vector3.zero;
                room2.GetComponent<Room>().parent = parentRoom; 
                room2.GetComponent<Room>().level = maxLevel-lvl;
                room2.transform.Rotate(new Vector3(0, i * 90, 0));
                var link2 = room2.transform.Find("Link"+l).position;
                room2.transform.position = linkP - link2;
                spawnedRooms.Add(room2);
            }
        }      
        return spawnedRooms;
    }

    List<Vector3> getLinks(GameObject room)
    {
        List<Vector3> links = new List<Vector3>();
        int linkCount = room.GetComponent<Room>().linkCount;
        for (int i = 0; i < linkCount; i++)
        {
            var transform = room.transform.Find("Link" + i);
            if (transform != null)
                links.Add(transform.position);
        }
        return links;
    }
}
