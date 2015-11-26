using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Model;

namespace Affecto.EntityFramework.PostgreSql
{
    public abstract class PostgreSqlDbMigration : DbMigration
    {
        protected abstract string ResolveSchemaName();

        /// <summary>
        /// Adds schema name as prefix to table name if present. Format: SchemaName.TableName or TableName.
        /// </summary>
        protected string FormatTableNameWithSchemaName(string tableName)
        {
            string schema = ResolveSchemaName();

            if (!string.IsNullOrEmpty(schema))
            {
                return string.Format("{0}.{1}", schema, tableName);
            }

            return tableName;
        }

        /// <summary>
        /// Adds schema name as prefix to table name if present. Adds also quotes around schema and table name. Format: "SchemaName"."TableName" or "TableName".
        /// </summary>
        protected string FormatTableNameWithSchemaNameAndQuotes(string tableName)
        {
            string schema = ResolveSchemaName();

            if (!string.IsNullOrEmpty(schema))
            {
                return string.Format("\"{0}\".\"{1}\"", schema, tableName);
            }

            return string.Format("\"{0}\"", tableName);
        }

        protected new TableBuilder<TColumns> CreateTable<TColumns>(string name, Func<ColumnBuilder, TColumns> columnsAction, object anonymousArguments = null)
        {
            return base.CreateTable(FormatTableNameWithSchemaName(name), columnsAction, anonymousArguments);
        }

        protected new TableBuilder<TColumns> CreateTable<TColumns>(string name, Func<ColumnBuilder, TColumns> columnsAction, IDictionary<string, object> annotations, object anonymousArguments = null)
        {
            return base.CreateTable(FormatTableNameWithSchemaName(name), columnsAction, annotations, anonymousArguments);
        }

        protected new void AlterTableAnnotations<TColumns>(string name, Func<ColumnBuilder, TColumns> columnsAction, IDictionary<string, AnnotationValues> annotations, object anonymousArguments = null)
        {
            base.AlterTableAnnotations(FormatTableNameWithSchemaName(name), columnsAction, annotations, anonymousArguments);
        }

        protected new void AddForeignKey(string dependentTable, string dependentColumn, string principalTable, string principalColumn = null, bool cascadeDelete = false, string name = null, object anonymousArguments = null)
        {
            base.AddForeignKey(FormatTableNameWithSchemaName(dependentTable), dependentColumn, FormatTableNameWithSchemaName(principalTable), principalColumn, cascadeDelete, name, anonymousArguments);
        }

        protected new void AddForeignKey(string dependentTable, string[] dependentColumns, string principalTable, string[] principalColumns = null, bool cascadeDelete = false, string name = null, object anonymousArguments = null)
        {
            base.AddForeignKey(FormatTableNameWithSchemaName(dependentTable), dependentColumns, FormatTableNameWithSchemaName(principalTable), principalColumns, cascadeDelete, name, anonymousArguments);
        }

        protected new void DropForeignKey(string dependentTable, string name, object anonymousArguments = null)
        {
            base.DropForeignKey(FormatTableNameWithSchemaName(dependentTable), name, anonymousArguments);
        }

        protected new void DropForeignKey(string dependentTable, string dependentColumn, string principalTable, object anonymousArguments = null)
        {
            base.DropForeignKey(FormatTableNameWithSchemaName(dependentTable), dependentColumn, FormatTableNameWithSchemaName(principalTable), anonymousArguments);
        }

        protected new void DropForeignKey(string dependentTable, string[] dependentColumns, string principalTable, object anonymousArguments = null)
        {
            base.DropForeignKey(FormatTableNameWithSchemaName(dependentTable), dependentColumns, FormatTableNameWithSchemaName(principalTable), anonymousArguments);
        }

        protected new void DropTable(string name, object anonymousArguments = null)
        {
            base.DropTable(FormatTableNameWithSchemaName(name), anonymousArguments);
        }

        protected new void DropTable(string name, IDictionary<string, IDictionary<string, object>> removedColumnAnnotations, object anonymousArguments = null)
        {
            base.DropTable(FormatTableNameWithSchemaName(name), removedColumnAnnotations, anonymousArguments);
        }

        protected new void DropTable(string name, IDictionary<string, object> removedAnnotations, object anonymousArguments = null)
        {
            base.DropTable(FormatTableNameWithSchemaName(name), removedAnnotations, anonymousArguments);
        }

        protected new void DropTable(string name, IDictionary<string, object> removedAnnotations, IDictionary<string, IDictionary<string, object>> removedColumnAnnotations, object anonymousArguments = null)
        {
            base.DropTable(FormatTableNameWithSchemaName(name), removedAnnotations, removedColumnAnnotations, anonymousArguments);
        }

        protected new void MoveTable(string name, string newSchema, object anonymousArguments = null)
        {
            base.MoveTable(FormatTableNameWithSchemaName(name), newSchema, anonymousArguments);
        }

        protected new void RenameTable(string name, string newName, object anonymousArguments = null)
        {
            base.RenameTable(FormatTableNameWithSchemaName(name), newName, anonymousArguments);
        }

