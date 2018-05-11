using System.Data;

namespace BaseProject.Common.Model
{
    public class Parameter
    {
        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
        public Parameter(string name, SqlDbType type, int size)
        {
            Name = name;
            Type = type;
            Size = size;
        }

        public string Name { get; set; }
        public object Value { get; set; }
        public SqlDbType Type { get; set; }
        public ParameterDirection Direction { get; set; }
        public int Size { get; set; }
    }
}
