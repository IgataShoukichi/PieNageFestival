using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChildCount
{

    // parent直下の子オブジェクトを再帰的に取得する
    public static Transform[] GetChildrenRecursive(this Transform parent)
    {
        // 親を含む子オブジェクトを再帰的に取得
        var parentAndChildren = parent.GetComponentsInChildren<Transform>();
        // 子オブジェクトの格納用配列作成
        var children = new Transform[parentAndChildren.Length - 1];

        // 親を除く子オブジェクトを結果にコピー
        Array.Copy(parentAndChildren, 1, children, 0, children.Length);

        // 子オブジェクトが再帰的に格納された配列
        return children;
    }
}
