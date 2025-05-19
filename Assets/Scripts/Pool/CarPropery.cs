using UnityEngine;

namespace Pool
{
    [CreateAssetMenu(fileName = "Car", menuName = "SwipeItems/Car", order = 51)]
    public class CarPropery : ScriptableObject
    {
        [SerializeField] private Color _body;

        public Color GetColor => _body;
    }
}