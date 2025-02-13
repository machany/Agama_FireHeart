using System;
using System.Collections.Generic;
using UnityEngine;

namespace Library
{
    public class GameObjectBuilder
    {
        private GameObject targetGameObject;
        private Dictionary<string, Action<GameObjectBuilder>> presets = new Dictionary<string, Action<GameObjectBuilder>>();
        private readonly string presetDefaultKey;

        public GameObjectBuilder(GameObject obj = null, string presetDefult = "Defult")
        {
            presets = new Dictionary<string, Action<GameObjectBuilder>>();
            targetGameObject = obj ?? new GameObject();
            presetDefaultKey = presetDefult;
        }

        public GameObjectBuilder StartBuild(GameObject obj = null)
        {
            targetGameObject = obj ?? new GameObject();
            return this;
        }

        public GameObjectBuilder SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can't be null or empty.");
            targetGameObject.name = name;
            return this;
        }

        public GameObjectBuilder SetStatic(bool isStatic)
        {
            targetGameObject.isStatic = isStatic;
            return this;
        }

        public GameObjectBuilder SetTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
                throw new ArgumentException("Tag can't be null or empty.");
            targetGameObject.tag = tag;
            return this;
        }

        public GameObjectBuilder SetLayer(int layer)
        {
            targetGameObject.layer = layer;
            return this;
        }

        public GameObjectBuilder SetParent(Transform parent)
        {
            targetGameObject.transform.parent = parent;
            return this;
        }

        public GameObjectBuilder SetPosition(Vector3 position)
        {
            targetGameObject.transform.position = position;
            return this;
        }

        public GameObjectBuilder SetRotation(Vector3 rotation)
        {
            targetGameObject.transform.rotation = Quaternion.Euler(rotation);
            return this;
        }

        public GameObjectBuilder SetScale(float scale)
            => SetScale(Vector3.one * scale);

        public GameObjectBuilder SetScale(Vector3 scale)
        {
            targetGameObject.transform.localScale = scale;
            return this;
        }

        public GameObjectBuilder SetActive(bool active)
        {
            targetGameObject.SetActive(active);
            return this;
        }

        /// <summary>
        /// GameObject에 지정된 컴포넌트를 추가하거나 가져옵니다.
        /// </summary>
        /// <typeparam name="T">추가하거나 가져올 컴포넌트의 타입</typeparam>
        /// <param name="func">추가된 컴포넌트에 대한 설정을 수행하는 함수</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder SetComponent<T>(Action<T> func) where T : Component
        {
            func(targetGameObject.transform.TryGetOrAddComponent<T>());
            return this;
        }

        #region Presets

        /// <summary>
        /// 저장된 프리셋의 키 목록을 반환합니다.
        /// </summary>
        /// <returns>프리셋 키 목록</returns>
        public IEnumerable<string> GetPresetKeys()
        {
            return presets.Keys;
        }

        /// <summary>
        /// 기본 키를 사용하여 프리셋을 저장합니다.
        /// </summary>
        /// <param name="preset">저장할 프리셋</param>
        public void SavePreset(Action<GameObjectBuilder> preset)
        {
            presets[presetDefaultKey] = preset;
        }

        /// <summary>
        /// 지정된 키를 사용하여 프리셋을 저장합니다.
        /// </summary>
        /// <param name="key">프리셋을 저장할 키</param>
        /// <param name="preset">저장할 프리셋</param>
        public void SavePreset(string key, Action<GameObjectBuilder> preset)
        {
            presets[key] = preset;
        }

        /// <summary>
        /// 지정된 키의 프리셋을 삭제합니다.
        /// </summary>
        /// <param name="key">삭제할 프리셋의 키</param>
        /// <exception cref="KeyNotFoundException">키가 존재하지 않을 경우 발생</exception>
        public void RemovePreset(string key)
        {
            if (!presets.Remove(key))
                throw new KeyNotFoundException($"Preset can't found key '{key}'.");
        }

        /// <summary>
        /// 지정된 키에 해당하는 프리셋이 존재하는지 확인합니다.
        /// </summary>
        /// <param name="key">확인할 프리셋의 키</param>
        /// <returns>프리셋 존재 여부</returns>
        public bool HasPreset(string key)
        {
            return presets.ContainsKey(key);
        }

        /// <summary>
        /// 지정된 키의 프리셋을 적용합니다.
        /// </summary>
        /// <param name="key">적용할 프리셋의 키 (옵션)</param>
        /// <returns>빌더 인스턴스</returns>
        /// <exception cref="KeyNotFoundException">프리셋이 존재하지 않으면 발생</exception>
        public GameObjectBuilder ApplyPreset(string key = null)
        {
            key = key ?? presetDefaultKey;

            if (!presets.ContainsKey(key))
                throw new KeyNotFoundException($"Preset can't found key '{key}'.");
            presets[key](this);
            return this;
        }

#if UNITY_EDITOR
        public GameObjectBuilder DebugPresets()
        {
            foreach (var key in presets.Keys)
            {
                UnityEngine.Debug.Log($"Preset: {key}");
            }
            return this;
        }
#endif

        #endregion

#if UNITY_EDITOR
        public GameObjectBuilder Debug(object obj)
        {
            UnityEngine.Debug.Log(obj);
            return this;
        }

        /// <summary>
        /// 지정된 디버그 동작을 실행하고 Unity 콘솔에 출력합니다.
        /// </summary>
        /// <param name="func">실행할 디버그 동작</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder Debug(Action func)
        {
            func();
            return this;
        }
#endif


        /// <summary>
        /// GameObject를 생성하고 선택적으로 빌더 상태를 리셋합니다.
        /// </summary>
        /// <param name="reset">true일 경우, 빌드를 완료한 후 빌더 상태를 새로운 GameObject로 리셋합니다.</param>
        /// <returns>생성된 GameObject.</returns>
        public GameObject Build(bool reset = true)
        {
            GameObject result = targetGameObject;
            if (reset)
                targetGameObject = new GameObject();
            return result;
        }
    }
}