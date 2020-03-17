using CoreGraphics;
using Foundation;
using System;
using App1;
using App1.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyLabel), typeof(MyLabelRenderer))]
namespace App1.iOS
{
    public class MyLabelRenderer : ViewRenderer<MyLabel, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MyLabel> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var uiLabel = new UILabel
                    {
                        UserInteractionEnabled = true,
                        LineBreakMode = UILineBreakMode.WordWrap,
                        Lines = 0,
                        Text = "عندما يريد العالم أن ‪يتكلّم ‬ ، فهو يتحدّث بلغة يونيكود. تسجّل الآن لحضور المؤتمر الدولي العاشر ليونيكود (Unicode Conference)، الذي سيعقد في 10-12 آذار 1997 بمدينة مَايِنْتْس، ألمانيا. و سيجمع المؤتمر بين خبراء من كافة قطاعات الصناعة على الشبكة العالمية انترنيت ويونيكود، حيث ستتم، على الصعيدين الدولي والمحلي على حد سواء مناقشة سبل استخدام يونكود في النظم القائمة وفيما يخص التطبيقات الحاسوبية، الخطوط، تصميم النصوص والحوسبة متعددة اللغات...TapMe"
                    };

                    var uiTapGestureRecognizer = new UITapGestureRecognizer();
                    uiTapGestureRecognizer.AddTarget(() =>
                    {
                        var label = uiLabel;
                        var recognizer = uiTapGestureRecognizer;
                        var range = new NSRange(uiLabel.Text.Length - "TapMe".Length, "TapMe".Length);

                        using (var ts = new NSTextStorage())
                        {
                            var lm = new NSLayoutManager();
                            var tc = new NSTextContainer(new CGSize(label.Frame.Width, double.MaxValue));

                            lm.AddTextContainer(tc);
                            ts.Append(label.AttributedText);
                            ts.AddLayoutManager(lm);

                            tc.LineFragmentPadding = (float) 0.0;
                            tc.LineBreakMode = label.LineBreakMode;
                            tc.MaximumNumberOfLines = (uint) label.Lines;
                            tc.Size = label.Bounds.Size;

                            var index = lm.GetCharacterIndex(recognizer.LocationOfTouch(0, label), tc);
                            var isWithinRange = (nint) index >= range.Location && (nint) index < range.Location + range.Length;

                            App.Current.MainPage.DisplayAlert("Alert", "Character count: " + label.Text.Length + "\nRange location: " + range.Location + "\nRange length:" + range.Length + "\nCharacter index: " + index +  "\nIs within range: " + isWithinRange, "Dismiss");
                        }
                    });
                    uiLabel.AddGestureRecognizer(uiTapGestureRecognizer);

                    SetNativeControl(uiLabel);
                }
            }
        }
    }
}