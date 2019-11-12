// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using BasicMVVMQuickstart_Desktop.Model;
using Microsoft.Practices.Prism.Mvvm;

namespace BasicMVVMQuickstart_Desktop.ViewModels
{
    public class QuestionnaireViewModel : BindableBase
    {
        private Questionnaire questionnaire;

        public QuestionnaireViewModel()
        {
            this.Questionnaire = new Questionnaire();
            this.AllColors = new[] { "Red", "Blue", "Green" };
        }

        public Questionnaire Questionnaire
        {
            get { return this.questionnaire; }
            set { SetProperty(ref this.questionnaire, value); }
        }

        public IEnumerable<string> AllColors { get; private set; }

    }
}
