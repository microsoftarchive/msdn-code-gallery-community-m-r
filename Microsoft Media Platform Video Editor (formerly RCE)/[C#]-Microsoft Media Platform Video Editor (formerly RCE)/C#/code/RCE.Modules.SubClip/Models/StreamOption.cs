// <copyright file="StreamOption.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: StreamOption.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.SubClip.Models
{
    using RCE.Infrastructure.Models;

    public class StreamOption : BaseModel
    {
        private bool previewSelected;

        private bool sequenceSelected;

        public bool PreviewSelected
        {
            get
            {
                return this.previewSelected;
            }

            set
            {
                this.previewSelected = value;
                this.OnPropertyChanged("PreviewSelected");
            }
        }

        public bool SequenceSelected
        {
            get
            {
                return this.sequenceSelected;
            }

            set
            {
                this.sequenceSelected = value;
                this.OnPropertyChanged("SequenceSelected");
            }
        }

        public string Name { get; set; }
    }
}
