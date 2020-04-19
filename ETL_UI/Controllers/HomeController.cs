using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Data;
using System.IO;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using ETL_UI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace InfoMatica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index() 
        {
            return View();
        }
        [HttpPost("FileUpload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            List<InfomaticaModel> InfomaticaModelList = new List<InfomaticaModel>();
            foreach (string fileNameFromDirectory in Directory.EnumerateFiles(@"C:\ETL_Projects\FileFolder", "*.xml"))
            {
                string contents = System.IO.File.ReadAllText(fileNameFromDirectory);
                //string filename = GetUniqueFileName(fileNameFromDirectory);
                InfomaticaModel infomatica = ConvertXmlToInformaticaMode(contents);
                InfomaticaModelList.Add(infomatica);
            }
            string response = await InsertIntoDb(InfomaticaModelList);
            ViewBag.Message = "File successfully uploaded";
            DataTable dt = new DataTable();
            return View(dt);
        }
        public async Task<string> InsertIntoDb(List<InfomaticaModel> InfomaticaModelList)
        {
            string result = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(InfomaticaModelList));
                var response = await client.PostAsync("https://localhost:44384/api/Repository", new StringContent(JsonConvert.SerializeObject(InfomaticaModelList), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    //product = JsonConvert.DeserializeObject<Product>(data);
                }
            }
            return result;
        }
        public InfomaticaModel ConvertXmlToInformaticaMode(string fileName)
        {
            //Initialize Model
            InfomaticaModel Infomatica = new InfomaticaModel();
            XDocument xdoc = XDocument.Parse(fileName);
            var repositorys = xdoc.Descendants("REPOSITORY");
            if (repositorys == null) return new InfomaticaModel();

            //Step 1 : Get Repository Details.
            Infomatica.Repository = (from reposit in repositorys
                                     select new Repository
                                     {
                                         RepositoryName = reposit.Attribute("NAME").SetAttributeValue(),
                                         RepositoryDataBaseType = reposit.Attribute("DATABASETYPE").SetAttributeValue(),
                                         RepositoryDescription = "Infomatica"
                                     }).FirstOrDefault();

            //Step 2 : Get Folder Informations
            var folders = xdoc.Descendants("FOLDER");

            if (folders == null) return new InfomaticaModel();
            foreach (var folder in folders)
            {
                var workFlowNode = folder.Descendants("WORKFLOW");

                if (workFlowNode.Count() <= 0) break;

                Folder Folder = new Folder();
                Folder.FolderName = folder.Attribute("NAME").SetAttributeValue();
                Folder.FolderDescription = folder.Attribute("DESCRIPTION").SetAttributeValue();
                Folder.Shared = folder.Attribute("SHARED").SetAttributeValue();
                Folder.Owner = folder.Attribute("OWNER").SetAttributeValue();
                Infomatica.Folders.Add(Folder);
                //Step 3 : Get workflow 

                //var workFlowNode = folder.Descendants("WORKFLOW");
                WorkFlow wkFlow = new WorkFlow();
                //Infomatica.WorkFlow = new WorkFlow();
                wkFlow.WorkFlowName = workFlowNode.Select(x => x.Attribute("NAME").SetAttributeValue()).FirstOrDefault();
                wkFlow.WorkFlowDescription = workFlowNode.Select(x => x.Attribute("DESCRIPTION").SetAttributeValue()).FirstOrDefault();
                wkFlow.WorkFlowColumn = "";
                //Step 4 : Get session inormation
                var sessionInformation = workFlowNode.Descendants("SESSION");
                Session sson = new Session();
                sson.SessionName = sessionInformation.Select(X => X.Attribute("NAME").SetAttributeValue()).FirstOrDefault();
                sson.SessionDescription = sessionInformation.Select(X => X.Attribute("DESCRIPTION").SetAttributeValue()).FirstOrDefault();
                sson.MappingName = sessionInformation.Select(X => X.Attribute("MAPPINGNAME").SetAttributeValue()).FirstOrDefault();

                Folder.WorkFlow = wkFlow;
                Folder.Session = sson;

                //Get Session Config
                var sessionConfigInformation = folder.Descendants("CONFIG");
                foreach (var config in sessionConfigInformation)
                {
                    SessionConfigModel configModel = new SessionConfigModel();
                    configModel.SessionName = sson.SessionName;
                    configModel.XMLTag = config.Name.LocalName;
                    configModel.ConfigAttributeName = config.Attribute("NAME").SetAttributeValue();
                    configModel.ConfigAttributeDescValue = config.Attribute("VALUE").SetAttributeValue();
                    configModel.IsDefault = config.Attribute("ISDEFAULT").SetAttributeValue();
                    Folder.SessoinConfigList.Add(configModel);
                    var configElements = config.Elements();
                    foreach (var elm in configElements)
                    {
                        SessionConfigModel configModelElm = new SessionConfigModel();
                        configModelElm.SessionName = sson.SessionName;
                        configModelElm.XMLTag = config.Name.LocalName;
                        configModelElm.ConfigAttributeName = elm.Attribute("NAME").SetAttributeValue();
                        configModelElm.ConfigAttributeDescValue = elm.Attribute("VALUE").SetAttributeValue();
                        configModelElm.IsDefault = elm.Attribute("ISDEFAULT").SetAttributeValue();
                        Folder.SessoinConfigList.Add(configModelElm);
                    }
                }
                //Step 5 : Get mapping information
                var mappingNode = folder.Descendants("MAPPING");

                string mappingName = mappingNode.Select(x => x.Attribute("NAME").Value).FirstOrDefault();
                string mapingdesc = mappingNode.Select(x => x.Attribute("DESCRIPTION").Value).FirstOrDefault();
                string isvalid = mappingNode.Select(x => x.Attribute("ISVALID").Value).FirstOrDefault();

                var transformationInformation = folder.Descendants("TRANSFORMATION");

                foreach (var transformation in transformationInformation)
                {
                    var transformationFieldElements = transformation.Elements();
                    //Step 7 : Get transformation
                    foreach (var transfermationField in transformationFieldElements)
                    {
                        Mapping Mapping = new Mapping();
                        Mapping.MappingName = mappingName;
                        Mapping.MappingDescription = mapingdesc;
                        Mapping.MappingIsValid = isvalid;
                        Mapping.XMLTag = transfermationField.Name.LocalName;
                        Mapping.TransName = transformation.Attribute("NAME").SetAttributeValue();
                        Mapping.TransType = transformation.Attribute("TYPE").SetAttributeValue();
                        Mapping.TransReusable = transformation.Attribute("REUSABLE").SetAttributeValue();
                        Mapping.FieldName = transfermationField.Attribute("NAME").SetAttributeValue();
                        Mapping.FieldDescription = transfermationField.Attribute("DESCRIPTION").SetAttributeValue();
                        Mapping.DataType = transfermationField.Attribute("DATATYPE").SetAttributeValue();
                        Mapping.PortType = transfermationField.Attribute("PORTTYPE").SetAttributeValue();
                        Mapping.Prescision = transfermationField.Attribute("PRECISION").SetAttributeValueInt();
                        Mapping.Scale = transfermationField.Attribute("SCALE").SetAttributeValueInt();
                        Mapping.Expression = transfermationField.Attribute("EXPRESSION").SetAttributeValue();
                        Mapping.ExpressionType = transfermationField.Attribute("EXPRESSIONTYPE").SetAttributeValue();

                        if (transfermationField.Name.LocalName == "TABLEATTRIBUTE")
                        {
                            Mapping.TableAttributeName = transfermationField.Attribute("NAME").SetAttributeValue();
                            Mapping.TableAttributeValue = transfermationField.Attribute("VALUE").SetAttributeValue();
                        }
                        else
                        {
                            Mapping.TableAttributeName = string.Empty;
                            Mapping.TableAttributeValue = string.Empty;
                        }
                        Mapping.SortKey = transfermationField.Attribute("ISSORTKEY").SetAttributeValue();
                        Mapping.SortDirection = transfermationField.Attribute("SORTDIRECTION").SetAttributeValue();
                        if (transfermationField.Name.LocalName == "INSTANCE")
                        {
                            Mapping.InstanceName = transfermationField.Attribute("NAME").SetAttributeValue();
                            Mapping.TableAttributeValue = transfermationField.Attribute("TRANSFORMATION_TYPE").SetAttributeValue();
                        }
                        else
                        {
                            Mapping.InstanceName = string.Empty;
                            Mapping.TableAttributeValue = string.Empty;
                        }
                        Folder.MappingList.Add(Mapping);
                    }
                }

                var instanceField = folder.Descendants("INSTANCE");
                foreach (var instance in instanceField)
                {
                    if (instance.Name.LocalName == "INSTANCE")
                    {
                        Mapping Mapping = new Mapping();
                        Mapping.MappingName = mappingName;
                        Mapping.MappingDescription = mapingdesc;
                        Mapping.XMLTag = instance.Name.LocalName;
                        Mapping.MappingIsValid = isvalid;
                        Mapping.InstanceName = instance.Attribute("NAME").SetAttributeValue();
                        Mapping.InstanceType = instance.Attribute("TYPE").SetAttributeValue();
                        Folder.MappingList.Add(Mapping);
                    }
                }
                var connectorField = folder.Descendants("CONNECTOR");
                foreach (var conect in connectorField)
                {
                    if (conect.Name.LocalName == "CONNECTOR")
                    {
                        Connector connector = new Connector();
                        connector.FromField = conect.Attribute("FROMFIELD").SetAttributeValue();
                        connector.XmlTag = conect.Name.LocalName;
                        connector.FromInstance = conect.Attribute("FROMINSTANCETYPE").SetAttributeValue();
                        connector.ToField = conect.Attribute("TOFIELD").SetAttributeValue();
                        connector.ToInstance = conect.Attribute("TOINSTANCE").SetAttributeValue();
                        connector.ToInstanceType = conect.Attribute("TOINSTANCETYPE").SetAttributeValue();
                        Folder.ConnectorList.Add(connector);
                        //Folder.MappingList.Add(Mapping);
                    }
                }
                
            }
            return Infomatica;
        }

        private string GetUniqueFileName(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }


    public static class NullValueChecker
    {
        public static string SetAttributeValue(this XAttribute value)
        {
            return value == null ? "" : value.Value;
        }
        public static int SetAttributeValueInt(this XAttribute value)
        {
            return value == null ? 0 : Convert.ToInt32(value.Value);
        }
    }
}
