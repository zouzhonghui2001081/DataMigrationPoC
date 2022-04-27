using System.Reflection;
using log4net;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class AcquisitionMethodBaseDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "AcquisitionMethod";
		internal static string IdColumn { get; } = "Id";
		internal static string MethodNameColumn { get; } = "MethodName";
		internal static string ReconciledRunTimeColumn { get; } = "ReconciledRunTime";
		internal static string CreateDateColumn { get; } = "CreateDate";
		internal static string ModifyDateColumn { get; } = "ModifyDate";
		internal static string CreateUserIdColumn { get; } = "CreateUserId";
		internal static string CreateUserNameColumn { get; } = "CreateUserName";
		internal static string ModifyUserIdColumn { get; } = "ModifyUserId";
		internal static string ModifyUserNameColumn { get; } = "ModifyUserName";
		internal static string GuidColumn { get; } = "Guid";
		internal static string VersionNumberColumn { get; } = "VersionNumber";
		internal static string ReviewApproveStateColumn { get; } = "ReviewApproveState";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({MethodNameColumn}," +
		                                      $"{ReconciledRunTimeColumn}," +
		                                      $"{CreateDateColumn}," +
		                                      $"{ModifyDateColumn}," +
		                                      $"{ModifyUserIdColumn}," +
		                                      $"{ModifyUserNameColumn}," +
		                                      $"{CreateUserIdColumn}," +
		                                      $"{CreateUserNameColumn}," +
		                                      $"{GuidColumn}," +
											  $"{VersionNumberColumn}," +
											  $"{ReviewApproveStateColumn}) " +
		                                      "VALUES " +
		                                      $"(@{MethodNameColumn}," +
		                                      $"@{ReconciledRunTimeColumn}," +
		                                      $"@{CreateDateColumn}," +
		                                      $"@{ModifyDateColumn}," +
		                                      $"@{ModifyUserIdColumn}," +
		                                      $"@{ModifyUserNameColumn}," +
		                                      $"@{CreateUserIdColumn}," +
		                                      $"@{CreateUserNameColumn}," +
		                                      $"@{GuidColumn}, " +
		                                      $"@{VersionNumberColumn}, " +
											  $"@{ReviewApproveStateColumn}) ";

		protected readonly string SelectSql = $"SELECT {TableName}.{IdColumn}," +
		                                      $"{TableName}.{MethodNameColumn}," +
		                                      $"{TableName}.{ReconciledRunTimeColumn}," +
		                                      $"{TableName}.{CreateDateColumn}," +
		                                      $"{TableName}.{ModifyDateColumn}," +
		                                      $"{TableName}.{ModifyUserIdColumn}," +
		                                      $"{TableName}.{ModifyUserNameColumn}," +
		                                      $"{TableName}.{CreateUserIdColumn}," +
		                                      $"{TableName}.{CreateUserNameColumn}," +
		                                      $"{TableName}.{ReviewApproveStateColumn}," +
		                                      $"{TableName}.{VersionNumberColumn}," +
											  $"{TableName}.{GuidColumn} ";
	}
}
