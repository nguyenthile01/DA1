using System.Collections.Generic;

namespace Y.Services
{
    public class SettingDto
    {
        public string GroupName { get; set; }
        public List<SettingDetail> Settings { get; set; }

    }

    public class SettingDetail
    {
        public string Name { get; set; }
        public string SettingKey { get; set; }
        public object Value { get; set; }
        public string ValueType { get; set; } = "String";
    }
}
