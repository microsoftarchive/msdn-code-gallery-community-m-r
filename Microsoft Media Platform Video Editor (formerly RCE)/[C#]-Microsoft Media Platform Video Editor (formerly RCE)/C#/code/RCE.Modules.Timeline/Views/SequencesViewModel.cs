// <copyright file="SequencesViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SequencesViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Views
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class SequencesViewModel : BaseModel, ISequencesViewModel
    {
        private readonly ISequencesView view;

        private readonly IProjectService projectService;

        private readonly ISequenceRegistry sequenceRegistry;

        private ObservableCollection<Sequence> sequences;

        public SequencesViewModel(ISequencesView view, IProjectService projectService, ISequenceRegistry sequenceRegistry)
        {
            this.ChangeCurrentSequenceCommand = new DelegateCommand<object>(this.ChangeCurrentSequence);
            this.projectService = projectService;
            this.sequenceRegistry = sequenceRegistry;
            this.Sequences = this.GetSequencesFromProject();
            this.projectService.ProjectRetrieved += this.RefreshSequences;
            this.view = view;

            this.view.SetViewModel(this);
        }

        public object View
        {
            get
            {
                return this.view;
            }
        }

        public string HeaderInfo
        {
            get
            {
                return "Sequences";
            }
        }

        public ObservableCollection<Sequence> Sequences
        {
            get
            {
                return this.sequences;
            } 
            
            set
            {
                this.sequences = value;
                this.OnPropertyChanged("Sequences");
            }
        }

        public Sequence SelectedSequence { get; set; }

        public ICommand ChangeCurrentSequenceCommand { get; set; }

        public ObservableCollection<Sequence> GetSequencesFromProject()
        {
            if (this.projectService.State != ProjectState.Retrieved || this.projectService.GetCurrentProject() == null)
            {
                return new ObservableCollection<Sequence>();
            }

            return this.projectService.GetCurrentProject().Timelines;
        }

        private void ChangeCurrentSequence(object o)
        {
            this.sequenceRegistry.CurrentSequenceModel = this.sequenceRegistry.GetSequenceForTimeline(this.SelectedSequence);
        }

        private void RefreshSequences(object sender, EventArgs e)
        {
            this.Sequences = this.GetSequencesFromProject();
        }
    }
}
