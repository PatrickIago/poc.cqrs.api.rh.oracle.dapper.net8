using Poc.Oracle.DTO;

namespace Poc.Oracle.SQL;

public static class RegionSqlConsts
{
    public const string SQL_GET =
   @$"
      SELECT r.REGION_ID AS {nameof(RegionSqlDTO.RegionId)},
           r.REGION_NAME AS {nameof(RegionSqlDTO.RegionName)},
           c.COUNTRY_NAME AS {nameof(RegionSqlDTO.CountryName)}
      FROM HR.REGIONS r
      LEFT JOIN HR.COUNTRIES c ON r.REGION_ID = c.REGION_ID
  ";

    public const string SQL_MAX = @$"SELECT MAX(REGION_ID+1) FROM HR.REGIONS";

    public const string SQL_GET_BY_ID =
   @$"
      SELECT r.REGION_ID as {nameof(RegionSqlDTO.RegionId)}, 
           r.REGION_NAME as {nameof(RegionSqlDTO.RegionName)}, 
           c.COUNTRY_NAME as {nameof(RegionSqlDTO.CountryName)}
       FROM HR.REGIONS r
       LEFT JOIN HR.COUNTRIES c ON r.REGION_ID = c.REGION_ID
      WHERE r.REGION_ID = :PR_REGION_ID
  ";

    public const string SQL_INSERT = @$"INSERT INTO HR.REGIONS (REGION_ID, REGION_NAME) VALUES (:PR_REGION_ID, :PR_REGION_NAME)";

    public const string SQL_UPDATE =
    @$"
        UPDATE HR.REGIONS
        SET REGION_NAME = :PR_REGION_NAME
        WHERE REGION_ID = :PR_REGION_ID
    ";

    public const string SQL_DELETE =
    @$"
        UPDATE HR.REGIONS
        SET IS_ACTIVE = 0,
            USER_ID_DELETED = :PR_USER_ID_DELETED,
            DELETED_AT = :PR_DELETED_AT
        WHERE REGION_ID = :PR_REGION_ID
    ";
}
