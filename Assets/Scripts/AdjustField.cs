using Unity.AI.Navigation;
using UnityEngine;

public class AdjustField : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject wW;
    public GameObject eW;
    public GameObject nW;
    public GameObject sW;

    public SpawnPickup spawn;
    public MainMenuController adjuster;
    private int winCount = 0;

    private NavMeshSurface surface;

    void incrScale(GameObject w)
    {
        w.transform.localScale = new Vector3(w.transform.localScale.x, w.transform.localScale.y, w.transform.localScale.z + 20.0f * winCount);
    }

    void Awake()
    {
        winCount = PlayerController.winCount;
        adjuster.Call();
        if(winCount == 0) { return; }

        transform.localScale += new Vector3(2.0f, 2.0f, 2.0f) * winCount;
        spawn.spawnCountPickup += 10 * winCount;
        spawn.spawnCountWall *= 4 * winCount;
        spawn.xMin -= 10.0f * winCount ; spawn.xMax += 10.0f * winCount;
        spawn.zMin -= 10.0f * winCount ; spawn.zMax += 10.0f * winCount;

        wW.transform.position = new Vector3(wW.transform.position.x - 10.0f * winCount, wW.transform.position.y, wW.transform.position.z);
        eW.transform.position = new Vector3(eW.transform.position.x + 10.0f * winCount, eW.transform.position.y, eW.transform.position.z);
        nW.transform.position = new Vector3(nW.transform.position.x, nW.transform.position.y, nW.transform.position.z + 10.0f * winCount);
        sW.transform.position = new Vector3(sW.transform.position.x, sW.transform.position.y, sW.transform.position.z - 10.0f * winCount);

        incrScale(wW);
        incrScale(eW);
        incrScale(nW);
        incrScale(sW);
    }

    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }
}
