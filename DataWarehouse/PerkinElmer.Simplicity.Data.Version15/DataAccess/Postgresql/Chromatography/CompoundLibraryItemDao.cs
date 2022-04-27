using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class CompoundLibraryItemDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "CompoundLibraryItem";
		internal static string IdColumn { get; } = "Id";
		internal static string CompoundNameColumn { get; } = "CompoundName";
        internal static string CompoundGuidColumn { get; } = "CompoundGuid";
		internal static string CreatedDateColumn { get; } = "CreatedDate";
		internal static string RetentionTimeColumn { get; } = "RetentionTime";
		internal static string SpectrumAbsorbancesColumn { get; } = "SpectrumAbsorbances";
		internal static string BaselineAbsorbancesColumn { get; } = "BaselineAbsorbances";
		internal static string StartWavelengthColumn { get; } = "StartWavelength";
		internal static string EndWavelengthColumn { get; } = "EndWavelength";
		internal static string StepColumn { get; } = "Step";
        internal static string IsBaselineCorrectedColumn { get; } = "IsBaselineCorrected";

        protected readonly string SqlInsert =
			$"INSERT INTO {TableName} " +
			$"({CompoundNameColumn}," +
            $"{CompoundGuidColumn}," +
			$"{CreatedDateColumn}," +
			$"{RetentionTimeColumn}," +
			$"{SpectrumAbsorbancesColumn}," +
			$"{BaselineAbsorbancesColumn}," +
			$"{StartWavelengthColumn}," +
			$"{EndWavelengthColumn}," +
			$"{StepColumn}," +
            $"{IsBaselineCorrectedColumn}) " +
            "VALUES " +
			$"(@{CompoundNameColumn}," +
            $"@{CompoundGuidColumn}," +
			$"@{CreatedDateColumn}," +
			$"@{RetentionTimeColumn}," +
			$"@{SpectrumAbsorbancesColumn}," +
			$"@{BaselineAbsorbancesColumn}," +
			$"@{StartWavelengthColumn}," +
			$"@{EndWavelengthColumn}," +
			$"@{StepColumn}," +
            $"@{IsBaselineCorrectedColumn}) ";

		protected readonly string SqlSelect =
			"SELECT " +
			$"{TableName}.{IdColumn}," +
			$"{TableName}.{CompoundNameColumn}," +
            $"{TableName}.{CompoundGuidColumn}," +
			$"{TableName}.{CreatedDateColumn}," +
			$"{TableName}.{RetentionTimeColumn}," +
			$"{TableName}.{SpectrumAbsorbancesColumn}," +
			$"{TableName}.{BaselineAbsorbancesColumn}," +
			$"{TableName}.{StartWavelengthColumn}," +
			$"{TableName}.{EndWavelengthColumn}," +
			$"{TableName}.{StepColumn}," +
            $"{TableName}.{IsBaselineCorrectedColumn} ";

		private readonly string _sqlUpdate =
			$"UPDATE {TableName} " +
			"SET " +
			$"{CompoundNameColumn}=@{CompoundNameColumn}," +
            $"{CompoundGuidColumn}=@{CompoundGuidColumn}," +
			$"{CreatedDateColumn}=@{CreatedDateColumn}," +
			$"{RetentionTimeColumn}=@{RetentionTimeColumn}," +
			$"{SpectrumAbsorbancesColumn}=@{SpectrumAbsorbancesColumn}," +
			$"{BaselineAbsorbancesColumn}=@{BaselineAbsorbancesColumn}," +
			$"{StartWavelengthColumn}=@{StartWavelengthColumn}," +
			$"{EndWavelengthColumn}=@{EndWavelengthColumn}," +
			$"{StepColumn}=@{StepColumn}," +
            $"{IsBaselineCorrectedColumn}=@{IsBaselineCorrectedColumn} ";


		public virtual void Create(IDbConnection connection, IList<CompoundLibraryItem> compoundLibraryItems)
		{
			try
			{
				foreach (var compoundLibraryItem in compoundLibraryItems)
				{

					if (compoundLibraryItem.CompoundName.Contains('\''))
					{
						var splitCount = compoundLibraryItem.CompoundName.Split('\'');
						if (splitCount.Length <= 3)
						{
							compoundLibraryItem.CompoundName = compoundLibraryItem.CompoundName.Replace("'", "''");
						}
					}

					compoundLibraryItem.Id = connection.ExecuteScalar<long>(
						SqlInsert +
						$"RETURNING {IdColumn}", compoundLibraryItem);
				}
			}
		    catch (Exception ex)
			{
				Log.Error($"Error in Create method", ex);
				throw;
			}
		}
		public void Create(IDbConnection connection, CompoundLibraryItem compoundLibraryItem)
		{
			try
			{
				if (compoundLibraryItem.CompoundName.Contains('\''))
				{
					var splitCount = compoundLibraryItem.CompoundName.Split('\'');
					if (splitCount.Length <= 3)
					{
						compoundLibraryItem.CompoundName = compoundLibraryItem.CompoundName.Replace("'", "''");
					}
				}

				compoundLibraryItem.Id = connection.ExecuteScalar<long>(
					SqlInsert +
					$"RETURNING {IdColumn}", compoundLibraryItem);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Create method", ex);
				throw;
			}
		}
		public CompoundLibraryItem GetProjectCompoundLibraryItem(IDbConnection connection, Guid projectGuid, Guid libraryGuid, Guid compoundGuid)
		{
            return connection.QueryFirstOrDefault<CompoundLibraryItem>(
				SqlSelect +
				$"FROM {ProjectDao.TableName} " +
				$"INNER JOIN {ProjectCompoundLibraryDao.TableName} ON {ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
				$"INNER JOIN {ProjectCompoundLibraryToLibraryItemMapDao.TableName} ON " +
					$"{ProjectCompoundLibraryToLibraryItemMapDao.TableName}.{ProjectCompoundLibraryToLibraryItemMapDao.ProjectCompoundLibraryIdColumn} = {ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.IdColumn} " +
				$"INNER JOIN {TableName} ON {TableName}.{IdColumn} = {ProjectCompoundLibraryToLibraryItemMapDao.TableName}.{ProjectCompoundLibraryToLibraryItemMapDao.CompoundLibraryItemIdColumn} " +
				"WHERE " +
				$"{ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
				$"{ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.LibraryGuidColumn} = @LibraryGuid AND " +
				$"{TableName}.{CompoundGuidColumn} = @CompoundGuid",
				new { ProjectGuid = projectGuid, LibraryGuid = libraryGuid, CompoundGuid = compoundGuid });
        }

        public CompoundLibraryItem GetProjectCompoundLibraryItem(IDbConnection connection, Guid projectGuid, Guid libraryGuid, string compoundName)
        {
            return connection.QueryFirstOrDefault<CompoundLibraryItem>(
                SqlSelect +
                $"FROM {ProjectDao.TableName} " +
                $"INNER JOIN {ProjectCompoundLibraryDao.TableName} ON {ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                $"INNER JOIN {ProjectCompoundLibraryToLibraryItemMapDao.TableName} ON " +
                $"{ProjectCompoundLibraryToLibraryItemMapDao.TableName}.{ProjectCompoundLibraryToLibraryItemMapDao.ProjectCompoundLibraryIdColumn} = {ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.IdColumn} " +
                $"INNER JOIN {TableName} ON {TableName}.{IdColumn} = {ProjectCompoundLibraryToLibraryItemMapDao.TableName}.{ProjectCompoundLibraryToLibraryItemMapDao.CompoundLibraryItemIdColumn} " +
                "WHERE " +
                $"{ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
                $"{ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.LibraryGuidColumn} = @LibraryGuid AND " +
                $"{TableName}.{CompoundNameColumn} = @CompoundName",
                new { ProjectGuid = projectGuid, LibraryGuid = libraryGuid, CompoundName = compoundName });
        }

		public List<CompoundLibraryItem> GetProjectCompoundLibraryItems(IDbConnection connection, Guid projectGuid, Guid libraryGuid)
		{
            return connection.Query<CompoundLibraryItem>(
				SqlSelect +
				$"FROM {ProjectDao.TableName} " +
				$"INNER JOIN {ProjectCompoundLibraryDao.TableName} ON {ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
				$"INNER JOIN {ProjectCompoundLibraryToLibraryItemMapDao.TableName} ON " +
					$"{ProjectCompoundLibraryToLibraryItemMapDao.TableName}.{ProjectCompoundLibraryToLibraryItemMapDao.ProjectCompoundLibraryIdColumn} = {ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.IdColumn} " +
				$"INNER JOIN {TableName} ON {TableName}.{IdColumn} = {ProjectCompoundLibraryToLibraryItemMapDao.TableName}.{ProjectCompoundLibraryToLibraryItemMapDao.CompoundLibraryItemIdColumn} " +
				"WHERE " +
				$"{ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
				$"{ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.LibraryGuidColumn} = @LibraryGuid ",
				new { ProjectGuid = projectGuid, LibraryGuid = libraryGuid }).ToList();

		}
		public List<CompoundLibraryItem> GetAnalysisResultSetCompoundLibraryItems(IDbConnection connection,
			Guid projectGuid,
			Guid analysisResultSetGuid,
			Guid libraryGuid)
		{
			return connection.Query<CompoundLibraryItem>(
				SqlSelect +
				$"FROM {ProjectDao.TableName} " +
				$"INNER JOIN {AnalysisResultSetDao.TableName} ON {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
				$"INNER JOIN {SnapshotCompoundLibraryDao.TableName} ON {SnapshotCompoundLibraryDao.TableName}.{SnapshotCompoundLibraryDao.AnalysisResultSetIdColumn} = {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn} " +
				$"INNER JOIN {SnapshotCompoundLibraryToLibraryItemMapDao.TableName} ON " +
					$"{SnapshotCompoundLibraryToLibraryItemMapDao.TableName}.{SnapshotCompoundLibraryToLibraryItemMapDao.SnapshotCompoundLibraryIdColumn} = {SnapshotCompoundLibraryDao.TableName}.{SnapshotCompoundLibraryDao.IdColumn} " +
				$"INNER JOIN {TableName} ON {TableName}.{IdColumn} = {SnapshotCompoundLibraryToLibraryItemMapDao.TableName}.{SnapshotCompoundLibraryToLibraryItemMapDao.CompoundLibraryItemIdColumn} " +
				"WHERE " +
				$"{ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
				$"{AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.GuidColumn} = @AnalysisResultSetGuid AND " +
				$"{SnapshotCompoundLibraryDao.TableName}.{SnapshotCompoundLibraryDao.LibraryGuidColumn} = @LibraryGuid ",
				new { ProjectGuid = projectGuid, AnalysisResultSetGuid = analysisResultSetGuid, LibraryGuid = libraryGuid }).ToList();

		}
        public List<CompoundLibraryItem> GetProjectCompoundLibraryItemsByRetentionTime(IDbConnection connection, Guid projectGuid, Guid libraryGuid, double retentionTime, 
			double retentionTimeTolerance)
		{
			try
			{
				double low = retentionTime - retentionTimeTolerance;
				double high = retentionTime + retentionTimeTolerance;

                return connection.Query<CompoundLibraryItem>(
					SqlSelect +
					$"FROM {ProjectDao.TableName} " +
					$"INNER JOIN {ProjectCompoundLibraryDao.TableName} ON {ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
					$"INNER JOIN {ProjectCompoundLibraryToLibraryItemMapDao.TableName} ON " +
					$"{ProjectCompoundLibraryToLibraryItemMapDao.TableName}.{ProjectCompoundLibraryToLibraryItemMapDao.ProjectCompoundLibraryIdColumn} = {ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.IdColumn} " +
					$"INNER JOIN {TableName} ON {TableName}.{IdColumn} = {ProjectCompoundLibraryToLibraryItemMapDao.TableName}.{ProjectCompoundLibraryToLibraryItemMapDao.CompoundLibraryItemIdColumn} " +
					"WHERE " +
					$"{ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
					$"{ProjectCompoundLibraryDao.TableName}.{ProjectCompoundLibraryDao.LibraryGuidColumn} = @LibraryGuid AND " +
					$"({RetentionTimeColumn} BETWEEN @Low AND @High)",
					new { ProjectGuid = projectGuid, LibraryGuid = libraryGuid, Low = low, High = high }).ToList();
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetProjectCompoundLibraryItemsByRetentionTime method", ex);
				throw;
			}
		}

        public CompoundLibraryItem GetProjectCompoundLibraryItem(IDbConnection connection, long compoundItemLibraryId)
        {
	        return connection.QueryFirstOrDefault<CompoundLibraryItem>(
		        SqlSelect + $"FROM {TableName} WHERE {IdColumn} = @Id",
		        new {Id = compoundItemLibraryId});
		}
        public CompoundLibraryItem GetProjectCompoundLibraryItem(IDbConnection connection, long projectCompoundLibraryId, Guid compoundGuid)
        {
	        return connection.QueryFirstOrDefault<CompoundLibraryItem>(
		        SqlSelect +
				$"FROM {ProjectCompoundLibraryToLibraryItemMapDao.TableName} " +
		        $"INNER JOIN {TableName} ON {TableName}.{IdColumn} = {ProjectCompoundLibraryToLibraryItemMapDao.TableName}.{ProjectCompoundLibraryToLibraryItemMapDao.CompoundLibraryItemIdColumn} " +
		        $"WHERE {ProjectCompoundLibraryToLibraryItemMapDao.TableName}.{ProjectCompoundLibraryToLibraryItemMapDao.ProjectCompoundLibraryIdColumn} = @CompoundLibraryId AND " +
		        $"{TableName}.{CompoundGuidColumn} = @CompoundGuid",
		        new { CompoundLibraryId = projectCompoundLibraryId, CompoundGuid = compoundGuid });
        }
		public void Update(IDbConnection connection, List<CompoundLibraryItem> compoundLibraryItems)
		{
			try
			{
			    foreach (var compoundLibraryItem in compoundLibraryItems)
			    {
				   connection.Execute(
					_sqlUpdate +
					$"WHERE LOWER({CompoundNameColumn}) =LOWER('{compoundLibraryItem.CompoundName.Replace("'", "''")}') ",
					compoundLibraryItem);
			    }
			
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Update method", ex);
				throw;
			}
		}
		public void UpdateRetentionTime(IDbConnection connection, long compoundLibraryId, Guid compoundGuid, double newRetentionTime)
		{
			try
			{
				connection.Execute(
					$"UPDATE {TableName} " +
					$"SET {RetentionTimeColumn}=@{RetentionTimeColumn} " +
					$"WHERE {CompoundGuidColumn} = '{compoundGuid}'",
					new { RetentionTime = newRetentionTime });
			}
			catch (Exception ex)
			{
				Log.Error($"Error in UpdateRetentionTime method", ex);
				throw;
			}
		}

		public bool Rename(IDbConnection connection, long compoundLibraryId, Guid compoundGuid, string newCompoundName)
		{
			try
			{
				var rowsUpdated = connection.Execute(
					$"UPDATE {TableName} " +
					$"SET {CompoundNameColumn}=@{CompoundNameColumn} " +
					$"WHERE {CompoundGuidColumn} = '{compoundGuid}'",
					new {CompoundName = newCompoundName});

				return (rowsUpdated != 0);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Rename method", ex);
				throw;
			}
		}
		public bool Delete(IDbConnection connection, long compoundLibraryItemId)
		{
			try
			{
				var rowsUpdated = connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {IdColumn}= @Id",
					new {Id = compoundLibraryItemId});

				return (rowsUpdated != 0);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Delete method", ex);
				throw;
			}
		}

		public bool IsExists(IDbConnection connection, Guid projectGuid, Guid libraryGuid, string compoundName)
		{
			try
			{
				var compoundLibraryItem = GetProjectCompoundLibraryItem(connection, projectGuid, libraryGuid, compoundName);
				return compoundLibraryItem != null;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in IsExists method", ex);
				throw;
			}
		}
	}
}
