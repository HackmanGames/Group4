using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageItem : IPoolAble
{
    GameObject gameObject;
    Transform transform;
    private Text txt_content;
    private CanvasGroup canvasGroup;
    
    Sequence show_sequence;
    Sequence hide_sequence;

    public MessageItem(Transform parent)
    {
        UnityEngine.Object prefab = ResourcesManager.Instance.LoadResouces(GameConfig.Instance.ItemPath + "Messages/MessageItem");
        gameObject = GameObject.Instantiate(prefab) as GameObject;
        transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.SetAsLastSibling();

        txt_content = Utils.GetComponent<Text>(gameObject, "txt_content");
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    public void Hide()
    {
        MessageManager.Instance.HideMessage(this);
    }

    public void Show(string content)
    {
        txt_content.text = content;
        transform.localPosition = Vector3.zero;
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0;

        show_sequence = DOTween.Sequence();
        show_sequence.Append(canvasGroup.DOFade(1, 0.3f));
        show_sequence.Insert(1.0f, transform.DOLocalMoveY(295, 2));
        show_sequence.Append(canvasGroup.DOFade(0, 0.3f).OnComplete(() => { Hide(); }));
        show_sequence.Play();
    }

    public void Recycle()
    {
        GameObject.Destroy(gameObject);
    }
}

public class MessageManager : Singleton<MessageManager>
{
    ObjectPool<MessageItem> message_pool;
    Transform root;

    public MessageManager()
    {
        root = GameObject.Find("Message").transform.Find("root");
        message_pool = new ObjectPool<MessageItem>(GetItem);
    }

    private MessageItem GetItem()
    {
        MessageItem item = new MessageItem(root);
        return item;
    }

    public void ShowMessage(string content)
    {
        MessageItem item = message_pool.Pop();
        item.Show(content);
    }

    public void HideMessage(MessageItem item)
    {
        message_pool.Push(item);
    }
}
