using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace TaskTracker
{
    public static class XmlHelper
    {
        public static List<List<RoleMapElem>> GetRoleMappingsFromXml()
        {
            List<List<RoleMapElem>> list = new List<List<RoleMapElem>>();
            for (int i = 0; i < 4; i++)
            {
                list.Add(new List<RoleMapElem>());
            }

            if (!System.IO.File.Exists(RoleMapElem.RoleMapXMLFilePath))
            {
                return list;
            }
            FileStream fs = new FileStream(RoleMapElem.RoleMapXMLFilePath, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            XmlSerializer x = new XmlSerializer(list.GetType());
            list = (List<List<RoleMapElem>>)x.Deserialize(reader);
            fs.Flush();
            reader.Close();
            fs.Close();
            return list;
        }
        
        public static void AppendRoleMappingToXml(FormCollection formCollection, string objectId)
        {
            List<List<RoleMapElem>> mappings;
            int mappingId = 0;
            if (System.IO.File.Exists(RoleMapElem.RoleMapXMLFilePath))
            {
                mappings = (List<List<RoleMapElem>>)XmlHelper.GetRoleMappingsFromXml();
                foreach (List<RoleMapElem> roleList in mappings)
                {
                    mappingId += roleList.Count;
                }
            }
            else
            {
                mappings = new List<List<RoleMapElem>>();
                for (int i = 0; i < 4; i++)
                {
                    mappings.Add(new List<RoleMapElem>());
                }
            }

            for (int i = 0; i < RoleMapElem.Roles.Length; i++)
            {
                if(RoleMapElem.Roles[i].Equals(formCollection["roletype"]))
                {
                    mappings[i].Add(new RoleMapElem(objectId, formCollection["roletype"], mappingId.ToString(CultureInfo.InvariantCulture)));
                }

            }
            XmlSerializer s = new XmlSerializer(typeof(List<List<RoleMapElem>>));
            TextWriter writer = new StreamWriter(RoleMapElem.RoleMapXMLFilePath);
            s.Serialize(writer, mappings);
            writer.Close();
        }
        
        public static void RemoveRoleMappingsFromXml(FormCollection formCollection)
        {
            if (!System.IO.File.Exists(RoleMapElem.RoleMapXMLFilePath))
            { 
                return; 
            }
            List<List<RoleMapElem>> roleMappings = (List<List<RoleMapElem>>)XmlHelper.GetRoleMappingsFromXml();
            RoleMapElem toRemove = null;
            foreach (string key in formCollection.Keys)
            {
                if (formCollection[key].Equals("delete"))
                {
                    foreach (List<RoleMapElem> list in roleMappings)
                    {
                        foreach (RoleMapElem elem in list)
                        {
                            if (elem.MappingId.Equals(key))
                            {
                                toRemove = elem;
                            }
                        }
                        if (toRemove != null)
                        {
                            list.Remove(toRemove);
                            toRemove = null;
                        }
                    }
                }
            }

            XmlSerializer s = new XmlSerializer(roleMappings.GetType());
            TextWriter writer = new StreamWriter(RoleMapElem.RoleMapXMLFilePath);
            s.Serialize(writer, roleMappings);
            writer.Close();
        }
        
        public static List<ACLElem> GetACLElemsFromXml()
        {
            if (!System.IO.File.Exists(ACLElem.ACLXMLFilePath))
            {
                return new List<ACLElem>();
            }
            FileStream fs = new FileStream(ACLElem.ACLXMLFilePath, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            XmlSerializer x = new XmlSerializer(typeof(List<ACLElem>));
            List<ACLElem> list = (List<ACLElem>)x.Deserialize(reader);
            fs.Flush();
            reader.Close();
            fs.Close();
            return list;
        }
        
        public static void AppendACLElemToXml(string objectId)
        {
            List<ACLElem> ACLElems = (List<ACLElem>)XmlHelper.GetACLElemsFromXml();
            int mappingId = ACLElems.Count;
            ACLElems.Add(new ACLElem(objectId, mappingId.ToString(CultureInfo.InvariantCulture)));
            XmlSerializer s = new XmlSerializer(typeof(List<ACLElem>));
            TextWriter writer = new StreamWriter(ACLElem.ACLXMLFilePath);
            s.Serialize(writer, ACLElems);
            writer.Close();
        }
        
        public static void RemoveACLElemFromXml(FormCollection formCollection)
        {
            if (!System.IO.File.Exists(ACLElem.ACLXMLFilePath)) 
            { 
                return; 
            }

            List<ACLElem> ACLElems = (List<ACLElem>)XmlHelper.GetACLElemsFromXml();
            ACLElem toRemove = null;
            foreach (string key in formCollection.Keys)
            {
                if (formCollection[key].Equals("delete"))
                {
                    foreach (ACLElem elem in ACLElems)
                    {
                        if (elem.MappingId.Equals(key))
                        {
                            toRemove = elem;
                        }
                    }

                    if (toRemove != null)
                    {
                        ACLElems.Remove(toRemove);
                        toRemove = null;
                    }
                }
            }

            XmlSerializer s = new XmlSerializer(typeof(List<ACLElem>));
            TextWriter writer = new StreamWriter(ACLElem.ACLXMLFilePath);
            s.Serialize(writer, ACLElems);
            writer.Close();
        }
        
        public static List<TaskElem> GetTaskElemsFromXml()
        {
            if (!System.IO.File.Exists(TaskElem.TasksXMLFilePath)) return new List<TaskElem>();
            FileStream fs = new FileStream(TaskElem.TasksXMLFilePath, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            XmlSerializer x = new XmlSerializer(typeof(List<TaskElem>));
            List<TaskElem> list = (List<TaskElem>)x.Deserialize(reader);
            fs.Flush();
            reader.Close();
            fs.Close();
            return list;
        }
        
        public static void AppendTaskElemToXml(FormCollection formCollection)
        {
            List<TaskElem> TaskElems = (List<TaskElem>)XmlHelper.GetTaskElemsFromXml();
            int taskId = TaskElems.Count == 0 ? 0 : Convert.ToInt32(((TaskElem)TaskElems[TaskElems.Count - 1]).TaskId, CultureInfo.InvariantCulture) + 1;
            TaskElems.Add(new TaskElem(taskId.ToString(CultureInfo.InvariantCulture), formCollection["newtask"], "NotStarted"));

            XmlSerializer s = new XmlSerializer(typeof(List<TaskElem>));
            TextWriter writer = new StreamWriter(TaskElem.TasksXMLFilePath);
            s.Serialize(writer, TaskElems);
            writer.Close();
        }
        
        public static void ChangeTaskAttribute(FormCollection formCollection)
        {
            List<TaskElem> TaskElems = (List<TaskElem>)XmlHelper.GetTaskElemsFromXml();

            foreach (TaskElem task in TaskElems)
            {
                if (formCollection[task.TaskId] != null)
                {
                    task.Status = formCollection[task.TaskId];
                }
            }

            XmlSerializer s = new XmlSerializer(typeof(List<TaskElem>));
            TextWriter writer = new StreamWriter(TaskElem.TasksXMLFilePath);
            s.Serialize(writer, TaskElems);
            writer.Close();
        }
    }
}