        /// <summary>
        /// Needs to be used if running database tests with SQL Server CE
        /// </summary>
        protected void RenameColumn(string table, string name, string newName, Func<ColumnBuilder, ColumnModel> columnAction)
        {
            string tempColumnName = string.Format("_{0}temp", newName);
            AddColumn(table, tempColumnName, columnAction);
            Sql(string.Format("UPDATE {0} SET {1} = {2}", FormatTableNameWithSchemaNameAndQuotes(table), tempColumnName, name));
            DropColumn(table, name);
            AddColumn(table, newName, columnAction);
            Sql(string.Format("UPDATE {0} SET {1} = {2}", FormatTableNameWithSchemaNameAndQuotes(table), newName, tempColumnName));
            DropColumn(table, tempColumnName);
        }

        /// <summary>
        /// Needs to be used if running database tests with SQL Server CE
        /// </summary>
        protected void RenameForeignKeyColumn(string table, string name, string newName, Func<ColumnBuilder, ColumnModel> columnAction, string principalTable, string principalColumn,
            string foreignKeyName)
        {
            string tempColumnName = string.Format("_{0}temp", newName);
            AddColumn(table, tempColumnName, columnAction);
            Sql(string.Format("UPDATE {0} SET {1} = {2}", FormatTableNameWithSchemaNameAndQuotes(table), tempColumnName, name));
            DropForeignKey(table, foreignKeyName);
            DropColumn(table, name);
            AddColumn(table, newName, columnAction);
            Sql(string.Format("UPDATE {0} SET {1} = {2}", FormatTableNameWithSchemaNameAndQuotes(table), newName, tempColumnName));
            DropColumn(table, tempColumnName);
            AddForeignKey(table, newName, principalTable, principalColumn, false, foreignKeyName);
        }

        protected new void RenameColumn(string table, string name, string newName, object anonymousArguments = null)
        {
            base.RenameColumn(FormatTableNameWithSchemaName(table), name, newName, anonymousArguments);
        }

        protected new void AddColumn(string table, string name, Func<ColumnBuilder, ColumnModel> columnAction, object anonymousArguments = null)
        {
            base.AddColumn(FormatTableNameWithSchemaName(table), name, columnAction, anonymousArguments);
        }

        protected new void DropColumn(string table, string name, object anonymousArguments = null)
        {
            base.DropColumn(FormatTableNameWithSchemaName(table), name, anonymousArguments);
        }

        protected new void DropColumn(string table, string name, IDictionary<string, object> removedAnnotations, object anonymousArguments = null)
        {
            base.DropColumn(FormatTableNameWithSchemaName(table), name, removedAnnotations, anonymousArguments);
        }

        protected new void AlterColumn(string table, string name, Func<ColumnBuilder, ColumnModel> columnAction, object anonymousArguments = null)
        {
            base.AlterColumn(FormatTableNameWithSchemaName(table), name, columnAction, anonymousArguments);
        }

        protected new void AddPrimaryKey(string table, string column, string name = null, bool clustered = true, object anonymousArguments = null)
        {
            base.AddPrimaryKey(FormatTableNameWithSchemaName(table), column, name, clustered, anonymousArguments);
        }

        protected new void AddPrimaryKey(string table, string[] columns, string name = null, bool clustered = true, object anonymousArguments = null)
        {
            base.AddPrimaryKey(FormatTableNameWithSchemaName(table), columns, name, clustered, anonymousArguments);
        }

        protected new void DropPrimaryKey(string table, string name, object anonymousArguments = null)
        {
            base.DropPrimaryKey(FormatTableNameWithSchemaName(table), name, anonymousArguments);
        }

        protected new void DropPrimaryKey(string table, object anonymousArguments = null)
        {
            base.DropPrimaryKey(FormatTableNameWithSchemaName(table), anonymousArguments);
        }

        protected new void CreateIndex(string table, string column, bool unique = false, string name = null, bool clustered = false, object anonymousArguments = null)
        {
            base.CreateIndex(FormatTableNameWithSchemaName(table), column, unique, name, clustered, anonymousArguments);
        }

        protected new void CreateIndex(string table, string[] columns, bool unique = false, string name = null, bool clustered = false, object anonymousArguments = null)
        {
            base.CreateIndex(FormatTableNameWithSchemaName(table), columns, unique, name, clustered, anonymousArguments);
        }

        protected new void DropIndex(string table, string name, object anonymousArguments = null)
        {
            base.DropIndex(FormatTableNameWithSchemaName(table), name, anonymousArguments);
        }

        protected new void DropIndex(string table, string[] columns, object anonymousArguments = null)
        {
            base.DropIndex(FormatTableNameWithSchemaName(table), columns, anonymousArguments);
        }

        protected new void RenameIndex(string table, string name, string newName, object anonymousArguments = null)
        {
            base.RenameIndex(FormatTableNameWithSchemaName(table), name, newName, anonymousArguments);
        }
    }
}