using System;
using System.Resources;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Text;
using System.IO;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Streaming;
//xsl-fo pdf assembly
using Fonet;
using Fonet.Render.Pdf;

namespace PdfWriterPipelineComponent
{
  
    /// <summary>
    /// Implements custom pipeline component to append and/or prepend data to a stream.
    /// </summary>
    /// <remarks>
    /// FixMag class implements pipeline component that can be used in receive and
    /// send BizTalk pipelines. The pipeline component gets a data stream, appends
    /// and/or prepends user specified data to it and outputs modified stream.
    ///</remarks>
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Encoder)]
    [ComponentCategory(CategoryTypes.CATID_Transmitter)]
    [System.Runtime.InteropServices.Guid("88BEC85A-20EE-40ad-BFD0-319B59A0DDBC")]
    public class PdfWriterPipelineComponent:IBaseComponent, Microsoft.BizTalk.Component.Interop.IComponent,
        Microsoft.BizTalk.Component.Interop.IPersistPropertyBag,
        IComponentUI
    {

    private string title = String.Empty;
    private string subject = String.Empty;
    private string password = String.Empty;

        public PdfWriterPipelineComponent()
	{
	}

    [Description("Title of pdf")]
    public string Title
    {
        get {   return title; }
        set {   title = value;}
    }

    [Description("Subject of pdf")]
    public string Subject
    {
        get { return subject; }
        set { subject = value; }
    }

    [Description("Password to protect pdf file")]
    public string Password
    {
        get { return password; }
        set { password = value; }
    }

        #region IBaseComponent

        /// <summary>
        /// Name of the component.
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get {   return "PdfWriter Component";  }
        }
        
        /// <summary>
        /// Version of the component.
        /// </summary>
        [Browsable(false)]
        public string Version
        {
            get {   return "1.0";   }
        }
        
        /// <summary>
        /// Description of the component.
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get {   return "XSL-FO PdfWriter Pipeline Component"; }
        }
    
        #endregion
        
        #region IComponent

        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pc">Pipeline context</param>
        /// <param name="inmsg">Input message.</param>
        /// <returns>Processed input message with appended or prepended data.</returns>
        /// <remarks>
        /// Converts xsl-fo transformed messages to pdf
        /// </remarks>
        public IBaseMessage Execute(IPipelineContext pc, IBaseMessage inmsg)
        {
            IBaseMessagePart bodyPart = inmsg.BodyPart;

            if(bodyPart.Data != null)
            {

                VirtualStream vtstm = new VirtualStream(VirtualStream.MemoryFlag.AutoOverFlowToDisk);
                
                FonetDriver driver = FonetDriver.Make();
                driver.CloseOnExit = false;//important for biztalk to work ... set position = 0
                
                PdfRendererOptions options = new PdfRendererOptions();
                options.Title = Title;
                options.Subject = Subject;
                options.UserPassword = Password;

                driver.Options = options;

                Stream stm = bodyPart.GetOriginalDataStream();
                stm.Seek(0, SeekOrigin.Begin);

                driver.Render(stm, vtstm);

                vtstm.Seek(0, SeekOrigin.Begin);

                bodyPart.Data = vtstm;

            }
            return inmsg;
        }
        #endregion

        #region IPersistPropertyBag
    
        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">Class ID of the component.</param>
        public void GetClassID(out Guid classid)
        {
            classid = new System.Guid("88BEC85A-20EE-40ad-BFD0-319B59A0DDBC");
        }
        
        /// <summary>
        /// Not implemented.
        /// </summary>
        public void InitNew()
        {
        }
        
        /// <summary>
        /// Loads configuration property for component.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="errlog">Error status (not used in this code).</param>
        public void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Int32 errlog)
        {
            string val = (string)ReadPropertyBag(pb, "Title");
            if (val != null) title = val;

            val = (string)ReadPropertyBag(pb, "Subject");
            if (val != null) subject = val;

            val = (string)ReadPropertyBag(pb, "Password");
            if (val != null) password = val;
        }
        
        /// <summary>
        /// Saves the current component configuration into the property bag.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="fClearDirty">Not used.</param>
        /// <param name="fSaveAllProperties">Not used.</param>
        public void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Boolean fClearDirty, Boolean fSaveAllProperties)
        {
            object val = (object)title;
            WritePropertyBag(pb, "Title", val);
            
            val = (object)subject;
            WritePropertyBag(pb, "Subject", val);

            val = (object)password;
            WritePropertyBag(pb, "Password", val);

        }

        /// <summary>
        /// Reads property value from property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <returns>Value of the property.</returns>
        private static object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName)
        {
            object val = null;
            try
            {
                pb.Read(propName,out val,0);
            }

            catch(ArgumentException)
            {
                return val;
            }
            catch(Exception ex)
            {
                throw new ApplicationException( ex.Message);
            }
            return val;
        }

        /// <summary>
        /// Writes property values into a property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <param name="val">Value of property.</param>
        private static void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName, object val)
        {
            try
            {
                pb.Write(propName, ref val);
            }
            catch(Exception ex)
            {
                throw new ApplicationException( ex.Message);
            }
        }
     



        #endregion

        #region IComponentUI

        /// <summary>
        /// Component icon to use in BizTalk Editor.
        /// </summary>
        [Browsable(false)]
        public IntPtr Icon
        {
		get
		{
			return IntPtr.Zero;
		}

        }

        /// <summary>
        /// The Validate method is called by the BizTalk Editor during the build 
        /// of a BizTalk project.
        /// </summary>
        /// <param name="obj">Project system.</param>
        /// <returns>
        /// A list of error and/or warning messages encounter during validation
        /// of this component.
        /// </returns>
        public IEnumerator Validate(object obj)
        {
		IEnumerator enumerator = null;

		return enumerator;
        }

        #endregion
    }
}
