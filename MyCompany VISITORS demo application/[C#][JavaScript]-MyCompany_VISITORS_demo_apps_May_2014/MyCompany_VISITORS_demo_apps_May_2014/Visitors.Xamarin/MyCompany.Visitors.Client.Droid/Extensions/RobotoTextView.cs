/*
* Copyright (C) 2013 @JamesMontemagno http://www.montemagno.com http://www.refractored.com
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;

using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using MyCompany.Visitors.Client.Droid;

namespace com.refractored.controls
{
    public class RobotoTextView : TextView
    {
        private const int RobotoThin = 0;
        private const int RobotoThinItalic = 1;
        private const int RobotoLight = 2;
        private const int RobotoLightItalic = 3;
        private const int RobotoRegular = 4;
        private const int RobotoItalic = 5;
        private const int RobotoMedium = 6;
        private const int RobotoMediumItalic = 7;
        private const int RobotoBold = 8;
        private const int RobotoBoldItalic = 9;
        private const int RobotoBlack = 10;
        private const int RobotoBlackItalic = 11;
        private const int RobotoCondensed = 12;
        private const int RobotoCondensedItalic = 13;
        private const int RobotoCondensedBold = 14;
        private const int RobotoCondensedBoldItalic = 15;

        private TypefaceStyle m_Style = TypefaceStyle.Normal;

        private static readonly Dictionary<int, Typeface> Typefaces = new Dictionary<int, Typeface>(16);

        public RobotoTextView(Context context) :
            base(context)
        {
        }

        protected RobotoTextView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }


        public RobotoTextView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            this.Initialize(context, attrs);
        }

        public RobotoTextView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            this.Initialize(context, attrs);
        }

        private void Initialize(Context context, IAttributeSet attrs)
        {


            try
            {
                TypedArray values = context.ObtainStyledAttributes(attrs, Resource.Styleable.RobotoTextView);

                int typefaceValue = values.GetInt(Resource.Styleable.RobotoTextView_typeface, 4);
                values.Recycle();
                var font = this.ObtainTypeface(context, typefaceValue);
                this.SetTypeface(font, this.m_Style);
            }
            catch (Exception)
            {

            }

        }

        private Typeface ObtainTypeface(Context context, int typefaceValue)
        {
            try
            {

                Typeface typeface = null;
                if (Typefaces.ContainsKey(typefaceValue))
                    typeface = Typefaces[typefaceValue];

                if (typeface == null)
                {
                    typeface = this.CreateTypeface(context, typefaceValue);
                    Typefaces.Add(typefaceValue, typeface);
                }
                return typeface;
            }
            catch (Exception)
            {

            }

            return null;
        }
        private Typeface CreateTypeface(Context context, int typefaceValue)
        {
            try
            {

                Typeface typeface;
                switch (typefaceValue)
                {
                    case RobotoThin:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-Thin.ttf");
                        break;
                    case RobotoThinItalic:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-ThinItalic.ttf");
                        m_Style = TypefaceStyle.Italic;
                        break;
                    case RobotoLight:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-Light.ttf");
                        break;
                    case RobotoLightItalic:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-LightItalic.ttf");
                        m_Style = TypefaceStyle.Italic;
                        break;
                    case RobotoRegular:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-Regular.ttf");
                        break;
                    case RobotoItalic:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-Italic.ttf");
                        m_Style = TypefaceStyle.Italic;
                        break;
                    case RobotoMedium:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-Medium.ttf");
                        break;
                    case RobotoMediumItalic:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-MediumItalic.ttf");
                        m_Style = TypefaceStyle.Italic;
                        break;
                    case RobotoBold:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-Bold.ttf");
                        m_Style = TypefaceStyle.Bold;
                        break;
                    case RobotoBoldItalic:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-BoldItalic.ttf");
                        m_Style = TypefaceStyle.BoldItalic;
                        break;
                    case RobotoBlack:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-Black.ttf");
                        break;
                    case RobotoBlackItalic:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-BlackItalic.ttf");
                        m_Style = TypefaceStyle.Italic;
                        break;
                    case RobotoCondensed:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-Condensed.ttf");
                        break;
                    case RobotoCondensedItalic:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-CondensedItalic.ttf");
                        m_Style = TypefaceStyle.Italic;
                        break;
                    case RobotoCondensedBold:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-BoldCondensed.ttf");
                        m_Style = TypefaceStyle.Bold;
                        break;
                    case RobotoCondensedBoldItalic:
                        typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Roboto-BoldCondensedItalic.ttf");
                        m_Style = TypefaceStyle.BoldItalic;
                        break;
                    default:
                        throw new ArgumentException("Unknown typeface attribute value " + typefaceValue);
                }
                return typeface;

            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}