using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.Auth
{
    public class UserInfoModel
    {
        #region 帳號下轄參數
        public string ID { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserLevel { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion
    }
}
