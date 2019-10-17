using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateUIGrid : MonoBehaviour
{
    public float hardcodeWidth;
    public float hardcodeHeight;
    
    public Image prefab;
    public float offset;
    public float yOverlap;

    public SinglePlayerGameManager singleManager;
    
    [ContextMenu("CreateUICells")]
    public void Grid4X4()
    {
        for (var i = transform.childCount; i > 0; i--) 
            UnityEditor.EditorApplication.delayCall+=()=>
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            };
        
        
        var rect = prefab.rectTransform;
        //float heightMod = (rect.rect.height + yOverlap) * rect.localScale.y * 3 / 4 + offset / Mathf.Cos(Mathf.PI / 3);
        //float widthMod = rect.rect.width * rect.localScale.x / 2 + offset;
        
        float heightMod = (hardcodeHeight + yOverlap) * rect.localScale.y * 3 / 4 + offset / Mathf.Tan(Mathf.PI / 3);
        float widthMod = hardcodeWidth * rect.localScale.x / 2 + offset;

        for (int y = 3; y >= -3; y--)
            for (int x = -Mathf.Abs(y); x <= Mathf.Abs(y); x += 2)
                Instantiate(prefab, transform.position + new Vector3(x * widthMod, y * heightMod, 0f), Quaternion.identity, transform);
        
        
        // TODO: import generated tiles to StateManager's PrivateGrid and PublicGrid
        // TODO: fix mountain textures
    }
}
