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
PAUSE
GOTO:EOF


::Rename files in directory
:RENAME_FILES %~1
ECHO Move to folder:%source_folder%\%~1
cd /d "%source_folder%\%~1"
ECHO Renameing files in "%source_folder%\%~1"
REN *_2014-03.* *_2014-05.*
GOTO:EOF