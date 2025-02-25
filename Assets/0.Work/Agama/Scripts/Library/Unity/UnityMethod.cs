using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Library
{
    public static class LidraryUnityMethod
    {
        /// <summary>
        /// Transform에서 <typeparamref name="T"/> 타입의 컴포넌트를 가져오거나 없으면 추가합니다.
        /// </summary>
        /// <typeparam name="T">가져오거나 추가할 컴포넌트 타입입니다.</typeparam>
        /// <param name="transform">컴포넌트를 가져오거나 추가할 Transform입니다.</param>
        /// <returns>찾은 또는 새로 추가된 <typeparamref name="T"/> 타입의 컴포넌트입니다.</returns>
        public static T TryGetOrAddComponent<T>(this Component transform)
            where T : Component
            => transform.TryGetComponent(out T comp) ? comp : transform.AddComponent<T>();

        /// <summary>
        /// Transform에서 <typeparamref name="T"/> 타입의 컴포넌트를 가져오거나 없으면 <typeparamref name="A"/> 타입의 컴포넌트를 추가합니다.
        /// </summary>
        /// <typeparam name="T">가져오거나 추가할 컴포넌트 타입입니다.</typeparam>
        /// <typeparam name="A"><typeparamref name="T"/>의 서브클래스 타입입니다.</typeparam>
        /// <param name="transform">컴포넌트를 가져오거나 추가할 Transform입니다.</param>
        /// <returns>찾은 <typeparamref name="T"/> 또는 새로 추가된 <typeparamref name="A"/> 타입의 컴포넌트입니다.</returns>

        public static T TryGetOrAddComponent<T, S>(this Component transform)
            where S : Component, T
            => transform.TryGetComponent(out T comp) ? comp : transform.AddComponent<S>();

        /// <summary>
        /// Transform에 지정된 컴포넌트를 추가하거나 가져옵니다.
        /// </summary>
        /// <typeparam name="T">추가하거나 가져올 컴포넌트의 타입</typeparam>
        /// <param name="func">추가된 컴포넌트에 대한 설정을 수행하는 함수</param>
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
        new Vector2(0, 1), new Vector2(0, -1), // 상하
        new Vector2(-1, 0), new Vector2(1, 0), // 좌우
        new Vector2(1, 1), new Vector2(-1, 1), // 북동, 북서
        new Vector2(1, -1), new Vector2(-1, -1) // 남동, 남서
};
        /// <summary>
        /// 주어진 벡터에서 목표 벡터까지의 방향을 8방향(상하좌우, 대각선) 중 가장 근접한 방향으로 변환
        /// </summary>
        /// <returns>8방향(상하좌우, 대각선) 중 가장 근접한 방향 벡터</returns>
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