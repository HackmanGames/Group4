using UnityEngine;
using UnityEngine.UI;

public class Utils
{
    public static void AddMask(BasePanel panel)
    {
        GameObject obj = new GameObject("mask");
        Image image = obj.AddComponent<Image>();
        image.color = new Color(0f, 0f, 0f, 0.5f);

        image.rectTransform.anchorMin = Vector2.zero;
        image.rectTransform.anchorMax = Vector2.one;
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        image.rectTransform.position = Vector3.zero;

        obj.transform.SetParent(panel.transform, false);
        obj.transform.SetAsFirstSibling();
    }

    public static T GetComponent<T>(GameObject gameObject, string path = null)
    {
        if(path == null) return gameObject.transform.GetComponent<T>();
        return gameObject.transform.Find(path).GetComponent<T>();
    }

    public static GameObject GetGameObject(GameObject gameObject, string path)
    {
        return gameObject.transform.Find(path).gameObject;
    }

    public static void RemoveAllChilds(GameObject gameObject){
        while(gameObject.transform.childCount > 0){
            GameObject obj = gameObject.transform.GetChild(0).gameObject;
            if(obj != null){
                GameObject.DestroyImmediate(obj);
            }
            else { break; }
        }
    }
}