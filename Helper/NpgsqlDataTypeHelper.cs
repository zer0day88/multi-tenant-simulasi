using System;
using System.Data;
using Dapper;
using Npgsql;
using NpgsqlTypes;

namespace Utility.PGSQLParameter.Helper
{
    public class JsonParameter : SqlMapper.ICustomQueryParameter
    {
        private readonly string _value;

        public JsonParameter(string value)
        {
            _value = value;
        }

        public void AddParameter(IDbCommand command, string name)
        {
            var parameter = new NpgsqlParameter(name, NpgsqlDbType.Json) { Value = _value };
            command.Parameters.Add(parameter);
        }
    }

    public class TimeParameter : SqlMapper.ICustomQueryParameter
    {
        private readonly TimeSpan _value;

        public TimeParameter(TimeSpan value)
        {
            _value = value;
        }

        public void AddParameter(IDbCommand command, string name)
        {
            var parameter = new NpgsqlParameter(name, NpgsqlDbType.Time) { Value = _value };
            command.Parameters.Add(parameter);
        }
    }

    public class DateParameter : SqlMapper.ICustomQueryParameter
    {
        private readonly DateTime _value;

        public DateParameter(DateTime value)
        {
            _value = value;
        }

        public void AddParameter(IDbCommand command, string name)
        {
            var parameter = new NpgsqlParameter(name, NpgsqlDbType.Date) { Value = _value };
            command.Parameters.Add(parameter);
        }
    }

    public class NullableDateParameter : SqlMapper.ICustomQueryParameter
    {
        private readonly DateTime? _value;

        public NullableDateParameter(DateTime? value)
        {
            _value = value;
        }

        public void AddParameter(IDbCommand command, string name)
        {
            
            var parameter = new NpgsqlParameter(name, NpgsqlDbType.Date);
            
            if (_value is null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = _value;
            }

            command.Parameters.Add(parameter);
        }
    }
}