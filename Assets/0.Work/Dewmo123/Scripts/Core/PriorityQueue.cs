using System;
using System.Collections;
using System.Collections.Generic;

namespace Scripts.Core
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        public List<T> _heap = new List<T>();
        public int Count => _heap.Count;

        public T Contains(T item)
        {
            int idx = _heap.IndexOf(item);
            if (idx < 0) return default;
            return _heap[idx];
        }

        public void Push(T newItem)
        {
            _heap.Add(newItem); //가장 마지막에 넣어주고
            int now = _heap.Count - 1; //현재 인덱스 저장
            while (now > 0)
            {
                int next = (now - 1) / 2;
                if (_heap[now].CompareTo(_heap[next]) < 0)
                {
                    break;
                }
                (_heap[now], _heap[next]) = (_heap[next], _heap[now]); //교환

                now = next;
            }
        }

        public T Pop()
        {
            T ret = _heap[0];

            //다시 힙을 구축해야해.
            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex); //마지막 녀석을 맨 앞으로 가져온다.
            lastIndex--;

            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now; //나부터 시작
                                //왼쪽 자식과 비교해서 왼쪽 자식이 더 작다면 now는 왼쪽 자식이 된다.
                if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                    next = left;

                //여기는 위에서 더 작은걸로 판명된 것과 오른쪽 자식이랑 비교한다.
                if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                    next = right;

                //이 작업이 모두 끝나면 next에는 나와 왼쪽 오른쪽 자식중 가장 작은녀석이 들어감.
                if (next == now)
                    break;

                (_heap[now], _heap[next]) = (_heap[next], _heap[now]); //교환

                now = next;
            }

            return ret;
        }

        public T Peek()
        {
            return _heap.Count == 0 ? default : _heap[0];
        }

    }
}