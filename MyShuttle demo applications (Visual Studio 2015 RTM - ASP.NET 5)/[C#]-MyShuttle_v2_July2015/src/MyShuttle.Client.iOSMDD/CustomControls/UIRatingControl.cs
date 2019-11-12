using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace MyShuttle.Client.iOS.CustomControls
{
    [Register("UIRatingControl")]
    public class UIRatingControl : UIView
    {
        private const int AmountOfStars = 5;

        private const string StarOffImagePath = "bigratingOFF.png";

        private const string StarOnImagePath = "bigratingON{0}.png";

        private List<UIButton> stars = new List<UIButton>(AmountOfStars);

        private int selectedIndex = 0;

        private bool isUpdating;

        public event EventHandler RatingChanged;
        
        private int rating = 0;

        public double Rating 
        {
            get 
            { 
                return this.rating; 
            }

            set 
            { 
                int rating = (int)Math.Ceiling(value);
                
                this.SetRating(rating); 
            }
        }

        public UIRatingControl()
            : base()
        {
            this.Initialize();
        }

        public UIRatingControl(IntPtr handle)
            : base(handle)
        {
            this.Initialize();
        }

        public UIRatingControl(RectangleF frame)
            : base(frame)
        {
            this.Initialize();
        }

        public void SetRating(int rating)
        {
            this.isUpdating = true;

            try
            {
                if (rating < 0 || rating > AmountOfStars)
                {
                    return;
                }

                // -1 for the 0-index based list
                this.UpdateStars(rating - 1);

                this.rating = rating;
            }
            finally
            {
                this.isUpdating = false;
            }
        }

        private void Initialize()
        {
            this.BackgroundColor = UIColor.Clear;

            var ratingImageWidth = 30;
            var yOffset = this.Frame.Height - ratingImageWidth;

            for (int i = 0; i < AmountOfStars; i++)
            {
                var star = new UIButton(new RectangleF(
                    i * (ratingImageWidth + 3), 
                    yOffset, 
                    ratingImageWidth, 
                    ratingImageWidth));
                star.ClipsToBounds = true;

                star.SetBackgroundImage(
                    UIImage.FromFile(StarOffImagePath), 
                    UIControlState.Normal);

                star.TouchUpInside += this.UpdateRating;

                this.Add(star);
                this.stars.Add(star);
            }
        }

        private void UpdateRating(object sender, EventArgs e)
        {
            if (this.isUpdating)
            {
                return;
            }

            var selectedStar = sender as UIButton;
            this.selectedIndex = this.stars.IndexOf(selectedStar);

            this.SetRating(this.selectedIndex + 1);

            var handler = RatingChanged;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void UpdateStars(int index)
        {
            for (int i = 0; i < AmountOfStars; i++)
            {
                var starAtIndex = this.stars[i];

                var newImagePath = i <= index ?
                    string.Format(StarOnImagePath, i + 1) :
                    StarOffImagePath;

                starAtIndex.SetBackgroundImage(
                    UIImage.FromFile(newImagePath),
                    UIControlState.Normal);
            }
        }
    }
}