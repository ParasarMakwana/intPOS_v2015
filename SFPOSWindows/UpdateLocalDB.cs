using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;

namespace SFPOSWindows
{
    public class UpdateLocalDB
    {
        public void UpdateLocalDatabase(string connectionString)
        {
            SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);

            #region Table Schemas
            List<TableSchema> tableSchemas = new List<TableSchema>
            {

                #region Table Counter Master
                new TableSchema("tbl_CounterMaster", new List<SchemaField>
                {
                    new SchemaField("CounterID", "BIGINT"),
                    new SchemaField("StoreID", "BIGINT"),
                    new SchemaField("CounterNo", "INT"),
                    new SchemaField("CounterIP", "NVARCHAR(15)"),
                    new SchemaField("IsActive", "BIT"),
                    new SchemaField("CreatedBy", "BIGINT"),
                    new SchemaField("CreatedDate", "DATETIME"),
                    new SchemaField("UpdatedBy", "BIGINT"),
                    new SchemaField("UpdatedDate", "DATETIME")
                }),
                #endregion

                #region Table Department Master
                new TableSchema("tbl_DepartmentMaster", new List<SchemaField>
                {
                    new SchemaField("DepartmentID", "BIGINT"),
                    new SchemaField("DepartmentName", "NVARCHAR(500)"),
                    new SchemaField("IsFoodStamp", "BIT"),
                    new SchemaField("TaxGroupID", "BIGINT"),
                    new SchemaField("UnitMeasureID", "BIGINT"),
                    new SchemaField("Remark", "NVARCHAR(500)"),
                    new SchemaField("IsActive", "BIT"),
                    new SchemaField("IsDelete", "BIT"),
                    new SchemaField("CreatedBy", "BIGINT"),
                    new SchemaField("CreatedDate", "DATETIME"),
                    new SchemaField("UpdatedBy", "BIGINT"),
                    new SchemaField("UpdatedDate", "DATETIME"),
                    new SchemaField("AgeVarificationAge", "INT"),
                    new SchemaField("DepartmentNo", "BIGINT"),
                    new SchemaField("SubNo", "BIGINT"),
                    new SchemaField("DepartmentBtn", "BIT"),
                    new SchemaField("BtnCode", "NVARCHAR(25)"),
                    new SchemaField("DepartmentBtnIndex", "BIGINT"),
                    new SchemaField("IsForceTax", "BIT"),
                    new SchemaField("ForcedTaxSuffix", "NVARCHAR(50)"),
                    new SchemaField("ForcedTaxSection", "BIGINT")
                }),
                #endregion

                #region Table Employee Master
                new TableSchema("tbl_EmployeeMaster", new List<SchemaField>
                {
                    new SchemaField("EmployeeID", "BIGINT"),
                    new SchemaField("RoleID", "BIGINT"),
                    new SchemaField("StoreID", "BIGINT"),
                    new SchemaField("FirstName", "NVARCHAR(50)"),
                    new SchemaField("LastName", "NVARCHAR(50)"),
                    new SchemaField("EmailID", "NVARCHAR(256)"),
                    new SchemaField("PhoneNo", "NVARCHAR(50)"),
                    new SchemaField("Password", "NVARCHAR(50)"),
                    new SchemaField("IsActive", "BIT"),
                    new SchemaField("IsDelete", "BIT"),
                    new SchemaField("CreatedBy", "BIGINT"),
                    new SchemaField("CreatedDate", "DATETIME"),
                    new SchemaField("UpdatedBy", "BIGINT"),
                    new SchemaField("UpdatedDate", "DATETIME"),
                    new SchemaField("MaxVoidAmount", "DECIMAL(10,2)"),
                    new SchemaField("BirthDate", "DATETIME"),
                    new SchemaField("IsCashPayout", "BIT"),
                    new SchemaField("IsLottoFunction", "BIT")
                }),
                #endregion

                #region Table LottoTrans
                new TableSchema("tbl_LottoTrans", new List<SchemaField>
                {
                    new SchemaField("LottoID", "BIGINT"),
                    new SchemaField("StoreID", "BIGINT"),
                    new SchemaField("LottoType", "INT"),
                    new SchemaField("LottoPrice", "DECIMAL(18,2)"),
                    new SchemaField("CounterIP", "NVARCHAR(25)"),
                    new SchemaField("MacAddress", "NVARCHAR(25)"),
                    new SchemaField("IsActive", "BIT"),
                    new SchemaField("IsDelete", "BIT"),
                    new SchemaField("CreatedDate", "DATETIME"),
                    new SchemaField("CreatedBy", "BIGINT"),
                    new SchemaField("UpdatedDate", "DATETIME"),
                    new SchemaField("UpdatedBy", "BIGINT")
                }),
                #endregion

                #region Table OrderDetail
                new TableSchema("tbl_OrderDetail", new List<SchemaField>
                {
                    new SchemaField("OrderDetailID", "BIGINT"),
                    new SchemaField("OrderID", "BIGINT"),
                    new SchemaField("ProductID", "BIGINT"),
                    new SchemaField("StoreID", "BIGINT"),
                    new SchemaField("UPCCode", "NVARCHAR(50)"),
                    new SchemaField("ProductName", "NVARCHAR(100)"),
                    new SchemaField("Quantity", "DECIMAL(18,2)"),
                    new SchemaField("SellPrice", "DECIMAL(18,2)"),
                    new SchemaField("Discount", "DECIMAL(18,2)"),
                    new SchemaField("finalPrice", "DECIMAL(18,2)"),
                    new SchemaField("IsFoodStamp", "BIT"),
                    new SchemaField("IsScale", "BIT"),
                    new SchemaField("IsTax", "BIT"),
                    new SchemaField("FoodStampTotal", "DECIMAL(18,2)"),
                    new SchemaField("CreatedBy", "BIGINT"),
                    new SchemaField("CreatedDate", "DATETIME"),
                    new SchemaField("DiscountApplyed", "BIT DEFAULT (0)"),
                    new SchemaField("IsRefund", "BIT"),
                    new SchemaField("TaxAmount", "DECIMAL(18,2)"),
                    new SchemaField("IsCancel", "BIT"),
                    new SchemaField("IsForceTax", "BIT"),
                    new SchemaField("DepartmentID", "BIGINT"),
                    new SchemaField("SectionID", "BIGINT"),
                    new SchemaField("CasePriceApplied", "BIT"),
                    new SchemaField("GroupQty", "DECIMAL(18,2)"),
                    new SchemaField("GroupPrice", "DECIMAL(18,2)"),
                    new SchemaField("CaseQty", "DECIMAL(18,2)"),
                    new SchemaField("CasePrice", "DECIMAL(18,2)"),
                    new SchemaField("IsTaxCarry", "BIT"),
                    new SchemaField("IsReturn", "BIT"),
                    new SchemaField("OverridePrice", "DECIMAL(18,2)"),
                    new SchemaField("IsForceTaxDept", "BIT NOT NULL DEFAULT 0"),
                    new SchemaField("ManWT", "BIT")
                }),
                #endregion

                #region Table TransSuspen Master
                new TableSchema("tbl_TransSuspenMaster", new List<SchemaField>
                {
                    new SchemaField("TransSuspendID", "BIGINT"),
                    new SchemaField("TransSuspendCode", "NVARCHAR(50)"),
                    new SchemaField("ProductID", "BIGINT"),
                    new SchemaField("ProductName", "NVARCHAR(100)"),
                    new SchemaField("UPCCode", "NVARCHAR(50)"),
                    new SchemaField("Quantity", "DECIMAL(18,2)"),
                    new SchemaField("SellPrice", "DECIMAL(18,2)"),
                    new SchemaField("FinalPrice", "DECIMAL(18,2)"),
                    new SchemaField("TotalAmount", "DECIMAL(18,2)"),
                    new SchemaField("GrossAmount", "DECIMAL(18,2)"),
                    new SchemaField("IsScale", "BIT"),
                    new SchemaField("IsFoodStamp", "BIT"),
                    new SchemaField("IsTax", "BIT"),
                    new SchemaField("DiscountApplyed", "BIT"),
                    new SchemaField("Status", "BIT"),
                    new SchemaField("StoreID", "BIGINT"),
                    new SchemaField("CreatedBy", "BIGINT"),
                    new SchemaField("CreatedDate", "DATETIME"),
                    new SchemaField("IsDelete", "BIT"),
                    new SchemaField("Tax", "DECIMAL(18,2)"),
                    new SchemaField("TotalTaxAmount", "DECIMAL(18,2)"),
                    new SchemaField("DepartmentID", "BIGINT"),
                    new SchemaField("SectionID", "BIGINT"),
                    new SchemaField("GroupQty", "DECIMAL(18,2)"),
                    new SchemaField("GroupPrice", "DECIMAL(18,2)"),
                    new SchemaField("CaseQty", "DECIMAL(18,2)"),
                    new SchemaField("CasePrice", "DECIMAL(18,2)"),
                    new SchemaField("CasePriceApplied", "BIT"),
                    new SchemaField("IsManWTRefund", "BIT"),
                    new SchemaField("FSEligibleAmount", "DECIMAL(18,2)")
                }),
                #endregion

                #region Table ProductMaster
                new TableSchema("tbl_ProductMaster", new List<SchemaField>
                {
                    new SchemaField("ProductID", "BIGINT"),
                    new SchemaField("ProductName", "NVARCHAR(100)"),
                    new SchemaField("UPCCode", "NVARCHAR(50)"),
                    new SchemaField("CertCode", "NVARCHAR(50)"),
                    new SchemaField("DepartmentID", "BIGINT"),
                    new SchemaField("SectionID", "BIGINT"),
                    new SchemaField("UnitMeasureID", "BIGINT"),
                    new SchemaField("Price", "DECIMAL(18,2)"),
                    new SchemaField("TaxGroupID", "BIGINT"),
                    new SchemaField("Image", "NVARCHAR(4000)"),
                    new SchemaField("IsFoodStamp", "BIT"),
                    new SchemaField("AgeVerification", "BIT"),
                    new SchemaField("IsScaled", "BIT"),
                    new SchemaField("TareWeight", "DECIMAL(18,2)"),
                    new SchemaField("GroupQty", "DECIMAL(18,2)"),
                    new SchemaField("GroupPrice", "DECIMAL(18,2)"),
                    new SchemaField("LinkedUPCCode", "NVARCHAR(50)"),
                    new SchemaField("IsActive", "BIT"),
                    new SchemaField("IsDelete", "BIT"),
                    new SchemaField("CreatedDate", "DATETIME"),
                    new SchemaField("CreatedBy", "BIGINT"),
                    new SchemaField("UpdatedDate", "DATETIME"),
                    new SchemaField("UpdatedBy", "BIGINT"),
                    new SchemaField("LabeledPrice", "BIT"),
                    new SchemaField("CaseQty", "DECIMAL(18,2)"),
                    new SchemaField("CasePrice", "DECIMAL(18,2)"),
                    new SchemaField("IsGroupPrice", "BIT"),
                    new SchemaField("SecondaryPLU", "NVARCHAR(25)"),
                    new SchemaField("PalletQTY", "NVARCHAR(25)"),
                    new SchemaField("FSEligibleAmount", "DECIMAL(18,2)")
                })
                #endregion
            };

