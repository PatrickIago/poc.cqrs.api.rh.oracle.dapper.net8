using Poc.Oracle.DTO; 

namespace Poc.Oracle.SQL;

public static class DepartamentSqlConsts
{
    public const string SQL_GET =
    @$"
            SELECT DEPARTMENT_ID as {nameof(DepartmentSqlDTO.Id)}, 
                   DEPARTMENT_NAME as {nameof(DepartmentSqlDTO.Name)}, 
                   MANAGER_ID as {nameof(DepartmentSqlDTO.ManagerId)}, 
                   LOCATION_ID as {nameof(DepartmentSqlDTO.Location)}
            FROM HR.DEPARTMENTS
        ";

    public const string SQL_MAX = @$"SELECT MAX(DEPARTMENT_ID + 1) FROM HR.DEPARTMENTS";

    public const string SQL_GET_BY_ID =
    @$"
            SELECT DEPARTMENT_ID as {nameof(DepartmentSqlDTO.Id)}, 
                   DEPARTMENT_NAME as {nameof(DepartmentSqlDTO.Name)}, 
                   MANAGER_ID as {nameof(DepartmentSqlDTO.ManagerId)}, 
                   LOCATION_ID as {nameof(DepartmentSqlDTO.Location)}
            FROM HR.DEPARTMENTS
            WHERE DEPARTMENT_ID = :PR_DEPARTMENT_ID
        ";


    public const string SQL_INSERT =
    @$"
            INSERT INTO HR.DEPARTMENTS (DEPARTMENT_ID, DEPARTMENT_NAME, MANAGER_ID, LOCATION_ID) 
            VALUES (:PR_DEPARTMENT_ID, :PR_DEPARTMENT_NAME, :PR_MANAGER_ID, :PR_LOCATION_ID)
        ";

    public const string SQL_UPDATE =
    @$"
           UPDATE HR.DEPARTMENTS
           SET DEPARTMENT_NAME = :PR_DEPARTMENT_NAME,
               MANAGER_ID = :PR_MANAGER_ID,
               LOCATION_ID = :PR_LOCATION_ID
           WHERE DEPARTMENT_ID = :PR_DEPARTMENT_ID
        ";


    public const string SQL_DELETE =
    @$"
            DELETE FROM HR.DEPARTMENTS
            WHERE DEPARTMENT_ID = :PR_DEPARTMENT_ID
        ";
}