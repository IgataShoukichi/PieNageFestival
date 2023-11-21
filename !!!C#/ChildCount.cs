using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChildCount
{

    // parent�����̎q�I�u�W�F�N�g���ċA�I�Ɏ擾����
    public static Transform[] GetChildrenRecursive(this Transform parent)
    {
        // �e���܂ގq�I�u�W�F�N�g���ċA�I�Ɏ擾
        var parentAndChildren = parent.GetComponentsInChildren<Transform>();
        // �q�I�u�W�F�N�g�̊i�[�p�z��쐬
        var children = new Transform[parentAndChildren.Length - 1];

        // �e�������q�I�u�W�F�N�g�����ʂɃR�s�[
        Array.Copy(parentAndChildren, 1, children, 0, children.Length);

        // �q�I�u�W�F�N�g���ċA�I�Ɋi�[���ꂽ�z��
        return children;
    }
}
