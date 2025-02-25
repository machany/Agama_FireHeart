using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Library
{
    public static class LidraryUnityMethod
    {
        /// <summary>
        /// Transform���� <typeparamref name="T"/> Ÿ���� ������Ʈ�� �������ų� ������ �߰��մϴ�.
        /// </summary>
        /// <typeparam name="T">�������ų� �߰��� ������Ʈ Ÿ���Դϴ�.</typeparam>
        /// <param name="transform">������Ʈ�� �������ų� �߰��� Transform�Դϴ�.</param>
        /// <returns>ã�� �Ǵ� ���� �߰��� <typeparamref name="T"/> Ÿ���� ������Ʈ�Դϴ�.</returns>
        public static T TryGetOrAddComponent<T>(this Component transform)
            where T : Component
            => transform.TryGetComponent(out T comp) ? comp : transform.AddComponent<T>();

        /// <summary>
        /// Transform���� <typeparamref name="T"/> Ÿ���� ������Ʈ�� �������ų� ������ <typeparamref name="A"/> Ÿ���� ������Ʈ�� �߰��մϴ�.
        /// </summary>
        /// <typeparam name="T">�������ų� �߰��� ������Ʈ Ÿ���Դϴ�.</typeparam>
        /// <typeparam name="A"><typeparamref name="T"/>�� ����Ŭ���� Ÿ���Դϴ�.</typeparam>
        /// <param name="transform">������Ʈ�� �������ų� �߰��� Transform�Դϴ�.</param>
        /// <returns>ã�� <typeparamref name="T"/> �Ǵ� ���� �߰��� <typeparamref name="A"/> Ÿ���� ������Ʈ�Դϴ�.</returns>

        public static T TryGetOrAddComponent<T, S>(this Component transform)
            where S : Component, T
            => transform.TryGetComponent(out T comp) ? comp : transform.AddComponent<S>();

        /// <summary>
        /// Transform�� ������ ������Ʈ�� �߰��ϰų� �����ɴϴ�.
        /// </summary>
        /// <typeparam name="T">�߰��ϰų� ������ ������Ʈ�� Ÿ��</typeparam>
        /// <param name="func">�߰��� ������Ʈ�� ���� ������ �����ϴ� �Լ�</param>
        public static Transform SetComponent<T>(this Transform transform, Action<T> func) where T : Component
        {
            func?.Invoke(transform.TryGetOrAddComponent<T>());
            return transform;
        }

        public static T SetComponent<T>(this T comp, Action<T> componentSetting)
            where T : Component
        {
            componentSetting?.Invoke(comp);
            return comp;
        }

        public static T SetComponent<T>(this Component transform, Action<T> componentSetting)
            where T : Component
        {
            T comp = transform.TryGetOrAddComponent<T>();
            componentSetting?.Invoke(comp);
            return comp;
        }

        public static T GetOrSetComponent<T>(this Component transform, Action<T> setting)
            where T : Component
        {
            if (!transform.TryGetComponent(out T component))
                setting?.Invoke(component = transform.AddComponent<T>());

            return component;
        }

        private static readonly Vector2[] normalizedDirections = new Vector2[8]
{
        new Vector2(0, 1), new Vector2(0, -1), // ����
        new Vector2(-1, 0), new Vector2(1, 0), // �¿�
        new Vector2(1, 1), new Vector2(-1, 1), // �ϵ�, �ϼ�
        new Vector2(1, -1), new Vector2(-1, -1) // ����, ����
};
        /// <summary>
        /// �־��� ���Ϳ��� ��ǥ ���ͱ����� ������ 8����(�����¿�, �밢��) �� ���� ������ �������� ��ȯ
        /// </summary>
        /// <returns>8����(�����¿�, �밢��) �� ���� ������ ���� ����</returns>
        public static Vector2 GetClosestDirection(this Vector2 owner, Vector2 target)
        {
            Vector2 direction = (target - owner).normalized;

            if (direction == Vector2.zero)
                return Vector2.zero;

            return normalizedDirections
                .OrderBy(dir => Vector2.Distance(direction, dir.normalized))
                .First();
        }
    }
}