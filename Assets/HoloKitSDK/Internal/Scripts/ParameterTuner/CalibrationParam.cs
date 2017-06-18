using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace HoloKit
{

    [RequireComponent(typeof(Text))]
    public class CalibrationParam : MonoBehaviour
    {

        public Func<float> ValueOnUpdate { get; set; }
        public Action<float> SetValue { get; set; }

        private Text text;

        void Awake()
        {
            text = GetComponent<Text>();
        }

        void Start()
        {
        }

        void Update()
        {
            if (ValueOnUpdate != null)
            {
                float newValue = ValueOnUpdate();
                text.text = string.Format("{0} = {1}", name, newValue);
            }
        }

        private bool isHighlighted;
        public bool IsHighlighted
        {
            get
            {
                return isHighlighted;
            }

            set
            {
                isHighlighted = value;
                text.color = isHighlighted ? new Color(1, 1, 0, 1) : new Color(0.3f, 0.3f, 0.3f, 1);
            }
        }
    }

}