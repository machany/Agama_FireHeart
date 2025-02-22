using Agama.Scripts.Entities;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetComponentInEntity", story: "set [Component] to [DefaultTypePath] . [IEntityComponentName] in [Entity] [IsDerived]", category: "Action", id: "bbb746c3fd31ed53cbe5c98b18a53ea6")]
    public partial class SetComponentInEntity : Action
    {
        [SerializeReference] public BlackboardVariable<MonoBehaviour> Component;
        [SerializeReference] public BlackboardVariable<string> DefaultTypePath;
        [SerializeReference] public BlackboardVariable<string> IEntityComponentName;
        [SerializeReference] public BlackboardVariable<Entity> Entity;
        [SerializeReference] public BlackboardVariable<bool> IsDerived;
        protected override Status OnStart()
        {
            string typePath = string.IsNullOrEmpty(DefaultTypePath.Value) ? IEntityComponentName.Value : DefaultTypePath.Value + "." + IEntityComponentName.Value;
            Type targetType = Type.GetType(typePath);

            if (!typeof(IEntityComponent).IsAssignableFrom(targetType))
                throw new InvalidCastException();

            Type genericType = typeof(SetIEntityComponentClass<>);
            Type specificType = genericType.MakeGenericType(targetType);

            if (Entity.Value == null)
                Debug.Log("Entity is null Action");
            Component.Value = specificType.GetMethod("SetComponent").Invoke(null, new object[] { Entity.Value, IsDerived.Value }) as MonoBehaviour;

            if (Component.Value != null)
            {
                Debug.Log(Component.Value.GetType());
                Debug.Log("Success");
            }
            
            return Status.Success;
        }
    }

    sealed internal class SetIEntityComponentClass<T> where T : IEntityComponent
    {
        public static T SetComponent(Entity entity, bool isDerived)
        {
            if (entity == null)
                Debug.Log("Entity is null");
            return entity.GetComp<T>(isDerived);
        }
    }
}