using System.Collections.Generic;
using System;

namespace Library
{
    public class SelectorComparer<T> : IComparer<T>
    {
        private readonly Func<T, int> _keySelector; // ���� �� ����
        
        public SelectorComparer(Func<T, int> keySelector) // �Ű����� T�� int������. // ���� �� : new SelectorComparer(x => x.int);
        {
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector)); // null�̸� �������, �ƴϸ� ����
        }

        public int Compare(T x, T y)
        {
            if (x is null || y is null)
                throw new ArgumentNullException("�Էµ� ������ �ϳ��� null�Դϴ�."); // ��������

            return _keySelector(x).CompareTo(_keySelector(y)); // x�� y���� ������ ����, ������ 0, ũ�� ���
        }
    }
}
