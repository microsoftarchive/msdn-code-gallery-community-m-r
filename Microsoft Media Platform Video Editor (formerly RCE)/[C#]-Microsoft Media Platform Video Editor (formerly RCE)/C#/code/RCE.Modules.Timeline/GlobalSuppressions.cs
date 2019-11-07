// <copyright file="GlobalSuppressions.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: GlobalSuppressions.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project. 
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc. 
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File". 
// You do not need to add suppressions to this file manually. 

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "RCE.Modules.Timeline.TimelinePresenter.#DropItem(RCE.Infrastructure.Events.DropPayload)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "RCE.Modules.Timeline.TimelineView.#TimeCodeToPixel(Microsoft.Imm.Sdk.Media.TimeCode)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "RCE.Modules.Timeline.TimelineView.#UserControl_SizeChanged(System.Object,System.Windows.SizeChangedEventArgs)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "RCE.Modules.Timeline.Controls")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "RCE.Modules.Timeline.TimelineElementView.#InLink_Click(System.Object,System.Windows.RoutedEventArgs)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "RCE.Modules.Timeline.TimelineElementView.#OutLink_Click(System.Object,System.Windows.RoutedEventArgs)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "RCE.Modules.Timeline.TimelineElementView.#OnLinkClicked(RCE.Modules.Timeline.LinkPosition)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Un", Scope = "member", Target = "RCE.Modules.Timeline.Commands.UndoableCommand.#UnExecuteCommand()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Un", Scope = "member", Target = "RCE.Modules.Timeline.Commands.IUndoableCommand.#UnExecute()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RCE.Modules.Timeline.AudioPreview.#Link")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "type", Target = "RCE.Modules.Timeline.TimelineView")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "RCE.Modules.Timeline.TimelineView.#LockTimeline_Click(System.Object,System.Windows.RoutedEventArgs)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Scope = "member", Target = "RCE.Modules.Timeline.TimelineView.#RootVisual_KeyDown(System.Object,System.Windows.Input.KeyEventArgs)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "RCE.Modules.Timeline.TimelineView.#VolumeControl_VolumeChanged(System.Object,System.EventArgs)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RCE.Modules.Timeline.VideoPreview.#Link")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "Playhead", Scope = "resource", Target = "RCE.Modules.Timeline.Resources.Resources.resources")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member", Target = "RCE.Modules.Timeline.TimelinePresenter.#.ctor(RCE.Modules.Timeline.ITimelineView,Microsoft.Practices.Composite.Events.IEventAggregator,RCE.Infrastructure.Models.ITimelineModel,RCE.Infrastructure.IProjectService,RCE.Modules.Timeline.Commands.ICaretaker,RCE.Infrastructure.IConfigurationService)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "RCE.Modules.Timeline.TimelineElementView.#DeleteButton_Click(System.Object,System.Windows.RoutedEventArgs)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "RCE.Modules.Timeline.TimelineElementView.#OnDeleteClicked(RCE.Infrastructure.Models.TimelineElement)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RCE.Modules.Timeline.TimelineView.#btnFrameBackward")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RCE.Modules.Timeline.TimelineView.#btnFrameForward")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RCE.Modules.Timeline.TimelineView.#btnNextClip")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RCE.Modules.Timeline.TimelineView.#btnPreviousClip")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RCE.Modules.Timeline.TimelineView.#image")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "RCE.Modules.Timeline.TimelineView.#UserControl_Loaded(System.Object,System.Windows.RoutedEventArgs)")]