            #endregion

            // Check and update the schemas for all tables
            foreach (TableSchema tableSchema in tableSchemas)
            {
                tableSchema.CheckAndUpdateSchema(connectionString);
            }
        }

        public class TableSchema
        {
            public string TableName { get; private set; }
            public List<SchemaField> Fields { get; private set; }

            public TableSchema(string tableName, List<SchemaField> fields)
            {
                TableName = tableName;
                Fields = fields;
            }

            public void CheckAndUpdateSchema(string connectionString)
            {
                try
                {
                    if (!IsTableExists(connectionString))
                    {
                        // Table does not exist, create it
                        CreateTable(connectionString);
                    }

                    // Get the actual schema from the database
                    Dictionary<string, string> actualSchema = GetActualSchema(connectionString);

                    #region  Remove any columns from the actual schema that are not defined in the schema code
                    //foreach (var columnName in actualSchema.Keys.ToList())
                    //{
                    //    if (!Fields.Any(field => field.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase)))
                    //    {
                    //        // Column exists in the database but is not in the defined schema, drop it
                    //        DropColumnFromDatabase(columnName, connectionString);
                    //        actualSchema.Remove(columnName);
                    //    }
                    //}
                    #endregion  

                    // Compare the defined schema with the actual schema
                    foreach (SchemaField field in Fields)
                    {
                        if (!actualSchema.ContainsKey(field.Name))
                        {
                            // Field doesn't exist in the database, add it
                            AddFieldToDatabase(field, connectionString);
                        }
                        else if (actualSchema[field.Name] != field.DataType)
                        {
                            // Field data type is different in the database, update it
                            UpdateFieldTypeInDatabase(field, connectionString);
                        }
                    }
                }
                catch (Exception ex)
                {
                   
                }
            }

