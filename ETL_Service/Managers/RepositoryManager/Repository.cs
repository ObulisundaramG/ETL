using ETL_Service.Managers.IRepositoryManager;
using ETL_Service.Models;
using ETL_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_Service.Managers.RepositoryManager
{
    public class Repository : IRepository
    {
        public string CreateRepository(List<InfomaticaModel> InfomaticaModelList)
        {
            try
            {
                using (var dbContext = new ETLMigrationContext())
                {
                    //Inserting into Repository folder
                    var repositoryValue = InfomaticaModelList.Select(x => x.Repository).FirstOrDefault();
                    var infaReposiotry = new InfaRepository
                    {
                        RepName = repositoryValue.RepositoryName,
                        RepDesc = repositoryValue.RepositoryDescription,
                        DatabaseType = repositoryValue.RepositoryDataBaseType,
                        Status = "A",
                        CreatedBy = "Obuli",
                        CreatedDate = DateTime.Now,
                        ModifiedBy = "Obuli",
                        ModifiedDate = DateTime.Now
                    };
                    dbContext.InfaRepository.Add(infaReposiotry);
                    dbContext.SaveChanges();
                    var repositoryId = infaReposiotry.RepId;

                    //Check if the work flow present in the folder
                    //If not should now insert.
                    var workFlow = InfomaticaModelList.Select(x => x.WorkFlow).FirstOrDefault();
                    if (workFlow == null)
                        return "";

                    //Inserting into InfraFolder;
                    var folderList = InfomaticaModelList.SelectMany(x => x.Folders).ToList();
                    foreach (var folder in folderList)
                    {
                        var infraFolder = new InfaFolder
                        {
                            RepId = repositoryId,
                            FldrName = folder.FolderName,
                            FldrDesc = folder.FolderDescription,
                            Shared = folder.Shared,
                            Status = "A",
                            CreatedBy = "Infa",
                            CreatedDate = DateTime.Now,
                            ModifiedBy = "Infa",
                            ModifiedDate = DateTime.Now
                        };
                        dbContext.InfaFolder.Add(infraFolder);
                        dbContext.SaveChanges();

                        var folderId = infraFolder.FldrId;

                        #region Inserting Workflow
                        //var workFlow = InfomaticaModelList.Select(x => x.WorkFlow).FirstOrDefault();
                        var infraWorkFlow = new InfaWorkflow
                        {
                            FldrId = folderId,
                            WkfName = workFlow.WorkFlowName,
                            WkfDesc = workFlow.WorkFlowDescription,
                            WkfCol = workFlow.WorkFlowColumn,
                            Status = "A",
                            CreatedBy = "Infra",
                            CreatedDate = DateTime.Now,
                            ModifiedBy = "Infra",
                            ModifiedDate = DateTime.Now
                        };
                        dbContext.InfaWorkflow.Add(infraWorkFlow);
                        dbContext.SaveChanges();

                        var workFlowId = infraWorkFlow.WkfId;

                        #endregion

                        #region Inserting Session
                        var session = InfomaticaModelList.Select(x => x.Session).FirstOrDefault();
                        var infraSession = new InfaSession
                        {
                            WkfId = workFlowId,
                            SessionName = session.SessionName,
                            SessionDesc = session.SessionDescription,
                            Status = "A",
                            CreatedBy = "Infra",
                            CreatedDate = DateTime.Now,
                            ModifiedBy = "Infra",
                            ModifiedDate = DateTime.Now
                        };
                        dbContext.InfaSession.Add(infraSession);
                        dbContext.SaveChanges();

                        var sessionId = infraSession.SessionId;

                        #endregion

                        #region Inserting Mapping

                        var mappingList = InfomaticaModelList.SelectMany(x => x.MappingList).ToList();

                        foreach (var mapping in mappingList)
                        {
                            var infaMapping = new InfaMapping
                            {
                                SessionId = sessionId,
                                FldrId = folderId,
                                MappingName = mapping.MappingName,
                                MappingDesc = mapping.MappingDescription,
                                MappingIsvalid = mapping.MappingIsValid,
                                TransType = mapping.TransType,
                                TransName = mapping.TransName,
                                TransReusable = mapping.TransReusable,
                                FieldName = mapping.FieldName,
                                FieldDesc = mapping.FieldDescription,
                                Datatype = mapping.DataType,
                                Porttype = mapping.PortType,
                                Precision = mapping.Prescision,
                                Scale = mapping.Scale,
                                Expression = mapping.Expression,
                                ExpressionType = mapping.ExpressionType,
                                TableattributeName = mapping.TableAttributeName,
                                TableattributeValue = mapping.TableAttributeValue,
                                SortKey = mapping.SortKey,
                                SortDirection = mapping.SortDirection,
                                InstanceName = mapping.InstanceName,
                                InstanceType = mapping.InstanceType,
                                LoadTimestamp = DateTime.Now,
                                Status = "A",
                                CreatedBy = "Infra",
                                CreatedDate = DateTime.Now,
                                ModifiedBy = "Infra",
                                ModifiedDate = DateTime.Now
                            };
                            dbContext.InfaMapping.Add(infaMapping);
                            dbContext.SaveChanges();
                        }

                        #endregion


                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
