using System;

namespace SwaggerTest.Models
{

    /// <summary>
    /// 人类
    /// </summary>
    public class People
    {

        /// <summary>
        ///名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        ///性别，0为男，1为女，2为其它
        /// </summary>
        public int Gender { get; set; }

    }
}