            private void DropColumnFromDatabase(string columnName, string connectionString)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL statement to drop the column from the table
                    string query = $"ALTER TABLE {TableName} DROP COLUMN {columnName};";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }

            private bool IsTableExists(string connectionString)
            {
                using (SqlCeConnection connection = new SqlCeConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{TableName}'";

                    using (SqlCeCommand command = new SqlCeCommand(query, connection))
                    {
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }

            private void CreateTable(string connectionString)
            {
                using (SqlCeConnection connection = new SqlCeConnection(connectionString))
                {
                    connection.Open();

                    string query = $"CREATE TABLE {TableName} (";
                    foreach (SchemaField field in Fields)
                    {
                        query += $"{field.Name} {field.DataType}, ";
                    }
                    query = query.TrimEnd(',', ' ') + ")";

                    using (SqlCeCommand command = new SqlCeCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }

            private void AddFieldToDatabase(SchemaField field, string connectionString)
            {
                try
                {
                    using (SqlCeConnection connection = new SqlCeConnection(connectionString))
                    {
                        connection.Open();

                        // SQL statement to add a new column to the table
                        string query = $"ALTER TABLE {TableName} ADD {field.Name} {field.DataType};";

                        using (SqlCeCommand command = new SqlCeCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch(Exception ex)
                {

                }
            }

            private void UpdateFieldTypeInDatabase(SchemaField field, string connectionString)
            {
                using (SqlCeConnection connection = new SqlCeConnection(connectionString))
                {
                    connection.Open();

                    // SQL statement to alter the data type of an existing column in the table
                    string query = $"ALTER TABLE {TableName} ALTER COLUMN {field.Name} {field.DataType};";

                    using (SqlCeCommand command = new SqlCeCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }

            private Dictionary<string, string> GetActualSchema(string connectionString)
            {
                Dictionary<string, string> actualSchema = new Dictionary<string, string>();

                using (SqlCeConnection connection = new SqlCeConnection(connectionString))
                {
                    connection.Open();

                    // Query to retrieve schema information for the table
                    string query = $"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{TableName}'";

                    using (SqlCeCommand command = new SqlCeCommand(query, connection))
                    {
                        using (SqlCeDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string columnName = reader["COLUMN_NAME"].ToString();
                                string dataType = reader["DATA_TYPE"].ToString();
                                actualSchema.Add(columnName, dataType);
                            }
                        }
                    }
                }

                return actualSchema;
            }
        }

        public class SchemaField
        {
            public string Name { get; set; }
            public string DataType { get; set; }

            public SchemaField(string name, string dataType)
            {
                Name = name;
                DataType = dataType;
            }
        }
    }
}
