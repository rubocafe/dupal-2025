using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

// Harshan Nishantha
// 2013-02-28

namespace LucidLibrary.Db
{
    public class TcStoredProcedure
    {
        public string CommandText { get; set; }
        private List<SqlParameter> InputParameters { get; set; }
        private List<SqlParameter> OutputParameters { get; set; }

        public TcStoredProcedure(string commandText)
        {
            this.CommandText = commandText;
            InputParameters  = new List<SqlParameter>();
            OutputParameters = new List<SqlParameter>();
        }

        public void AddInputParameter(string name, SqlDbType type, object value)
        {
            SqlParameter parameter  = new SqlParameter();
            parameter.ParameterName = name;
            parameter.SqlDbType     = type;
            parameter.Value         = value;
            parameter.Direction     = ParameterDirection.Input;

            InputParameters.Add(parameter);
        }

        public void AddOutputParameter(string name, SqlDbType type)
        {
            SqlParameter parameter  = new SqlParameter();
            parameter.ParameterName = name;
            parameter.SqlDbType     = type;
            parameter.Direction     = ParameterDirection.Output;

            OutputParameters.Add(parameter);
        }

        public SqlParameter GetInputParameter(string name)
        {
            SqlParameter requestedParameter = null;
            foreach (SqlParameter parameter in InputParameters)
            {
                if (parameter.ParameterName.Equals(name))
                {
                    requestedParameter = parameter;
                    break;
                }
            }

            return requestedParameter;
        }

        public SqlParameter GetOutputParameter(string name)
        {
            SqlParameter requestedParameter = null;
            foreach (SqlParameter parameter in OutputParameters)
            {
                if (parameter.ParameterName.Equals(name))
                {
                    requestedParameter = parameter;
                    break;
                }
            }

            return requestedParameter;
        }

        public SqlCommand GetCommand()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = CommandText;
            command.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter parameter in InputParameters)
            {
                command.Parameters.Add(parameter);
            }

            foreach (SqlParameter parameter in OutputParameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }
    }
}
