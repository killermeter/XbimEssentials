﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xbim.Common;
using Xbim.Common.Step21;
using Xbim.Ifc;
using Xbim.Ifc2x3.Kernel;
using Xbim.Ifc2x3.PropertyResource;
using Xbim.Ifc2x3.SharedBldgElements;
using Xbim.IO.Step21;
using PersistEntityExtensions = Xbim.IO.PersistEntityExtensions;
using Xbim.IO.Esent;
using Xbim.Ifc2x3;

namespace Xbim.Essentials.Tests
{
    [TestClass]
    public class TransactionalChanges
    {
        [TestMethod]
        public void TransactionLog()
        {
            using (var model = IfcStore.Create(IfcSchemaVersion.Ifc2X3, XbimStoreType.InMemoryModel))
            {
                var changed = new List<string>();
                var valuesLog = new StringWriter();
                var initialLog = new StringWriter();
                model.EntityModified += (entity, property) =>
                {
                    //use reflection to get property information for the changed property
                    var pInfo = entity.ExpressType.Properties[property];
                    changed.Add(pInfo.Name);

                    //express indices are 1 based
                    var propertyIndex = property - 1;

                    //overriden attributes have to be treated specially. But these are not present in CobieExpress.
                    if (pInfo.EntityAttribute.State == EntityAttributeState.DerivedOverride)
                        initialLog.Write("*");
                    else
                    {
                        //you can use reflection to get the current (new) value
                        var value = pInfo.PropertyInfo.GetValue(entity, null);

                        //this is part of the serialization engine but you can use it for a single property as well
                        Part21Writer.WriteProperty(pInfo.PropertyInfo.PropertyType, value, valuesLog, null, model.Metadata);
                    }
                    
                    valuesLog.WriteLine();
                };
                model.EntityNew += entity =>
                {
                    //iterate over all properties. These are sorted in the right order already.
                    foreach (var property in entity.ExpressType.Properties.Values)
                    {
                        //overriden attributes have to be treated specially. But these are not present in CobieExpress.
                        if (property.EntityAttribute.State == EntityAttributeState.DerivedOverride)
                            initialLog.Write("*");
                        else
                        {
                            var value = property.PropertyInfo.GetValue(entity, null);
                            Part21Writer.WriteProperty(property.PropertyInfo.PropertyType, value, initialLog, null, model.Metadata);
                        }
                        initialLog.WriteLine();
                    }
                };
                using (var txn = model.BeginTransaction())
                {
                    var wall = model.Instances.New<IfcWall>();
                    wall.Name = "New name";
                    wall.Description = null;

                    var pset = model.Instances.New<IfcPropertySet>();
                    pset.HasProperties.Add(model.Instances.New<IfcPropertySingleValue>());

                    Assert.IsTrue(changed.SequenceEqual(new []{"Name", "Description", "HasProperties"}));
                    Assert.AreEqual(valuesLog.ToString(), "'New name'\r\n$\r\n(#3)\r\n");
                    
                    txn.Commit();
                }
            }
        }

        [TestMethod]
        public void EsentMultiTransactionTest()
        {
            const string file = "test.ifc";
            using (var model = EsentModel.CreateTemporaryModel(new EntityFactory()))
            {
                IfcCurtainWall wall;
                using (var txn = model.BeginTransaction("New wall"))
                {
                    wall = model.Instances.New<IfcCurtainWall>(w => w.Name = "Name");
                    txn.Commit();
                }
                using (var txn = model.BeginTransaction("Edit wall"))
                {
                    wall.Description = "Description";
                    txn.Commit();
                }
                model.SaveAs(file);
            }

            using (var model = EsentModel.CreateTemporaryModel(new EntityFactory()))
            {
                model.CreateFrom(file, null, null, true, true);
                var wall = model.Instances.FirstOrDefault<IfcCurtainWall>();
                Assert.IsTrue(wall.Name == "Name");
                Assert.IsTrue(wall.Description == "Description");
            }
        }
    }
}
