using Agama.Scripts.Animators;
using UnityEngine;

namespace Agama.Scripts.Entities.FSM
{
    [CreateAssetMenu(fileName = "StateSO", menuName = "SO/FSM/State", order = 0)]
    public class EntityStateSO : ScriptableObject
    {
        [Tooltip(" <color=red>명명 규칙</color> : 반드시 스테이트 이름은 영문 소문자로 입력하며, 띄어쓰기의 기준은 다른 단어를 사용할 때를 기준으로 언더바(_)를 사용한다.\n" +
            " <color=red>규격</color> : 해당 스테이트를 사용하는 (entity의 이름, <color=green>첫 알파벳만 대문자</color>)이 가장 먼저 앞으로 와야하며, 마지막에 (State_스테이트의 타입)을 붙인다. (event, default등) 단, default는 생략한다. <color=green>[예 : <color=red>P</color>layer_use_item_<color=red>S</color>tate_event]</color>")]
        public string stateName;
        [Tooltip(" <color=red>명명 규칙</color> : (네임스페이스위치.클래스네임)이다.")]
        public string className;
        public AnimationParamiterSO animParam;
    }
}
