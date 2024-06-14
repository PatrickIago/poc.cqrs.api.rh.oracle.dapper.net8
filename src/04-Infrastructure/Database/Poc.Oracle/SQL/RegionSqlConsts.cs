using Poc.Oracle.DTO;

namespace Poc.Oracle.SQL;

public static class RegionSqlConsts
{
    public const string SQL_GET =
    @$"
        SELECT REGION_ID as {nameof(RegionSqlDTO.RegionId)}, 
               REGION_NAME as {nameof(RegionSqlDTO.RegionName)} 
        FROM HR.REGIONS
    ";

    public const string SQL_MAX = @$"SELECT MAX(REGION_ID+1) FROM HR.REGIONS";

    public const string SQL_GET_BY_ID =
    @$"
        SELECT REGION_ID as {nameof(RegionSqlDTO.RegionId)}, 
               REGION_NAME as {nameof(RegionSqlDTO.RegionName)} 
        FROM HR.REGIONS
        WHERE REGION_ID = :PR_REGION_ID
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
        DELETE FROM HR.REGIONS
        WHERE REGION_ID = :PR_REGION_ID
    ";
}
