using Poc.Oracle.DTO;
namespace Poc.Oracle.SQL;

public static class JobHistorySqlConsts
{
    public const string SQL_GET =
    @$"
            SELECT EMPLOYEE_ID as {nameof(JobHistorySqlDTO.EmployeeId)}, 
                   START_DATE as {nameof(JobHistorySqlDTO.StartDate)}, 
                   END_DATE as {nameof(JobHistorySqlDTO.EndDate)},
                   JOB_ID as {nameof(JobHistorySqlDTO.JobId)},
                   DEPARTMENT_ID as {nameof(JobHistorySqlDTO.DepartmentId)}
            FROM HR.JOB_HISTORY
        ";

    public const string SQL_GET_BY_ID =
    @$"
            SELECT EMPLOYEE_ID as {nameof(JobHistorySqlDTO.EmployeeId)}, 
                   START_DATE as {nameof(JobHistorySqlDTO.StartDate)}, 
                   END_DATE as {nameof(JobHistorySqlDTO.EndDate)},
                   JOB_ID as {nameof(JobHistorySqlDTO.JobId)},
                   DEPARTMENT_ID as {nameof(JobHistorySqlDTO.DepartmentId)}
            FROM HR.JOB_HISTORY
            WHERE EMPLOYEE_ID = :PR_EMPLOYEE_ID
        ";

    public const string SQL_INSERT =
    @$"
    INSERT INTO HR.JOB_HISTORY (EMPLOYEE_ID, START_DATE, END_DATE, JOB_ID, DEPARTMENT_ID) 
    VALUES (:PR_EMPLOYEE_ID, :PR_START_DATE, :PR_END_DATE, :PR_JOB_ID, :PR_DEPARTMENT_ID)
    ";

    public const string SQL_UPDATE =
    @$"
            UPDATE HR.JOB_HISTORY
            SET START_DATE = :PR_START_DATE,
                END_DATE = :PR_END_DATE,
                JOB_ID = :PR_JOB_ID,
                DEPARTMENT_ID = :PR_DEPARTMENT_ID
            WHERE EMPLOYEE_ID = :PR_EMPLOYEE_ID
        ";

    public const string SQL_DELETE =
    @$"
            DELETE FROM HR.JOB_HISTORY
            WHERE EMPLOYEE_ID = :PR_EMPLOYEE_ID
        ";
}