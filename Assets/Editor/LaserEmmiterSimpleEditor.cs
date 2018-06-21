using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LaserEmitterSimple))]
public class LaserEmmiterSimpleEditor : Editor {

    public override void OnInspectorGUI()
    {
        var script = target as LaserEmitterSimple;

        script.projectile = (Laser)EditorGUILayout.ObjectField("Projectile", script.projectile, typeof(Laser), false);
        script.maxLength = EditorGUILayout.FloatField("Max Length", script.maxLength);
        script.fireInterval = EditorGUILayout.FloatField("Fire interval", script.fireInterval);
        //script.BulletVelocity = EditorGUILayout.FloatField("Bullet Velocity", script.BulletVelocity);
        script.bulletLifetime = EditorGUILayout.FloatField("Bullet lifeteime", script.bulletLifetime);
        script.gun = (Transform)EditorGUILayout.ObjectField("Gun", script.gun, typeof(Transform), true);
        EditorGUILayout.PrefixLabel("Damage");
        script.damage.min = EditorGUILayout.IntField("Min", script.damage.min);
        script.damage.max = EditorGUILayout.IntField("Max", script.damage.max);
    }

}
