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
        /// GameObject�� ������ ������Ʈ�� �߰��ϰų� �����ɴϴ�.
        /// </summary>
        /// <typeparam name="T">�߰��ϰų� ������ ������Ʈ�� Ÿ��</typeparam>
        /// <param name="func">�߰��� ������Ʈ�� ���� ������ �����ϴ� �Լ�</param>
        /// <returns>���� �ν��Ͻ�</returns>
        public GameObjectBuilder SetComponent<T>(Action<T> func) where T : Component
        {
            func(targetGameObject.transform.TryGetOrAddComponent<T>());
            return this;
        }

        #region Presets

        /// <summary>
        /// ����� �������� Ű ����� ��ȯ�մϴ�.
        /// </summary>
        /// <returns>������ Ű ���</returns>
        public IEnumerable<string> GetPresetKeys()
        {
            return presets.Keys;
        }

        /// <summary>
        /// �⺻ Ű�� ����Ͽ� �������� �����մϴ�.
        /// </summary>
        /// <param name="preset">������ ������</param>
        public void SavePreset(Action<GameObjectBuilder> preset)
        {
            presets[presetDefaultKey] = preset;
        }

        /// <summary>
        /// ������ Ű�� ����Ͽ� �������� �����մϴ�.
        /// </summary>
        /// <param name="key">�������� ������ Ű</param>
        /// <param name="preset">������ ������</param>
        public void SavePreset(string key, Action<GameObjectBuilder> preset)
        {
            presets[key] = preset;
        }

        /// <summary>
        /// ������ Ű�� �������� �����մϴ�.
        /// </summary>
        /// <param name="key">������ �������� Ű</param>
        /// <exception cref="KeyNotFoundException">Ű�� �������� ���� ��� �߻�</exception>
        public void RemovePreset(string key)
        {
            if (!presets.Remove(key))
                throw new KeyNotFoundException($"Preset can't found key '{key}'.");
        }

        /// <summary>
        /// ������ Ű�� �ش��ϴ� �������� �����ϴ��� Ȯ���մϴ�.
        /// </summary>
        /// <param name="key">Ȯ���� �������� Ű</param>
        /// <returns>������ ���� ����</returns>
        public bool HasPreset(string key)
        {
            return presets.ContainsKey(key);
        }

        /// <summary>
        /// ������ Ű�� �������� �����մϴ�.
        /// </summary>
        /// <param name="key">������ �������� Ű (�ɼ�)</param>
        /// <returns>���� �ν��Ͻ�</returns>
        /// <exception cref="KeyNotFoundException">�������� �������� ������ �߻�</exception>
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
        /// ������ ����� ������ �����ϰ� Unity �ֿܼ� ����մϴ�.
        /// </summary>
        /// <param name="func">������ ����� ����</param>
        /// <returns>���� �ν��Ͻ�</returns>
        public GameObjectBuilder Debug(Action func)
        {
            func();
            return this;
        }
#endif


        /// <summary>
        /// GameObject�� �����ϰ� ���������� ���� ���¸� �����մϴ�.
        /// </summary>
        /// <param name="reset">true�� ���, ���带 �Ϸ��� �� ���� ���¸� ���ο� GameObject�� �����մϴ�.</param>
        /// <returns>������ GameObject.</returns>
        public GameObject Build(bool reset = true)
        {
            GameObject result = targetGameObject;
            if (reset)
                targetGameObject = new GameObject();
            return result;
        }
    }
}