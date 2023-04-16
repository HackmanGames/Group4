using UnityEngine;
using DG.Tweening;

public class TweenUtils {
    public static Tweener ScaleTween(Transform trans, Vector3 end_scale, float duration, TweenCallback complete_func = null){
        Tweener tweener = trans.DOScale(end_scale, duration);
        if(complete_func != null) { tweener.onComplete = complete_func; }
        return tweener;
    }

    public static void FadeTween(Transform trans){
    }

    public static void RotateTween(Transform trans, Vector3 endValue, float duration){
        trans.DORotate(endValue, duration);
    }

    public static Tweener MoveTween(Transform trans, Vector3 endValue, float duration){
        Tweener tweener = trans.DOMove(endValue, duration);
        return tweener;
    }
    
    public static void ShakeTween(Transform trans){
        trans.DOShakeScale(1, 0.3f, 4, 30);
        trans.DOShakeRotation(1, new Vector3(0 , 0, 20), 4, 150);
    }
}