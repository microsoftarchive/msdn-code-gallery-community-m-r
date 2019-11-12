// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using BasicMVVMQuickstart_Desktop.Model;
using Microsoft.Practices.Prism.Commands;

namespace BasicMVVMQuickstart_Desktop.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            this.SubmitCommand = new DelegateCommand<object>(this.OnSubmit);
            this.QuestionnaireViewModel = new QuestionnaireViewModel();
            this.ResetCommand = new DelegateCommand(this.OnReset);
        }

        public ICommand SubmitCommand { get; private set; }

        public ICommand ResetCommand { get; private set; }

        public QuestionnaireViewModel QuestionnaireViewModel { get; set; }

        private void OnSubmit(object obj)
        {
            Debug.WriteLine(this.BuildResultString());
        }

        private void OnReset()
        {
            this.QuestionnaireViewModel.Questionnaire = new Questionnaire();
        }

        private string BuildResultString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Name: ");
            builder.Append(this.QuestionnaireViewModel.Questionnaire.Name);
            builder.Append("\nAge: ");
            builder.Append(this.QuestionnaireViewModel.Questionnaire.Age);
            builder.Append("\nQuestion 1: ");
            builder.Append(this.QuestionnaireViewModel.Questionnaire.Quest);
            builder.Append("\nQuestion 2: ");
            builder.Append(this.QuestionnaireViewModel.Questionnaire.FavoriteColor);
            return builder.ToString();
        }
    }
}
