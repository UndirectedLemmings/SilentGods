using UnityEditor;

[CustomEditor(typeof(PlayerAbility))]
public class PAE : Editor
{
    public override void OnInspectorGUI()
    {
        var script = target as PlayerAbility;
        EditorGUILayout.LabelField("Current energy", script.currentEnergy.ToString());
        script.maxEnergy = EditorGUILayout.IntField("Max energy", script.maxEnergy);
        script.IncomePerSecond = EditorGUILayout.IntField("Income per second", script.IncomePerSecond);
        script.AbilityCost = EditorGUILayout.IntField("Ability cost", script.AbilityCost);
        script.AbilityType = (AType)EditorGUILayout.EnumPopup("Ability Type", script.AbilityType);

        switch (script.AbilityType)
        {
            case AType.Heal:
                script.AmountHealed = EditorGUILayout.IntField("Amount healed", script.AmountHealed);
                script.HealRadius = EditorGUILayout.FloatField("Heal radius", script.HealRadius);
                break;
            case AType.Hide:
                script.HideTime = EditorGUILayout.FloatField("Hide time", script.HideTime);
                break;
            case AType.Rage:
                script.RageTime = EditorGUILayout.FloatField("Rage time", script.RageTime);
                script.AttackSpeedIncrease = EditorGUILayout.FloatField("Attack speed increase", script.AttackSpeedIncrease);
                break;
        }



    }

}
