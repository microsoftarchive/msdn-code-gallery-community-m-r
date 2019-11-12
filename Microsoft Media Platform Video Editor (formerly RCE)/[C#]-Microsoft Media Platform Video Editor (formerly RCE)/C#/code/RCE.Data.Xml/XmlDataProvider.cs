// <copyright file="XmlDataProvider.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: XmlDataProvider.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Data.Xml
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Web;

    using LAgger;

    using RCE.Services.Contracts;

    public class XmlDataProvider : IDataProvider
    {
        private readonly ILoggerService logger;

        private string projectPath;

        private string savedProjectsPath;

        public XmlDataProvider(ILoggerService logger)
            : this(logger, HttpContext.Current.Server.MapPath(@"bin\DefaultProject.xml"), HttpContext.Current.Server.MapPath(@"projects"))
        {
        }

        protected XmlDataProvider(ILoggerService logger, string projectPath, string savedProjectsPath)
        {
            this.logger = logger;
            this.projectPath = projectPath;
            this.savedProjectsPath = savedProjectsPath;

            if (!Directory.Exists(this.savedProjectsPath))
            {
                try
                {
                    Directory.CreateDirectory(this.savedProjectsPath);
                }
                catch (Exception ex)
                {
                    this.Log(ex);
                }
            }
        }

        public MediaBin LoadMediaBin(Uri mediaBinUri)
        {
            return new MediaBin();
        }

        public TitleTemplateCollection LoadTitleTemplates()
        {
            TitleTemplateCollection titleTemplates = new TitleTemplateCollection
                {
                    CreateTitleTemplate("FadeCenter"),
                    CreateTitleTemplate("Spinner"),
                    CreateTitleTemplate("ScrollingCenter"),
                    CreateTitleTemplate("ZoomCenter"),
                };

            return titleTemplates;
        }

        public Project LoadProject(Uri site)
        {
            Project project = null;
            string projectXml = null;

            if (site != null)
            {
                string projectId = site.ToString();

                if (projectId.EndsWith("/"))
                {
                    projectId = projectId.Substring(0, projectId.LastIndexOf("/"));
                }

                string id = projectId.Substring(projectId.LastIndexOf("/") + 1);

                string path = Path.Combine(this.savedProjectsPath, string.Concat(id, ".xml"));

                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        projectXml = reader.ReadToEnd();
                    }
                }
            }

            if (string.IsNullOrEmpty(projectXml))
            {
                using (StreamReader reader = new StreamReader(this.projectPath))
                {
                    projectXml = reader.ReadToEnd();
                }
            }

            try
            {
                project = this.Deserialize<Project>(projectXml);
            }
            catch (Exception ex)
            {
                this.Log(ex);
            }

            return project;
        }

        public bool SaveProject(Project project)
        {
            try
            {
                string content = Serialize<Project>(project);

                string id = project.Id.ToString().Substring(project.Id.ToString().LastIndexOf("/") + 1);

                string path = Path.Combine(this.savedProjectsPath, string.Concat(id, ".xml"));

                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(content);
                }

                return true;
            }
            catch (Exception ex)
            {
                this.Log(ex);

                return false;
            }
        }

        public ProjectCollection GetProjectsByUser(string userName)
        {
            ProjectCollection projects = new ProjectCollection();

            if (Directory.Exists(this.savedProjectsPath))
            {
                string[] files = Directory.GetFiles(this.savedProjectsPath, "*.xml");

                foreach (string filePath in files)
                {
                    string projectXml;

                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        projectXml = reader.ReadToEnd();
                    }

                    try
                    {
                        Project project = this.Deserialize<Project>(projectXml);

                        projects.Add(project);
                    }
                    catch (Exception ex)
                    {
                        this.Log(ex);
                    }
                }
            }

            return projects;
        }

        public bool DeleteProject(Uri site)
        {
            return false;
        }

        private static TitleTemplate CreateTitleTemplate(string name)
        {
            return new TitleTemplate
            {
                Id = CreateUri("TitleTemplates"),
                TemplateName = name,
                
                // TitleType = titleType
            };
        }

        private static Uri CreateUri(string item)
        {
            string uriString = string.Format(CultureInfo.InvariantCulture, "http://rce.litwareinc.com/samples/2.0/{0}/{1}", item, Guid.NewGuid().ToString("D"));
            return new Uri(uriString);
        }

        private static string Serialize<T>(T graph)
        {
            string serializedContent;

            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(ms, graph);
                byte[] bytes = ms.ToArray();

                serializedContent = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            }

            return serializedContent;
        }

        private T Deserialize<T>(string result)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(result);
                T graph;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    graph = (T)serializer.ReadObject(ms);
                }

                return graph;
            }
            catch (Exception ex)
            {
                this.Log(ex);
                throw;
            }
        }

        private void Log(Exception ex)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Exception: {0}{1}", ex.Message, Environment.NewLine);
            builder.AppendFormat("Stack Trace: {0}", ex.StackTrace);

            if (ex.InnerException != null)
            {
                builder.AppendFormat("{0}Inner Exception: {1}{2}", Environment.NewLine, ex.InnerException.Message, Environment.NewLine);
                builder.AppendFormat("Inner Stack Trace: {0}", ex.InnerException.StackTrace);
            }

            LogEntry entry = new LogEntry(builder.ToString(), "Data Service", 1, 0, TraceEventType.Error, "Error in Data Service");

            this.logger.LogEntries(new[] { entry });
        }
    }
}
