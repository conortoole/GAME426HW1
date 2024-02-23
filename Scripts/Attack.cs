using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Highlights;

public class Attack : MonoBehaviour
{
    [SerializeField]
    public Color color = Color.red;
    public float highlightDuration = 1.0f;
    private Renderer lastTileRenderer;
    public GameObject floorTile;
    public List<GameObject> neighbors;
    public float RechargeTime = 5f;
    public int AreaEffectDist = 2;
    RaycastHit hit;
    RaycastHit[] hits;
    Renderer hitRenderer;

    // Start is called before the first frame update
    void Start()
    {
        floorTile = getCurrentTile();
    }

    // Update is called once per frame
    void Update()
    {
        floorTile = getCurrentTile();
        if (floorTile != null){
            neighbors = Dijkstra(floorTile, AreaEffectDist);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Space))
        {
            neighbors.Add(floorTile);
            highlightTiles(neighbors);
        }
    }

    List<GameObject> Dijkstra(GameObject floorTile, int AreaEffectDist){
        //this is where dikjstra goes
        List<GameObject> result = new List<GameObject>();
        GameObject N = getN(floorTile);
        GameObject E = getE(floorTile);
        GameObject S = getS(floorTile);
        GameObject W = getW(floorTile);
            if(N != null && N.name == "Cube_Floor"){
                result.Add(N); 
            }
            if(E != null && E.name == "Cube_Floor"){
                result.Add(E); 
            }
            if(S != null && S.name == "Cube_Floor"){
                result.Add(S); 
            }
            if(W != null && W.name == "Cube_Floor"){
                result.Add(W); 
            }
        for(int i = 1; i < AreaEffectDist; i++){
            if (N != null){
                N = getN(N);
            }
            if (E != null){
                E = getE(E);
            }
            if (S != null){
                S = getS(S);
            }
            if (W != null){
                W = getW(W);
            }
            if(N != null && N.name == "Cube_Floor"){
                result.Add(N); 
            }
            if(E != null && E.name == "Cube_Floor"){
                result.Add(E); 
            }
            if(S != null && S.name == "Cube_Floor"){
                result.Add(S); 
            }
            if(W != null && W.name == "Cube_Floor"){
                result.Add(W); 
            }
        }
        return result;
    }

    GameObject getN(GameObject floorTile){
        //takes a floor piece and casts rays to get all other floor pieces
        Ray N = new Ray(floorTile.transform.position, floorTile.transform.forward);
        if (Physics.Raycast(N, out hit, Mathf.Infinity)){
            if (hit.transform != null){
                return hit.transform.gameObject;
            }
        }
        return null;
    }

    GameObject getE(GameObject floorTile){
        //takes a floor piece and casts rays to get all other floor pieces
        Ray E = new Ray(floorTile.transform.position, floorTile.transform.right);
        if (Physics.Raycast(E, out hit, Mathf.Infinity)){
            if (hit.transform != null){
                return hit.transform.gameObject;
            }
        }
        return null;
    }

    GameObject getS(GameObject floorTile){
        //takes a floor piece and casts rays to get all other floor pieces
        Ray S = new Ray(floorTile.transform.position, -floorTile.transform.forward);
        if (Physics.Raycast(S, out hit, Mathf.Infinity)){
            if (hit.transform != null){
                return hit.transform.gameObject;
            }
        }
        return null;
    }

    GameObject getW(GameObject floorTile){
        //takes a floor piece and casts rays to get all other floor pieces
        Ray W = new Ray(floorTile.transform.position, -floorTile.transform.right);
        if (Physics.Raycast(W, out hit, Mathf.Infinity)){
            if (hit.transform != null){
                return hit.transform.gameObject;
            }
        }
        return null;
    }

    GameObject getCurrentTile(){
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
            if (hit.transform != null){
                return hit.transform.gameObject;
            }
        }
        return null;
    }

    void highlightTiles(List<GameObject> tiles){
        foreach (var tile in tiles){
            if (tile != null){
                Renderer hitRenderer = tile.GetComponent<Renderer>();
                Collider cast = tile.GetComponent<Collider>();
                //this part kills the enemies
                if (cast != null){
                    hits = Physics.BoxCastAll(cast.bounds.center, transform.localScale, transform.up, transform.rotation, Mathf.Infinity);
                    if (hits != null){
                        foreach (RaycastHit hit in hits){
                            Debug.Log(hit);
                            if (hit.transform != null){
                                if (hit.transform.gameObject.tag == "turtle" || hit.transform.gameObject.tag == "slime"){
                                    hit.transform.gameObject.SetActive(false);
                                }
                            }
                        }
                    }
                    /////
                    if (hitRenderer != null){
                        gameObject.GetComponent<Highlight>().HighlightObject(hitRenderer, true);
                        StartCoroutine(RemoveHighlightAfterDelay(hitRenderer));
                    }
                }
            }
        }
    }

    IEnumerator RemoveHighlightAfterDelay(Renderer renderer)
    {
        yield return new WaitForSeconds(highlightDuration);

        // Remove the highlight
        gameObject.GetComponent<Highlight>().HighlightObject(renderer, false);
    }
}
