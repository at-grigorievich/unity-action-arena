using UnityEngine;
using UnityEngine.UI;

namespace ATG.UI
{
    public class FillRoundedProgressBar: CanvasGroupElement<float>
    {
        [SerializeField] private Image fillRateSrc;

        protected override void Awake()
        {
            base.Awake();
            fillRateSrc.type = Image.Type.Filled;
            fillRateSrc.fillAmount = 0f;
        }

        public override void Show(object sender, float rate)
        {
            base.Show(sender, rate);
            rate = Mathf.Clamp01(rate);
            
            fillRateSrc.fillAmount = rate;
        }
    }
}