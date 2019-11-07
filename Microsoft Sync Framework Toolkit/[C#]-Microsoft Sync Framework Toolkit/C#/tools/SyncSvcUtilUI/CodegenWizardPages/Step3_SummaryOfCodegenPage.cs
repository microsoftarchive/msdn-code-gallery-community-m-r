// Copyright 2010 Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License"); 
// You may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, 
// INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
// CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, 
// MERCHANTABLITY OR NON-INFRINGEMENT. 

// See the Apache 2 License for the specific language governing 
// permissions and limitations under the License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using Microsoft.Synchronization.ClientServices.Configuration;
using System.Diagnostics;

namespace SyncSvcUtilUI.CodegenWizardPages
{
    public partial class Step3_SummaryOfCodegenPage : UserControl, IWizardPage
    {
        public Step3_SummaryOfCodegenPage()
        {
            InitializeComponent();
        }

        #region IWizardPage Members

        public void OnFocus()
        {
            string sourceSpecificParams = null;
            string commonParams = null;

            if (WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.SELECTED_CODEGEN_SOURCE] ==
                WizardHelper.CONFIG_FILE_CODEGEN_SOURCE)
            {
                // Display the contents of the current SyncScope for review
                this.displayBox.Text = "Running SyncSvcUtil command...\n";

                sourceSpecificParams = string.Format(WizardHelper.CONFIG_CODEGEN_PARAM_FORMAT,
                                  WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.CONFIG_FILE_NAME],
                                  WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.SELECTED_CONFIG_NAME],
                                  WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.SELECTED_DB_NAME]);

            }
            else
            {
                sourceSpecificParams = string.Format(WizardHelper.CSDL_CODEGEN_PARAM_FORMAT,
                                  WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.CSDL_CODEGEN_URL],
                                  WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.SELECTED_CONFIG_NAME]);
            }
            commonParams = string.Format(WizardHelper.CODEGEN_COMMON_PARAMS_FORMAT,
                              WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.CODEGEN_LANGUAGE],
                              WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.CODEGEN_NAMESPACE],                              
                              WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.CODEGEN_TARGET],
                              WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.CODEGEN_OUTDIRECTORY]);

            if (!string.IsNullOrEmpty(WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.CODEGEN_OUTPREFIX]))
            {
                commonParams = string.Format(WizardHelper.CODEGEN_OUTPREFIX_PARAM_FORMAT,
                    commonParams,
                    WizardHelper.Instance.CodeGenWizardHelper[WizardHelper.CODEGEN_OUTPREFIX]);
            }

            this.displayBox.Text += WizardHelper.ExecuteProcessAndReturnLog(sourceSpecificParams + commonParams);

        }

        public bool OnMovingNext()
        {
            return false;
            // no-op
        }

        public void OnFinish()
        {
            // no-op
        }

        #endregion
    }
}
