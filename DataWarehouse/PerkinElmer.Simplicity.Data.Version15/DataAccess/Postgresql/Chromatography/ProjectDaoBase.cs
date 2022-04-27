using System.Reflection;
using log4net;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
    public class ProjectDaoBase
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static string TableName { get; } = "Project";

        internal static string IdColumn { get; } = "Id";
        internal static string GuidColumn { get; } = "Guid";
        internal static string ProjectNameColumn { get; } = "Name";
        internal static string DescriptionColumn { get; } = "Description";
        internal static string IsEnabledColumn { get; } = "IsEnabled";
        internal static string IsSecurityOnColumn { get; } = "IsSecurityOn";
        internal static string IsESignatureOnColumn { get; } = "IsESignatureOn";
        internal static string IsReviewApprovalOnColumn { get; } = "IsReviewApprovalOn";
        internal static string CreatedDateColumn { get; } = "CreatedDate";
        internal static string CreatedUserIdColumn { get; } = "CreatedUserId";
        internal static string ModifiedDateColumn { get; } = "ModifiedDate";
        internal static string ModifiedUserIdColumn { get; } = "ModifiedUserId";
        internal static string StartDateColumn { get; } = "StartDate";
        internal static string EndDateColumn { get; } = "EndDate";
        internal static string CreatedUserNameColumn { get; } = "CreatedUserName";
        internal static string ModifiedUserNameColumn { get; } = "ModifiedUserName";

        protected readonly string InsertSql =
            $"INSERT INTO {TableName} " +
            $"({GuidColumn}," +
            $"{ProjectNameColumn}," +
            $"{DescriptionColumn}," +
            $"{IsEnabledColumn}," +
            $"{IsSecurityOnColumn}," +
            $"{IsESignatureOnColumn}," +
            $"{IsReviewApprovalOnColumn}," +
            $"{CreatedDateColumn}," +
            $"{CreatedUserIdColumn}," +
            $"{CreatedUserNameColumn}," +
            $"{ModifiedDateColumn}," +
            $"{ModifiedUserIdColumn}," +
            $"{ModifiedUserNameColumn}," +
            $"{StartDateColumn}," +
            $"{EndDateColumn}) " +
            "VALUES " +
            $"(@{GuidColumn}," +
            $"@{ProjectNameColumn}," +
            $"@{DescriptionColumn}," +
            $"@{IsEnabledColumn}," +
            $"@{IsSecurityOnColumn}," +
            $"@{IsESignatureOnColumn}," +
            $"@{IsReviewApprovalOnColumn}," +
            $"@{CreatedDateColumn}," +
            $"@{CreatedUserIdColumn}," +
            $"@{CreatedUserNameColumn}," +
            $"@{ModifiedDateColumn}," +
            $"@{ModifiedUserIdColumn}," +
            $"@{ModifiedUserNameColumn}," +
            $"@{StartDateColumn}," +
            $"@{EndDateColumn}) ";

        protected readonly string SelectSql =
            "SELECT " +
            $"{IdColumn}, " +
            $"{GuidColumn}," +
            $"{ProjectNameColumn}," +
            $"{DescriptionColumn}," +
            $"{IsEnabledColumn}," +
            $"{IsSecurityOnColumn}," +
            $"{IsESignatureOnColumn}," +
            $"{IsReviewApprovalOnColumn}," +
            $"{CreatedDateColumn}," +
            $"{CreatedUserIdColumn}," +
            $"{CreatedUserNameColumn}," +
            $"{ModifiedDateColumn}," +
            $"{ModifiedUserIdColumn}," +
            $"{ModifiedUserNameColumn}," +
            $"{StartDateColumn}," +
            $"{EndDateColumn} " +
            $"FROM {TableName} ";
    }
}
