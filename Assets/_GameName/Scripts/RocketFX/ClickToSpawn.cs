using UnityEngine;

public class ClickToSpawn : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _prefab;
    
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) 
            return;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var hit))
            return;


        Instantiate(_prefab, hit.point, Quaternion.identity);
    }
}
