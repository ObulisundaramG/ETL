﻿using ETL_Service.Managers.IRepositoryManager;
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
        public async Task<string> CreateRepository(List<InfomaticaModel> InfomaticaModelList)
        {
            try
            {
                using (var dbContext = new ETLMigrationContext())
                {
                    #region Inserting into Repository
                    //Inserting into Repository folder
                    var repositoryValue = InfomaticaModelList.Select(x => x.Repository).FirstOrDefault();

                    var existingRepository = dbContext.InfaRepository.Where(x => x.RepName == repositoryValue.RepositoryName).FirstOrDefault();
                    int repositoryId = 0;
                    if (existingRepository == null)
                    {
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
                        await dbContext.SaveChangesAsync();
                        repositoryId = infaReposiotry.RepId;
                    }
                    else
                    {
                        existingRepository.ModifiedBy = "Infra";
                        existingRepository.ModifiedDate = DateTime.Now;
                        await dbContext.SaveChangesAsync();
                        repositoryId = existingRepository.RepId;
                    }

                    #endregion
                    //Getting folder list
                    var folders = InfomaticaModelList.SelectMany(x => x.Folders).ToList();
                    foreach (var folder in folders)
                    {
                        #region Inserting into folders
                        var existingFolder = dbContext.InfaFolder.Where(x => x.FldrName == folder.FolderName && x.FldrDesc == folder.FolderDescription).FirstOrDefault();
                        int folderId = 0;
                        if (existingFolder == null)
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
                            await dbContext.SaveChangesAsync();
                            folderId = infraFolder.FldrId;
                        }
                        else
                        {
                            existingFolder.ModifiedBy = "Infa";
                            existingFolder.ModifiedDate = DateTime.Now;
                            await dbContext.SaveChangesAsync();
                            folderId = existingFolder.FldrId;
                        }

                        #endregion

                        #region Inserting Workflow

                        var workFlow = folder.WorkFlow;

                        var existingWorkFlow = dbContext.InfaWorkflow.Where(x => x.WkfName == workFlow.WorkFlowName).FirstOrDefault();
                        int workFlowId = 0;
                        if (existingWorkFlow == null)
                        {
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
                            await dbContext.SaveChangesAsync();
                            workFlowId = infraWorkFlow.WkfId;
                        }
                        else
                        {
                            existingWorkFlow.WkfDesc = workFlow.WorkFlowDescription;
                            existingWorkFlow.WkfCol = workFlow.WorkFlowColumn;
                            existingWorkFlow.ModifiedBy = "Infra";
                            existingWorkFlow.ModifiedDate = DateTime.Now;
                            await dbContext.SaveChangesAsync();
                            workFlowId = existingWorkFlow.WkfId;
                        }
                        #endregion

                        #region Inserting Session
                        var session = folder.Session;

                        var existingSession = dbContext.InfaSession.Where(x => x.SessionName == session.SessionName).FirstOrDefault();
                        int sessionId = 0;
                        if (existingSession == null)
                        {
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
                            await dbContext.SaveChangesAsync();
                            sessionId = infraSession.SessionId;
                        }
                        else
                        {
                            existingSession.SessionDesc = session.SessionDescription;
                            existingSession.ModifiedBy = "Infra";
                            existingSession.ModifiedDate = DateTime.Now;
                            dbContext.SaveChanges();
                            sessionId = existingSession.SessionId;
                        }

                        #endregion

                        #region Insertin Session Config
                        var sessionConfig = folder.SessoinConfigList.ToList();
                        var existingConfigSession = dbContext.InfaSessionConfig.Where(x => x.WorkFlowId == workFlowId && x.SessionId == sessionId).FirstOrDefault();
                        if (existingConfigSession == null)
                        {
                            foreach (var item in sessionConfig)
                            {
                                var sessionConfigModel = new InfaSessionConfig
                                {
                                    WorkFlowId = workFlowId,
                                    SessionId = sessionId,
                                    ConfigAttributeName = item.ConfigAttributeName,
                                    ConfigAttributeDescValue = item.ConfigAttributeDescValue,
                                    IsDefault = item.IsDefault,
                                    SessionName = item.SessionName,
                                    XMLTag = item.XMLTag
                                };
                                dbContext.InfaSessionConfig.Add(sessionConfigModel);
                                await dbContext.SaveChangesAsync();
                            }
                        }
                        #endregion

                        #region Inserting Mapping

                        var existingMappingList = dbContext.InfaMapping.Where(x => x.FldrId == folderId).ToList();
                        var connector = folder.ConnectorList;
                        if (existingMappingList.Count <= 0)
                        {
                            var mappingList = folder.MappingList.ToList();

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
                                    XMLTag = mapping.XMLTag,
                                    LoadTimestamp = DateTime.Now,
                                    Status = "A",
                                    CreatedBy = "Infra",
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = "Infra",
                                    ModifiedDate = DateTime.Now
                                };
                                dbContext.InfaMapping.Add(infaMapping);
                                await dbContext.SaveChangesAsync();

                                var lastInsertedMappingId = infaMapping.MappingId;
                                #region Inserting into connector

                                foreach (var contor in connector.Where(x => x.IsExecuted == false))
                                {
                                    var existingConnector = dbContext.InfaConnector.FirstOrDefault(x => x.SessionId == sessionId && x.MappingId == lastInsertedMappingId);
                                    if (existingConnector == null)
                                    {
                                        var infaConnector = new InfaConnector
                                        {
                                            MappingId = lastInsertedMappingId,
                                            SessionId = sessionId,
                                            XmlTag = contor.XmlTag,
                                            FromField = contor.FromField,
                                            FromInstance = contor.FromInstance,
                                            FromInstanceType = contor.FromInstanceType,
                                            ToField = contor.ToField,
                                            ToInstance = contor.ToInstance,
                                            ToInstanceType = contor.ToInstanceType,
                                            Status = "A",
                                            CreatedBy = "Infa",
                                            CreatedDate = DateTime.Now,
                                            ModifiedBy = "Infa",
                                            ModifiedDate = DateTime.Now
                                        };
                                        dbContext.InfaConnector.Add(infaConnector);
                                        await dbContext.SaveChangesAsync();
                                        contor.IsExecuted = true;
                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            foreach (var existingMapping in existingMappingList)
                            {
                                var mapping = folder.MappingList.FirstOrDefault(x => x.FieldName == existingMapping.FieldName && x.XMLTag == existingMapping.XMLTag);
                                existingMapping.MappingName = mapping.MappingName;
                                existingMapping.MappingDesc = mapping.MappingDescription;
                                existingMapping.MappingIsvalid = mapping.MappingIsValid;
                                existingMapping.TransType = mapping.TransType;
                                existingMapping.TransName = mapping.TransName;
                                existingMapping.TransReusable = mapping.TransReusable;
                                existingMapping.FieldName = mapping.FieldName;
                                existingMapping.FieldDesc = mapping.FieldDescription;
                                existingMapping.Datatype = mapping.DataType;
                                existingMapping.Porttype = mapping.PortType;
                                existingMapping.Precision = mapping.Prescision;
                                existingMapping.Scale = mapping.Scale;
                                existingMapping.Expression = mapping.Expression;
                                existingMapping.ExpressionType = mapping.ExpressionType;
                                existingMapping.TableattributeName = mapping.TableAttributeName;
                                existingMapping.TableattributeValue = mapping.TableAttributeValue;
                                existingMapping.SortKey = mapping.SortKey;
                                existingMapping.SortDirection = mapping.SortDirection;
                                existingMapping.InstanceName = mapping.InstanceName;
                                existingMapping.InstanceType = mapping.InstanceType;
                                existingMapping.XMLTag = mapping.XMLTag;
                                existingMapping.ModifiedBy = "Infra";
                                existingMapping.ModifiedDate = DateTime.Now;
                                await dbContext.SaveChangesAsync();
                            }
                        }
                        #endregion

                    }
                }
                return "Successfully Inserted";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
