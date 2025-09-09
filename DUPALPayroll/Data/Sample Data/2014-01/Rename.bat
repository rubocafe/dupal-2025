SET source_folder=%CD%

@ECHO OFF
ECHO "Source Folder:%source_folder%"
CALL:RENAME_FILES "Auditors"
CALL:RENAME_FILES "Call Center Inbound"
CALL:RENAME_FILES "Call Center Outbound"
CALL:RENAME_FILES "Care Takers"
CALL:RENAME_FILES "Commission Agents"
CALL:RENAME_FILES "Customer Care"
CALL:RENAME_FILES "Office Staff"
CALL:RENAME_FILES "Premier Sales"
CALL:RENAME_FILES "Supervisors And Back Office"
CALL:RENAME_FILES "Shared"
CD "%source_folder%"
PAUSE
@ECHO ON
DIR /s /B
GOTO:EOF


::Rename files in directory
:RENAME_FILES %~1
ECHO Move to folder:%source_folder%\%~1
cd "%source_folder%\%~1"
ECHO Renameing excel files
rename *_2014-01.xls *_2014-03.xls
rename *_2014-01.xlsx *_2014-03.xlsx
rename *_2014-01.ods *_2014-03.ods
ECHO Renaming CSV files
rename *_2014-01.csv *_2014-03.csv
GOTO:EOF